﻿using System.Linq;

namespace SeaBattle
{
    public class RandomShot : AutoShotMethod
    {
        public RandomShot(IPlayer player, IBattlefield battlefield) : base(player, battlefield)
        {
        }   

        public override bool Shoot()
        {
            var target = GetRandomCell();
            
            while (!IsCellSuitable(target))
                target = GetRandomCell();
            
            target.State = target.State | CellState.WasFired; //add WasFired flag to cell state
            
            if (!target.State.HasFlag(CellState.Ship)) return false;

            var ship = Battlefield.Ships.Find(s => s.Cells.Contains(target));
            // ReSharper disable once PossibleNullReferenceException
            if (ship.Cells.Count == 1)
            {
                ship.State = ShipState.Destroyed;
                return true;
            }
            
            ship.State = ShipState.Damaged;
            ChangeShotMethod(new RandomNearShot(Player, Battlefield));
            return true;

        }

        private bool IsCellSuitable(Cell cell)
        {
            return !cell.State.HasFlag(CellState.WasFired);
        }

        private Cell GetRandomCell()
        {
            var random = RandomHelper.GetHelper();
            return Battlefield[random.GetRandomPoint(Player.Battlefield.Size)];
        }
    }
}