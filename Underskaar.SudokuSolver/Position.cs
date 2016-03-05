using System.Diagnostics;
using System.Linq;

namespace Underskaar.SudokuSolver
{
    [DebuggerDisplay("{Visualize()}")]
    public class Position
    {
        private readonly PositionSubSet[] _subSets;
        public Position(uint value, params PositionSubSet[] subSets)
        {
            _subSets = subSets;
            Value = value;
            foreach (var positionSet in _subSets)
            {
                positionSet.Add(this);
            }
        }

        private uint[] GetPossibleValues()
        {
            return Enumerable.Range(1, (int) _subSets.Max(s => s.MaxValue))
                .Select(v => (uint) v)
                .Where(v => _subSets.All(s=>s.AvailableValues.Any(a=>a==v)))
                .ToArray();
        }

        private string Visualize()
        {
            if (Value > 0) return Value.ToString();
            var possibleValues = PossibleValues;
            if (!possibleValues.Any()) return "X";
            return string.Join("|", possibleValues);
        }

        public uint[] PossibleValues => Value < 1 ? GetPossibleValues() : new uint[] {};
        public bool Unsolvable => Value < 1 && PossibleValues.Length == 0;
        public uint Value { get; private set; }

        public override string ToString()
        {
            if (Unsolvable) return "X";
            return string.Format("{0}", Value > 0 ? Value.ToString() : " ");
        }
        
    }
}