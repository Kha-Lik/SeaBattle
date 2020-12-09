using System.Collections.Generic;

namespace SeaBattle
{
    public class GameSettings
    {
        public string FirstPlayerName { get; set; }
        public string SecondPlayerName { get; set; }
        public int FieldSize { get; set; }

        //size, count
        public Dictionary<string, int> Ships { get; set; }
    }
}