using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TwoSum.Domain.Solution;

namespace TwoSum.Persistence.Configurations;

internal class SolutionConfiguration : IEntityTypeConfiguration<Solution>, IEntityTypeConfiguration<SolutionIteration>
{
    public void Configure(EntityTypeBuilder<Solution> builder)
    {
        var solutionIdConverter = new ValueConverter<SolutionId, Guid>(
            v => v.Value,
            v => new SolutionId(v));

        builder.ToTable("Solutions");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(
                v => v.Value,
                v => new SolutionId(v));

        builder.HasMany(s => s.Iterations)
            .WithOne(s => s.Solution)
            .HasForeignKey(s => s.SolutionId)
            .HasPrincipalKey(s => s.Id);

        builder.Property(b => b.Nums).IsRequired();
        builder.Property(b => b.Target).IsRequired();
        builder.Property(b => b.Status)
            .HasConversion(
                v => v.Value,
                v => new SolutionStatus(v))
            .IsRequired();
    }

    public void Configure(EntityTypeBuilder<SolutionIteration> builder)
    {
        var solutionIdConverter = new ValueConverter<SolutionId, Guid>(
            v => v.Value,
            v => new SolutionId(v));

        builder.ToTable("SolutionIterations");
        builder.HasKey(s => s.Id);
        builder.Property(si => si.Id)
            .HasConversion(new ValueConverter<SolutionIterationId, Guid>(
                v => v.Value,
                v => new SolutionIterationId(v)));
        builder.Property(si => si.SolutionId)
            .HasConversion(solutionIdConverter);
        builder.HasIndex(s => new { s.SolutionId, s.Index }).IsUnique();
        builder.Property(b => b.Result).IsRequired(false);
        builder.Property(b => b.Status)
            .HasConversion(
                v => v.Value,
                v => new SolutionIterationStatus(v))
            .IsRequired();
    }
}
