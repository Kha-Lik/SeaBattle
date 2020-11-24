using System.Collections.Generic;

namespace SeaBattle
{
    public class Ship
    {
        public IList<Cell> Cells { get; set; }
        public ShipState State { get; set; }

        public Ship(IList<Cell> cells)
        {
            Cells = cells;
            State = ShipState.Undamaged;
        }
    }
}