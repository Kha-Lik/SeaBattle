using System.Drawing;

namespace SeaBattle.Abstractions
{
    public interface IPlayer
    {
        public IBattlefield Battlefield { get; set; }
        public AutoShotMethod AutoShotMethod { get; set; }
        public bool IsMyTurn { get; set; }

        public bool AutoShot(IBattlefield battlefield);
        bool ManualShot(Point target, IBattlefield battlefield);
    }
}