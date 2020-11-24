using System.Drawing;

namespace SeaBattle
{
    public interface IGame
    {
        IBattlefield GetPlayerOneField();
        
        IBattlefield GetPlayerTwoField();
        
        bool AutoShoot();
        
        bool ManualShoot(Point target);
        
    }
}