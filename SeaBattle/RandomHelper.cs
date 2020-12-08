using System;
using System.Collections.Generic;
using System.Drawing;
using SeaBattle.Implementations;

namespace SeaBattle
{
    public class RandomHelper
    {
        private static RandomHelper _helper;
        private readonly Random _random;

        private RandomHelper()
        {
            _random = new Random();
        }

        public static RandomHelper GetHelper()
        {
            return _helper ??= new RandomHelper();
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

        public bool GetRandomBool()
        {
            return _random.Next(100) > 49;
        }
    }
}