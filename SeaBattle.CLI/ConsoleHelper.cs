using System;
using System.Drawing;
using System.Linq;

namespace SeaBattle.CLI
{
    public static class ConsoleHelper
    {
        public static int GetIntAnswer()
        {
            string answer;
            int dimension;
            do
            {
                answer = Console.ReadLine();
            }
            while (!int.TryParse(answer, out dimension));

            return dimension;
        }

        public static Point GetPointAnswer()
        {
            string[] numbers;

            do
            {
                Console.WriteLine("Write two numbers separated by space: " +
                                  "coordinates of the point to open:");

                numbers = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            }
            while (numbers.Length != 2 || numbers.Any(x => !int.TryParse(x, out _)));

            var result = numbers.Select(x => Convert.ToInt32(x)).ToArray();

            return new Point(result[0], result[1]);
        }
    }
}