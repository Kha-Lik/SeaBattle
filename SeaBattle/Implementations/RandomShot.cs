using SeaBattle.Abstractions;
using SeaBattle.Enums;

namespace SeaBattle.Implementations
{
    public class RandomShot : AutoShotMethod
    {
        public RandomShot(IPlayer player) : base(player)
        {
        }

        public override bool Shoot(IBattlefield battlefield)
        {
            var target = battlefield.GetRandomCell();

            while (!IsCellSuitable(target))
                target = battlefield.GetRandomCell();

            target.State = target.State | CellState.WasFired; //adds WasFired flag to cell state

            if (!target.State.HasFlag(CellState.Ship)) return false;

            var ship = battlefield.Ships.Find(s => s.Cells.Contains(target));
            // ReSharper disable once PossibleNullReferenceException
            if (ship.Cells.Count == 1)
            {
                ship.State = ShipState.Destroyed;
                return true;
            }

            ship.State = ShipState.Damaged;
            ChangeShotMethod(new RandomNearShot(Player));
            return true;
        }

        private bool IsCellSuitable(Cell cell)
        {
            return !cell.State.HasFlag(CellState.WasFired);
        }
    }
}