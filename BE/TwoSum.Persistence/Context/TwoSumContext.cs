using Microsoft.EntityFrameworkCore;
using TwoSum.Domain.Solutions;

namespace TwoSum.Persistence.Context;

public class TwoSumContext : DbContext
{
    public TwoSumContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Solution> Solutions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TwoSumContext).Assembly);
    }
}
