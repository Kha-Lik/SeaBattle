using System.Collections.Generic;
using SeaBattle.Enums;

namespace SeaBattle.Implementations
{
    public class Ship
    {
        public Ship(IList<Cell> cells)
        {
            Cells = cells;
            State = ShipState.Undamaged;
        }

        public IList<Cell> Cells { get; set; }
        public ShipState State { get; set; }
    }
}