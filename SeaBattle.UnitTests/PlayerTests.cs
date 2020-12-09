using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SeaBattle.Abstractions;
using SeaBattle.Enums;
using SeaBattle.Implementations;

namespace SeaBattle.UnitTests
{
    [TestFixture]
    public class PlayerTests
    {
        private IPlayer _player;
        private IBattlefield _battlefield;

        [SetUp]
        public void SetUp()
        {
            RandomBattlefieldBuilder builder = new(new GameSettings {
                FieldSize = 10,
                Ships = new() {{"4", 1}, {"3", 2}, {"2", 3}, {"1", 4}}
            });
            PlayerBuilder playerBuilder = new(builder);
            
            _player = playerBuilder.ConstructPlayer();
            builder.BuildBattlefield();
            builder.PlaceShips();
            _battlefield = builder.GetResult();
        }

        [Test]
        [TestCase(-1, -1)]
        [TestCase(10, 10)]
        public void ManualShot_PointOutOfRange_ThrowsException(int x, int y) =>
            _player.Invoking(p => p.ManualShot(new(x, y), _battlefield)).Should().Throw<IndexOutOfRangeException>()
                .And.Message.Should().ContainAll(x.ToString(), y.ToString());

        [Test]
        public void ManualShot_CellWasFired_ThrowsException()
        {
            const int x = 1;
            const int y = 1;
            _battlefield[new(x, y)].State = CellState.WasFired;
            
            _player.Invoking(p => p.ManualShot(new(x, y), _battlefield)).Should().Throw<ArgumentException>()
                .And.Message.Should().ContainAll(x.ToString(), y.ToString());
        }

        [Test]
        public void ManualShot_CellEmpty_ReturnFalseAndSetCellToFired()
        {
            const int x = 1;
            const int y = 1;
            _battlefield[new(x, y)].State = CellState.Empty;

            _player.ManualShot(new(x, y), _battlefield).Should().BeFalse();
            _battlefield[new(x, y)].State.Should().HaveFlag(CellState.WasFired);
        }
        
        [Test]
        public void ManualShot_CellHasShip_ReturnTrueAndSetCellToFired()
        {
            var x = _battlefield.Ships.First().Cells.First().Coordinates.X;
            var y = _battlefield.Ships.First().Cells.First().Coordinates.Y;

            _player.ManualShot(new(x, y), _battlefield).Should().BeTrue();
            _battlefield[new(x, y)].State.Should().HaveFlag(CellState.WasFired);
        }
    }
}