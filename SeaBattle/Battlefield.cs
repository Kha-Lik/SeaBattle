using System.Drawing;

namespace SeaBattle
{
    public class Battlefield
    {
        private Cell[,] _field;

        public Battlefield(int size)
        {
            _field = new Cell[size, size];

            Size = size;
        }

        public int Size { get; set; }

        public Cell this[Point point]
        {
            get => _field[point.X, point.Y];
            set => _field[point.X, point.Y] = value;
        }
    }
}