using System.Drawing;

namespace SeaBattle
{
    public class Cell
    {
        public Point Coordinates { get; set; }
        public CellState State { get; set; }

        public Cell(int coordX, int coordY)
        {
            Point point = new(coordX, coordY);
            Coordinates = point;
        }
    }
}