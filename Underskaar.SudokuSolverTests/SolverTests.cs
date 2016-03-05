using System.Linq;
using NUnit.Framework;
using Underskaar.SudokuSolver;

namespace Underskaar.SudokuSolverTests
{
    public class SolverTests
    {
        [Test]
        public void Solve_WithKnownSet_ReturnsSolution()
        {
            var set = new uint[]
            {
                0, 0, 2, 0, 3, 0, 1, 5, 0,
                0, 0, 0, 0, 0, 2, 0, 0, 6,
                0, 0, 4, 1, 9, 5, 0, 0, 3,
                0, 2, 7, 0, 0, 4, 0, 0, 0,
                0, 0, 5, 0, 6, 0, 8, 0, 0,
                0, 0, 0, 2, 0, 0, 5, 7, 0,
                4, 0, 0, 8, 2, 6, 3, 0, 0,
                2, 0, 0, 5, 0, 0, 0, 0, 0,
                0, 3, 1, 0, 4, 0, 0, 0, 0
            };

            var result = Solver.Solve(set).ToList();

            Assert.That(result, Is.Not.Empty);

        }
    }
}