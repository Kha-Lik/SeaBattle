using System.Collections.Generic;

namespace SeaBattle
{
    public class Ship
    {
        public List<Cell> Cells { get; set; }
        public ShipState State { get; set; }
    }
}