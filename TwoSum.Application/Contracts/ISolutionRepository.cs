using Enterprise.Persistence;
using TwoSum.Domain.Solution;

namespace TwoSum.Application.Contracts;

public interface ISolutionRepository : IRepository
{
    Task<Solution?> GetSolutionById(SolutionId solutionId);
    Task CreateSolution(Solution solution);
    void DeleteSolution(Solution solution);

    Task SaveChanges();
}
