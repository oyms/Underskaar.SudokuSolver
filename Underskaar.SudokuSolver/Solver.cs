using System.Collections.Generic;
using System.Linq;

namespace Underskaar.SudokuSolver
{
    public static class Solver
    {
        public static IEnumerable<uint[]> Solve(params uint[] values)
        {
            var positions = new PositionSet(values);
            return GetSolutions(positions).Select(positionse => positionse.Select(p => p.Value).ToArray());
        }

        private static IEnumerable<PositionSet> GetSolutions(PositionSet positions)
        {
            if (positions.Unsolvable) yield break;
            if (positions.HasSolution) yield return positions;
            else
            {
                var index = GetIndexOfBestCandidate(positions);
                foreach (var possible in positions[index].PossibleValues)
                {
                    var newArray = positions.Clone(index, possible);
                    foreach (var solution in GetSolutions(newArray))
                    {
                        yield return solution;
                    }
                }
            }
        }

        private static int GetIndexOfBestCandidate(PositionSet positions)
        {
            return positions.Select((p, i) => new {index = i, possible = p.PossibleValues.Length})
                .Where(p => p.possible > 0)
                .OrderBy(p => p.possible)
                .Select(p => p.index)
                .First();
        }
    }
}
