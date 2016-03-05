using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Underskaar.SudokuSolver
{
    public class PositionSet : IEnumerable<Position>
    {
        private readonly Position[] _positions;

        public PositionSet(uint[] values)
        {
            var count = values.Length;
            if (count != 81) throw new ArgumentException();
            var sets = Enumerable.Range(0, 3 * 9).Select(i => new PositionSubSet()).ToArray();
            _positions = new Position[count];
            Func<int, int> third = (i) => (int)Math.Floor((double)i / 3);
            for (int i = 0; i < count; i++)
            {
                var value = values[i];
                var x = i % 9;
                var y = (int)Math.Floor((double)i / 9);
                var z = third(x) + 3 * third(y);
                var position = new Position(
                    value,
                    sets[x],
                    sets[9 + y],
                    sets[2 * 9 + z]);
                _positions[i] = position;
            }
            if (sets.Any(s => s.InvalidDuplicates)) throw new ArgumentException("Invalid duplicates");
        }

        public bool Unsolvable => _positions.Any(p => p.Unsolvable);
        public bool HasSolution => _positions.All(p => p.Value > 0);

        public Position this[int index] => _positions[index];

        public PositionSet Clone(int index, uint newValue)
        {
            var values = _positions.Select(p => p.Value).ToArray();
            values[index] = newValue;
            return new PositionSet(values);
        }

        public IEnumerator<Position> GetEnumerator()
        {
            return _positions.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _positions.GetEnumerator();
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    result.Append(_positions[9 * i + j]);
                }
                result.AppendLine();
            }
            return result.ToString();
        }
    }
}