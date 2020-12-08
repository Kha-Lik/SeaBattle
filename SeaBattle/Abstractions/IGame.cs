using System.Drawing;

namespace SeaBattle.Abstractions
{
    public interface IGame
    {
        IBattlefield GetPlayerOneField();

        IBattlefield GetPlayerTwoField();

        bool AutoShoot();

        bool ManualShoot(Point target);
    }
}