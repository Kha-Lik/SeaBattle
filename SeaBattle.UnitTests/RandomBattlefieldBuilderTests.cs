using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SeaBattle.Abstractions;
using SeaBattle.Enums;
using SeaBattle.Implementations;

namespace SeaBattle.UnitTests
{
    [TestFixture]
    public class RandomBattlefieldBuilderTests
    {
        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(int.MinValue)]
        public void BuildBattlefield_SizeIsLessOrEqualToZero_ThrowsException(int size)
        {
            var builder = new RandomBattlefieldBuilder(new GameSettings {FieldSize = size});
            
            builder.Invoking(b => b.BuildBattlefield()).Should().Throw<BattlefieldBuilderException>()
                .And.Message.Should().Contain($"{size}");
        }

        [Test]
        public void BuildBattlefield_SizeIsGreaterThanZero_FieldFilledWithDefaultCells()
        {
            var builder = new RandomBattlefieldBuilder(new GameSettings
            {
                FieldSize = 10
            });

            builder.BuildBattlefield();
            var field = builder.GetResult();
            
            AssertCellsAreDefault(field);
        }

        [Test]
        public void PlaceShips_CalledBeforeBuildBattlefield_ThrowsException()
        {
            var builder = new RandomBattlefieldBuilder(new GameSettings());

            builder.Invoking(b => b.PlaceShips()).Should().Throw<BattlefieldBuilderException>()
                .And.ParamName.Should().Be("_battlefield");
        }

        [Test]
        [TestCase("string")]
        [TestCase("")]
        public void PlaceShips_IncorrectShipSizeStringFormat_ThrowsException(string size)
        {
            var settings = new GameSettings { FieldSize = 1,Ships = new() { {size, 1} }};
            var builder = new RandomBattlefieldBuilder(settings);
            
            builder.BuildBattlefield();

            builder.Invoking(b => b.PlaceShips()).Should().Throw<BattlefieldBuilderException>()
                .And.Message.Should().ContainAll("size", size);
        }

        [Test]
        public void PlaceShips_TooHighShipsDensity_ThrowsException()
        {
            var settings = new GameSettings
            {
                FieldSize = 5,
                Ships = new() {{"4", 4 }}
            };
            
            var builder = new RandomBattlefieldBuilder(settings);
            builder.BuildBattlefield();

            var ex = Assert.Throws<BattlefieldBuilderException>(() => builder.PlaceShips());
            Assert.That(ex.Message, Does.Contain("density").IgnoreCase);
        }
        
        
        [Test]
        [Ignore("Strange bug, test runs only on clean solution")]
        public void PlaceShips_WhenCalled_PutsShipsOnField()
        {
            var settings = new GameSettings {
                FieldSize = 10,
                Ships = new() {{"4", 1}, {"3", 2}, {"2", 3}, {"1", 4}}
            };
            var builder = new RandomBattlefieldBuilder(settings);
            
            builder.BuildBattlefield();
            builder.PlaceShips();
            var battlefield = builder.GetResult();

            Assert.That(battlefield.Ships.Count, Is.EqualTo(10));
            Assert.That(battlefield.Ships.All(s => s.Cells.All(c => c.State == CellState.Ship)));
        }

        [Test]
        [Ignore("Strange bug, test runs only on clean solution")]
        public void GetResult_WhenCalled_IsReturnedObjectTypeOfBattlefield()
        {
            var settings = new GameSettings {
                FieldSize = 10,
                Ships = new() {{"4", 1}, {"3", 2}, {"2", 3}, {"1", 4}}
            };
            var builder = new RandomBattlefieldBuilder(settings);
            
            builder.BuildBattlefield();
            builder.PlaceShips();

            builder.GetResult().Should().BeAssignableTo<IBattlefield>();
        }

        private void AssertCellsAreDefault(IBattlefield battlefield)
        {
            foreach (Cell cell in battlefield)
            {
                Assert.That(cell.State, Is.EqualTo(CellState.Empty));
            }
        }
    }
}