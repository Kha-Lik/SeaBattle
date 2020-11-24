﻿using System.Drawing;

namespace SeaBattle
{
    public interface IPlayer
    {
        public IBattlefield Battlefield { get; set; }
        public AutoShotMethod AutoShotMethod { get; set; }
        public bool IsMyTurn { get; set; }

        public bool AutoShot();
        bool ManualShot(Point target, IBattlefield battlefield);
    }
}