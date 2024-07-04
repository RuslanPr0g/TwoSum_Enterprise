using TwoSum.Application.Contracts;
using TwoSum.Domain.Solution;
using TwoSum.Persistence.Context;

namespace TwoSum.Persistence.Repositories;

public sealed class SolutionRepository(TwoSumContext ctx) : ISolutionRepository
{
    private readonly TwoSumContext _ctx = ctx;

    public async Task<Solution?> GetSolutionById(SolutionId solutionId)
    {
        return await _ctx.Solutions.FindAsync(solutionId);
    }

    public async Task CreateSolution(Solution solution)
    {
        await _ctx.Solutions.AddAsync(solution);
    }

    public void DeleteSolution(Solution solution)
    {
        _ctx.Solutions.Remove(solution);
    }

    public async Task SaveChanges()
    {
        await _ctx.SaveChangesAsync();
    }
}
