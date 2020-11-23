using System.Drawing;

namespace SeaBattle
{
    public class Cell
    {
        public Point Coordinates { get; set; }
        public CellState State { get; set; }

        public Cell(Point point)
        {
            Coordinates = point;
            State = CellState.Empty;
        }
    }
}