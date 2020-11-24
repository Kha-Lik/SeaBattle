using System;
using System.Drawing;
using System.Linq;

namespace SeaBattle
{
    public class Player : IPlayer
    {
        public IBattlefield Battlefield { get; set; }
        public AutoShotMethod AutoShotMethod { get; set; }
        public bool IsMyTurn { get; set; }
        
        public bool AutoShot()
        {
            return AutoShotMethod.Shoot();
        }

        public bool ManualShot(Point target, IBattlefield battlefield)
        {
            if (!battlefield.IsPointInField(target))
                throw new IndexOutOfRangeException($"Point {target.X}, {target.Y} is not in field");
            
            var cell = battlefield[target];
            if (cell.State.HasFlag(CellState.WasFired))
                throw new ArgumentException($"Can't shoot fired cell: {target.X}, {target.Y}");
            cell.State = battlefield[target].State | CellState.WasFired;

            if (!cell.State.HasFlag(CellState.Ship)) return false;
            var ship = battlefield.Ships.Find(s => s.Cells.Contains(cell));
            ship.State = ship.Cells.All(c => 
                c.State.HasFlag(CellState.WasFired)) ? ShipState.Destroyed : ShipState.Damaged;
            return true;
        }
    }
}