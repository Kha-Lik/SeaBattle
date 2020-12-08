using System.Drawing;
using SeaBattle.Enums;

namespace SeaBattle.Implementations
{
    public class Cell
    {
        public Cell(Point point)
        {
            Coordinates = point;
            State = CellState.Empty;
        }

        public Point Coordinates { get; set; }
        public CellState State { get; set; }
    }
}