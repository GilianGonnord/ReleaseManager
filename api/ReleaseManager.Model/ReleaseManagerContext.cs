using Microsoft.EntityFrameworkCore;
using ReleaseManager.Model.Models;

namespace ReleaseManager.Model;

public class ReleaseManagerContext : DbContext
{
    public ReleaseManagerContext(DbContextOptions<ReleaseManagerContext> options) : base(options) { }

    public DbSet<App> Apps => Set<App>();
    private DbSet<Config> Configs => Set<Config>();
    //private DbSet<GithubCredential> GithubCredentials => Set<GithubCredential>();
    //private DbSet<GithubCredential> GithubCredentials => Set<GithubCredential>();
    public DbSet<Release> Releases => Set<Release>();
    public DbSet<User> Users => Set<User>();

    public Config Config => Configs.First();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Config>()
            .HasData(new Config { Id = 1, GitExePath = @"C:\Program Files\Git\bin\git.exe", RootProjectRepository = @"C:\ReleaseManager" });

        modelBuilder
            .Entity<Release>()
            .Property(r => r.DateCreated)
            .HasDefaultValueSql("now()");
    }
}