using System.Collections.Generic;
using System.Linq;

namespace SeaBattle
{
    public class SmartShot : AutoShotMethod
    {
        public SmartShot(IPlayer player) : base(player)
        {
        }

        public override bool Shoot()
        {
            var targetShip = Player.Battlefield.Ships.FirstOrDefault(s =>
                s.Cells.Count(c => c.State.HasFlag(CellState.WasFired)) >= 2 && s.State == ShipState.Damaged);
            var target = GetRandomCell(targetShip);

            target.State = target.State | CellState.WasFired;
            if (!target.State.HasFlag(CellState.Ship)) return false;

            if (!targetShip.Cells.All(c => c.State.HasFlag(CellState.WasFired))) return true;
            
            targetShip.State = ShipState.Destroyed;
            if (Player.Battlefield.Ships.Exists(s => s.State == ShipState.Damaged)) 
                ChangeShotMethod(new RandomNearShot(Player));
            
            ChangeShotMethod(new RandomShot(Player));
            return true;
        }

        private Cell GetRandomCell(Ship ship)
        {
            return RandomHelper.GetHelper().GetRandomCell(GetSuitableCells(ship));
        }

        private IList<Cell> GetSuitableCells(Ship ship)
        {
            List<Cell> cells = new();
            
            foreach (var cell in ship.Cells)
            {
                cells.AddRange(Player.Battlefield.GetNeighbours(cell).Where(c => !c.State.HasFlag(CellState.WasFired)));
            }

            cells = cells.Distinct().ToList();
            
            var a = ship.Cells.FirstOrDefault(c => c.State.HasFlag(CellState.WasFired));
            var b = ship.Cells.LastOrDefault(c => c.State.HasFlag(CellState.WasFired));
            
            cells = a.Coordinates.X == b.Coordinates.X 
                ? cells.Where(c => c.Coordinates.X == a.Coordinates.X).ToList() 
                : cells.Where(c => c.Coordinates.Y == a.Coordinates.Y).ToList();

            return cells;
        }
    }
}