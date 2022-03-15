using Microsoft.EntityFrameworkCore;
using ReleaseManager.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ReleaseManagerContext>(options => options
    .UseNpgsql(builder.Configuration.GetConnectionString("ReleaseManagerContext"))
    .UseSnakeCaseNamingConvention()
);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ReleaseManager", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReleaseManager v1"));
}

app.MapControllers();

app.Run();

