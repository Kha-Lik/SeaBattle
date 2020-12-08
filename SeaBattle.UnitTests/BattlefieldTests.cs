using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace SeaBattle.UnitTests
{
    [TestFixture]
    public class BattlefieldTests
    {
        private Battlefield _battlefield;

        [SetUp]
        public void SetUp()
        {
            _battlefield = new Battlefield(3);
            
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    Point point = new(j, i);

                    _battlefield[point] = new Cell(point);
                }
            }
        }
        
        [Test]
        [TestCase(-1, -1, false)]
        [TestCase(0, 1, true)]
        [TestCase(3, 3, false)]
        public void IsPointInField_WhenCall_TrueIfPointInFieldOtherwiseFalse(int x, int y, bool expectedResult)
        {
            var point = new Point {X = x, Y = y};

            _battlefield.IsPointInField(point).Should().Be(expectedResult);
        }

        [Test]
        public void GetNeighbours_CellIsAtCorner_ThreeNeighs()
        {
            var cell = new Cell(new (){X = 0, Y = 0});

            _battlefield.GetNeighbours(cell).Count.Should().Be(3);
        }
        
        [Test]
        public void GetNeighbours_CellIsInMiddle_EightNeighs()
        {
            var cell = new Cell(new (){X = 1, Y = 1});

            _battlefield.GetNeighbours(cell).Count.Should().Be(8);
        }
    }
}