using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Common;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using SeaBattle.Abstractions;
using SeaBattle.Enums;
using SeaBattle.Implementations;

namespace SeaBattle.UnitTests
{
    [TestFixture]
    public class RandomShotTests : ShotMethodTestSetup
    {
        private RandomShot _randomShot;
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _randomShot = new(Player.Object);
        }

        [Test]
        public void Shoot_CellIsEmpty_ReturnFalse()
        {
            Battlefield.Setup(b => b.GetRandomCell())
                .Returns(new Cell(new(0, 0)) {State = CellState.Empty});

            _randomShot.Shoot(Battlefield.Object).Should().BeFalse();
        }

        [Test]
        public void Shoot_CellHasShip_ReturnsTrue()
        {
            var cell = new Cell(new(0, 0)) {State = CellState.Ship};
            Battlefield.Setup(b => b.GetRandomCell())
                .Returns(cell);
            Battlefield.Setup(b => b.Ships)
                .Returns(new List<Ship>() {new Ship(new List<Cell> {cell})});
            

            _randomShot.Shoot(Battlefield.Object).Should().BeTrue();
        }

        [Test]
        public void Shoot_CellHasShipWithOneDeck_SwitchShipStateToDestroyed()
        {
            var cell = new Cell(new(0, 0)) {State = CellState.Ship};
            var ship = new Ship(new List<Cell> {cell});
            Battlefield.Setup(b => b.GetRandomCell()).Returns(cell);
            Battlefield.Setup(b => b.Ships).Returns(new List<Ship>() {ship});

            _randomShot.Shoot(Battlefield.Object);
            ship.State.Should().Be(ShipState.Destroyed);
        }
        
        [Test]
        public void Shoot_CellHasShipWithTwoOrMoreDecks_SwitchShipStateToDamaged()
        {
            var cell = new Cell(new(0, 0)) {State = CellState.Ship};
            var ship = new Ship(new List<Cell> {cell, new Cell(new(1, 0))});
            Battlefield.Setup(b => b.GetRandomCell()).Returns(cell);
            Battlefield.Setup(b => b.Ships).Returns(new List<Ship>() {ship});

            _randomShot.Shoot(Battlefield.Object);
            ship.State.Should().Be(ShipState.Damaged);
        }

        [Test]
        public void Shoot_WhenShipShot_SwitchShootMethodToRandomNearShot()
        {
            var cell = new Cell(new(0, 0)) {State = CellState.Ship};
            var ship = new Ship(new List<Cell> {cell, new Cell(new(1, 0))});
            Battlefield.Setup(b => b.GetRandomCell()).Returns(cell);
            Battlefield.Setup(b => b.Ships).Returns(new List<Ship>() {ship});
            Player.SetupProperty(p => p.AutoShotMethod);

            _randomShot.Shoot(Battlefield.Object);
            Player.Object.AutoShotMethod.Should().BeOfType<RandomNearShot>();
        }
        
    }
}