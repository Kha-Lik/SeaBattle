using System;

namespace SeaBattle.Enums
{
    [Flags]
    public enum CellState
    {
        Empty = 1,
        Ship = 2,
        WasFired = 4,
        NearShip = 8
    }
}