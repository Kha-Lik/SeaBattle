using System.Collections.Generic;

namespace SeaBattle
{
    public class GameSettings
    {
        public int FieldSize { get; set; }
        
        //size, count
        public Dictionary<string, int> Ships { get; set; }
    }
}