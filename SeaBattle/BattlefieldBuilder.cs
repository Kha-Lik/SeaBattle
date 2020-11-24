using System.Collections.Generic;

namespace SeaBattle
{
    public abstract class BattlefieldBuilder
    {
        protected readonly GameSettings Settings;
        public BattlefieldBuilder(GameSettings settings)
        {
            Settings = settings;
        }

        public abstract void BuildBattlefield();

        public abstract void PlaceShips();

        public abstract IBattlefield GetResult();

        protected Dictionary<int, int> GetShipsFromSettings(GameSettings settings)
        {
            Dictionary<int, int> shipsSettings = new();
            foreach (var (size, count) in settings.Ships)
            {
                if (int.TryParse(size, out var result))
                    shipsSettings.Add(result, count);
            }

            return shipsSettings;
        }
    }
}