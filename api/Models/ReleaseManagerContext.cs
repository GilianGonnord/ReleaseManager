using Microsoft.EntityFrameworkCore;

namespace ReleaseManager.Models;

public class ReleaseManagerContext : DbContext
{
    public ReleaseManagerContext(DbContextOptions<ReleaseManagerContext> options) : base(options) { }

    public DbSet<Release> Releases => Set<Release>();
}