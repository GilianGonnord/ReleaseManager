using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReleaseManager.Api.GitPilot;
using ReleaseManager.Api.Hubs;
using ReleaseManager.Api.Policies.Handers;
using ReleaseManager.Api.Policies.JsonNaming;
using ReleaseManager.Api.Policies.Requirements;
using ReleaseManager.Api.Repositories;
using ReleaseManager.Model;
using ReleaseManager.Model.Enums;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

FirebaseApp.Create(new AppOptions()
{
    //Credential = GoogleCredential.GetApplicationDefault(),
    Credential = GoogleCredential.FromFile(builder.Configuration["ReleaseManager:FirebaseConfidential"]),
    ProjectId = "release-manager-f2b47",
});

var projectId = FirebaseApp.DefaultInstance.Name;
var firebaseAppId = FirebaseApp.DefaultInstance.Options.ProjectId;

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/" + firebaseAppId;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://securetoken.google.com/" + firebaseAppId,
            ValidateAudience = true,
            ValidAudience = firebaseAppId,
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization(options => options.AddPolicy("Admin", policy => policy.AddRequirements(new RoleRequirement(Roles.Admin))));
builder.Services.AddAuthorization(options => options.AddPolicy("Manager", policy => policy.AddRequirements(new RoleRequirement(Roles.Manager))));
builder.Services.AddAuthorization(options => options.AddPolicy("User", policy => policy.AddRequirements(new RoleRequirement(Roles.User))));

builder.Services
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance);

builder.Services.AddDbContext<ReleaseManagerContext>(options => options
    .UseNpgsql(builder.Configuration.GetConnectionString("ReleaseManagerContext"))
    .UseSnakeCaseNamingConvention()
);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ReleaseManager", Version = "v1" });

});

// DI

builder.Services.AddScoped<IReleaseRepository, ReleaseRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAuthorizationHandler, RoleHandler>();
builder.Services.AddSingleton<IGitPilot, GitPilot>();
builder.Services.AddTransient<GitLogNotifyService, GitLogNotifyService>();

var app = builder.Build();

app.MapHub<GitLogHub>("/GitLog");

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReleaseManager v1"));
    app.UseCors(policy => policy
             .WithOrigins(new string[] { "http://localhost:3000" })
             //.AllowAnyOrigin()
             .AllowAnyHeader()
             .AllowAnyMethod()
             .AllowCredentials()
             .SetPreflightMaxAge(TimeSpan.FromHours(1))
    );
}

app.MapControllers();

app.Run();

