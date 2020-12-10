using System;
using System.Collections.Generic;
using SeaBattle.Abstractions;

namespace SeaBattle.CLI
{
    public class GameSetUpView
    {
        public GameSettings Run()
        {
            Console.WriteLine("Hello! Welcome to SeaBattle!\nPlease, enter some settings:");
            var settings = new GameSettings();

            Console.WriteLine("First player name:");
            settings.FirstPlayerName = Console.ReadLine();
            Console.WriteLine("Second player name:");
            settings.SecondPlayerName = Console.ReadLine();

            var tmp = AskForSettings();
            settings.Ships = tmp.Ships;
            settings.FieldSize = tmp.FieldSize;

            return settings;
        }

        private GameSettings AskForSettings()
        {
            Dictionary<string, int> ships = new();
            int size = 0;

            var isAnswered = false;
            while (!isAnswered)
            {
                Console.WriteLine("Do you like to set custom filed size and ships amount? [y/n]:");
                var answer = Console.ReadLine().ToLower();
                switch (answer)
                {
                    case "y":
                        Console.WriteLine("Enter field size:");
                        size = ConsoleHelper.GetIntAnswer();
                        ships = ReadShips();
                        isAnswered = true;
                        break;
                    case "n":
                        ships = new()
                        {
                            {"4", 1},
                            {"3", 2},
                            {"2", 3},
                            {"1", 4}
                        };
                        size = 10;
                        isAnswered = true;
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        break;
                }
            }

            return new() {FieldSize = size, Ships = ships};
        }

        private Dictionary<string, int> ReadShips()
        {
            var isSetUp = false;
            var ships = new Dictionary<string, int>();
            while (!isSetUp)
            {
                Console.WriteLine("[A]dd ships or [e]xit?");
                var answer = Console.ReadLine()?.ToLower();
                if (answer != null && answer.Equals("a"))
                {
                    Console.WriteLine("Enter setting in format {ship size}:{amount of ships}");
                    var setting = Console.ReadLine();
                    var size = setting.Substring(0, 1);
                    var amount = 0;
                    int.TryParse(setting.Substring(2, 1), out amount);
                    ships.Add(size, amount);
                }
                else if (answer != null && answer.Equals("e"))
                {
                    isSetUp = true;
                }
                else
                {
                    Console.WriteLine("Wrong input");
                }
            }

            return ships;
        }
    }
}