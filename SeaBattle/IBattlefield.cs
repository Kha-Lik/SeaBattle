using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace SeaBattle
{
    public interface IBattlefield : IEnumerable
    {
        int Size { get; set; }
        Cell this[Point point] { get; set; }
        bool IsPointInField(Point point);
        ICollection<Cell> GetNeighbours(Cell cell);
    }
}