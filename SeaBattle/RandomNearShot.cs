using System.Linq;

namespace SeaBattle
{
    public class RandomNearShot : AutoShotMethod
    {
        public RandomNearShot(IPlayer player, IBattlefield battlefield) : base(player, battlefield)
        {
        }

        public override bool Shoot()
        {
            var targetShip = Battlefield.Ships.FirstOrDefault(s => s.State is ShipState.Damaged);
            //ReSharper disable once PossibleNullReferenceException
            var damagedCell = targetShip.Cells.FirstOrDefault(c => c.State.HasFlag(CellState.WasFired));

            var target = GetRandomCell(damagedCell);
            target.State = target.State | CellState.WasFired;
            
            if (!target.State.HasFlag(CellState.Ship)) return false;

            if (targetShip.Cells.All(c => c.State.HasFlag(CellState.WasFired)))
            {
                targetShip.State = ShipState.Destroyed;
                if (Battlefield.Ships.Exists(s => s.State == ShipState.Damaged)) 
                    return true;
                ChangeShotMethod(new RandomShot(Player, Battlefield));
                return true;

            }
            ChangeShotMethod(new SmartShot(Player, Battlefield));
            return true;
        }

        private Cell GetRandomCell(Cell damaged)
        {
            var cells = Battlefield.GetNeighbours(damaged).Where(c => !c.State.HasFlag(CellState.WasFired)).ToList();
            return RandomHelper.GetHelper().GetRandomCell(cells);
        }
    }
}