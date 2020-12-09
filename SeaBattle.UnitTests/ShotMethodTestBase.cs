using Moq;
using NUnit.Framework;
using SeaBattle.Abstractions;

namespace SeaBattle.UnitTests
{
    public class ShotMethodTestSetup
    {
        protected readonly Mock<IPlayer> Player = new();
        protected Mock<IBattlefield> Battlefield = new();
        
        [SetUp]
        public virtual void SetUp()
        {
            Player.Setup(p => p.IsMyTurn).Returns(true);
            Player.Setup(p => p.Battlefield).Returns(Battlefield.Object);
            Battlefield.Setup(b => b.Size).Returns(10);
        }
    }
}