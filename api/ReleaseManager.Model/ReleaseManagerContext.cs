using Microsoft.EntityFrameworkCore;
using ReleaseManager.Model.Models;

namespace ReleaseManager.Model;

public class ReleaseManagerContext : DbContext
{
    public ReleaseManagerContext(DbContextOptions<ReleaseManagerContext> options) : base(options) { }

    public DbSet<Release> Releases => Set<Release>();
}