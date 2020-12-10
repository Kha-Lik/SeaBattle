using System.Linq;
using SeaBattle.Abstractions;
using SeaBattle.Enums;

namespace SeaBattle.Implementations
{
    public class RandomNearShot : AutoShotMethod
    {
        public RandomNearShot(IPlayer player) : base(player)
        {
        }

        public override bool Shoot(IBattlefield battlefield)
        {
            var targetShip = battlefield.Ships.FirstOrDefault(s => s.State is ShipState.Damaged);
            //ReSharper disable once PossibleNullReferenceException
            var damagedCell = targetShip.Cells.FirstOrDefault(c => c.State.HasFlag(CellState.WasFired));

            var target = GetRandomCell(damagedCell, battlefield);
            target.State = target.State | CellState.WasFired;

            if (!target.State.HasFlag(CellState.Ship)) return false;

            if (targetShip.Cells.All(c => c.State.HasFlag(CellState.WasFired)))
            {
                targetShip.State = ShipState.Destroyed;
                if (battlefield.Ships.Exists(s => s.State == ShipState.Damaged))
                    return true;
                ChangeShotMethod(new RandomShot(Player));
                return true;
            }

            ChangeShotMethod(new SmartShot(Player));
            return true;
        }

        private Cell GetRandomCell(Cell damaged, IBattlefield battlefield)
        {
            var cells = battlefield.GetNeighbours(damaged).Where(c => !c.State.HasFlag(CellState.WasFired) 
                                                                      && (c.Coordinates.X.Equals(damaged.Coordinates.X ) 
                                                                          || c.Coordinates.Y.Equals(damaged.Coordinates.Y ))).ToList();
            return RandomHelper.GetHelper().GetRandomCell(cells);
        }
    }
}