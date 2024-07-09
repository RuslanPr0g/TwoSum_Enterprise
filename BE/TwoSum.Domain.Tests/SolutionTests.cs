using FluentAssertions;
using TwoSum.Domain.Events;
using TwoSum.Domain.Solutions;

namespace TwoSum.Domain.Tests
{
    public class SolutionTests
    {
        [Fact]
        public void Create_ShouldInitializeCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var nums = new[] { 2, 7, 11, 15 };
            var target = 9;

            // Act
            var solution = Solution.Create(id, nums, target);

            // Assert
            solution.Id.Value.Should().Be(id);
            solution.Nums.Should().BeEquivalentTo(nums);
            solution.Target.Should().Be(target);
            solution.Status.Value.Should().Be(SolutionStatus.SolutionStatusEnum.Started);
            solution.Iterations.Should().BeEmpty();
            solution.DomainEvents.Should().ContainSingle(e => e.GetType() == typeof(SolutionCreatedDomainEvent));
        }

        [Fact]
        public void MoveNext_ShouldMoveToInProgressIfStarted()
        {
            // Arrange
            var id = Guid.NewGuid();
            var nums = new[] { 2, 7, 11, 15 };
            var target = 18;
            var solution = Solution.Create(id, nums, target);

            // Act
            solution.MoveNext();

            // Assert
            solution.Status.Value.Should().Be(SolutionStatus.SolutionStatusEnum.InProgress);
            solution.Iterations.Should().HaveCount(1);
            solution.Iterations[0].Status.Value.Should().Be(SolutionIterationStatus.SolutionIterationStatusEnum.Processing);
        }

        [Fact]
        public void MoveNext_ShouldSolveIfNoNums()
        {
            // Arrange
            var id = Guid.NewGuid();
            var nums = Array.Empty<int>();
            var target = 9;
            var solution = Solution.Create(id, nums, target);

            // Act
            solution.MoveNext();

            // Assert
            solution.Status.Value.Should().Be(SolutionStatus.SolutionStatusEnum.Solved);
            solution.Iterations.Should().BeEmpty();
        }

        [Fact]
        public void MoveNext_ShouldSolveIfSolutionFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var nums = new[] { 2, 7, 11, 15 };
            var target = 9;
            var solution = Solution.Create(id, nums, target);

            // Act
            solution.MoveNext(); // Process 2,7
            solution.MoveNext(); // Solve as 2,7 are the solution

            // Assert
            solution.Status.Value.Should().Be(SolutionStatus.SolutionStatusEnum.Solved);
            solution.Iterations.Should().ContainSingle(i => i.Status.Value == SolutionIterationStatus.SolutionIterationStatusEnum.Finished);
        }

        [Fact]
        public void RetrieveSolutionAsString_ShouldReturnSolutionIfFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var nums = new[] { 2, 7, 11, 15 };
            var target = 9;
            var solution = Solution.Create(id, nums, target);

            // Act
            solution.MoveNext(); // Process 2,7
            solution.MoveNext(); // Solve as 2,7 are the solution
            var result = solution.RetrieveSolutionAsString();

            // Assert
            result.Should().Contain("I: 1, J: 0, nums: 2,7,11,15");
        }

        [Fact]
        public void RetrieveSolutionAsString_ShouldReturnMessageIfNoSolution()
        {
            // Arrange
            var id = Guid.NewGuid();
            var nums = new[] { 1, 2, 3, 4 };
            var target = 10;
            var solution = Solution.Create(id, nums, target);

            // Act
            while (!solution.IsSolved())
            {
                solution.MoveNext();
            }
            var result = solution.RetrieveSolutionAsString();

            // Assert
            result.Should().Contain("No solution found.");
        }

        [Fact]
        public void RetrieveSolutionAsString_ShouldReturnMessageIfNotSolved()
        {
            // Arrange
            var id = Guid.NewGuid();
            var nums = new[] { 2, 7, 11, 15 };
            var target = 18;
            var solution = Solution.Create(id, nums, target);

            // Act
            solution.MoveNext();
            var result = solution.RetrieveSolutionAsString();

            // Assert
            result.Should().Contain("No solution yet.");
        }
    }
}
