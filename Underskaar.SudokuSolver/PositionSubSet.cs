using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Underskaar.SudokuSolver
{
    [DebuggerDisplay("{_positions.Count}")]
    public class PositionSubSet
    {
        private readonly ISet<Position> _positions = new HashSet<Position>();
        internal void Add(Position position)
        {
            _positions.Add(position);
        }

        public uint MaxValue => (uint)_positions.Count;
        public IEnumerable<uint> AvailableValues
        {
            get
            {
                return Enumerable.Range(1, (int)MaxValue)
                    .Select(v => (uint)v)
                    .Where(v => _positions.Select(p => p.Value).All(p => p != v));
            }
        }

        public bool InvalidDuplicates
        {
            get
            {
                return Enumerable.Range(1, (int)MaxValue)
                    .Select(v => (uint)v)
                    .Select(v => _positions.Count(p => p.Value == v))
                    .Any(c => c > 1);
            }
        }

        public override string ToString()
        {
            return string.Join("", _positions.Select(p => p.ToString()));
        }
    }
}