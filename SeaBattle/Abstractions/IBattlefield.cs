using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using SeaBattle.Implementations;

namespace SeaBattle.Abstractions
{
    public interface IBattlefield : IEnumerable
    {
        int Size { get; set; }
        public List<Ship> Ships { get; set; }
        Cell this[Point point] { get; set; }
        bool IsPointInField(Point point);
        ICollection<Cell> GetNeighbours(Cell cell);
    }
}