using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SeaBattle
{
    public class Battlefield : IBattlefield
    {
        private Cell[,] _field;

        public Battlefield(int size)
        {
            _field = new Cell[size, size];

            Size = size;
        }

        public int Size { get; set; }

        public List<Ship> Ships { get; set; }

        public Cell this[Point point]
        {
            get => _field[point.X, point.Y];
            set => _field[point.X, point.Y] = value;
        }

        public bool IsPointInField(Point point)
        {
            return (point.X >= 0 && point.Y >= 0) && (point.X < Size && point.Y < Size);
        }

        public ICollection<Cell> GetNeighbours(Cell cell)
        {
            var neighs = new List<Cell>();

            var startPoint = new Point {X = cell.Coordinates.X - 1, Y = cell.Coordinates.Y - 1};
            var neighPoints = new List<Point>();
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    var point = new Point {X = startPoint.X + i, Y = startPoint.Y + j};
                    if (IsPointInField(point) && point != cell.Coordinates)
                        neighPoints.Add(point);
                }
            }
            
            neighs.AddRange(neighPoints.Select(p => this[p]));
            return neighs;
        }

        public IEnumerator GetEnumerator()
        {
            return _field.GetEnumerator();
        }
    }
}