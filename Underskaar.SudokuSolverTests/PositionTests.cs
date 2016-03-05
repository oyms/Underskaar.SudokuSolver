using System;
using NUnit.Framework;
using Underskaar.SudokuSolver;

namespace Underskaar.SudokuSolverTests
{
    public class PositionTests
    {
        [Test]
        public void Ctor_WhenNonZero_ReturnsSameValue()
        {
            const int value = 4;
            var target = new Position(value);

            Assert.That(target.Value, Is.EqualTo(value));
            Assert.That(target.Unsolvable, Is.False);
            Assert.That(target.PossibleValues, Is.Empty);
            Assert.That(target.ToString(), Is.EqualTo("4"));
        }

    }

    public class PositionSetTests
    {
        [Test]
        public void AvailableValues_WithOneValueOfTwo_ReturnsAvailable()
        {
            var set0 = new PositionSubSet();
            var set1 = new PositionSubSet();
            var set2 = new PositionSubSet();
            var position0 = new Position(0, set0, set1, set2);
            var position1 = new Position(2, set0, set1, set2);
            var position2 = new Position(0, set0, set1, set2);

            var expected = new[] {1,3};
            Assert.That(set0.AvailableValues, Is.EquivalentTo(expected));
            Assert.That(position0.PossibleValues, Is.EquivalentTo(expected));
        }
    }
}
