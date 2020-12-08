using System;
using System.Collections.Generic;

namespace SeaBattle
{
    public abstract class BattlefieldBuilder
    {
        protected readonly GameSettings Settings;

        protected BattlefieldBuilder(GameSettings settings) => Settings = settings;

        public abstract void BuildBattlefield();

        public abstract void PlaceShips();

        public abstract IBattlefield GetResult();

        protected Dictionary<int, int> GetShipsFromSettings( )
        {
            Dictionary<int, int> shipsSettings = new();
            foreach (var (size, count) in Settings.Ships)
            {
                if (int.TryParse(size, out var result))
                    shipsSettings.Add(result, count);
                else
                    throw new BattlefieldBuilderException($"Incorrect ship size string: {size}");
            }

            return shipsSettings;
        }
    }
}