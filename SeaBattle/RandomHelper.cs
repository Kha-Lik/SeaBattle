using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace SeaBattle
{
    public class RandomHelper
    {
        private static RandomHelper _helper;
        private Random _random;

        private RandomHelper() => _random = new();

        public static RandomHelper GetHelper()
        {
            return _helper ??= new();
        }

        public Point GetRandomPoint(int range)
        {
            Point point = new()
            {
                X = _random.Next(range),
                Y = _random.Next(range)
            };
            
            return point;
        }

        public Cell GetRandomCell(IList<Cell> cells)
        {
            var index = _random.Next(cells.Count);
            return cells[index];
        }

        public bool GetRandomBool() => _random.Next(100) > 49;
    }
}