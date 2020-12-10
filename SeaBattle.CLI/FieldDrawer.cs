using System;
using SeaBattle.Abstractions;
using SeaBattle.Enums;
using SeaBattle.Implementations;

namespace SeaBattle.CLI
{
    public class FieldDrawer
    {
        public void DrawField(IBattlefield battlefield)
        {
            var size = battlefield.Size;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var cell = battlefield[new(j, i)];
                    if (cell.State.HasFlag(CellState.WasFired) && cell.State.HasFlag(CellState.Ship)) Console.Write("X ");
                    else if (cell.State.HasFlag(CellState.WasFired)) Console.Write("* ");
                    else Console.Write("# ");
                }

                Console.WriteLine();
            }
            
        }

        public void DrawLegend()
        {
            Console.WriteLine("Legend:" +
                              "\n\t# - not revealed cell" +
                              "\n\t* - empty shot cell" +
                              "\n\tX - damaged ship" +
                              "\n");
        }
    }
}