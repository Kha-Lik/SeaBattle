using System;
using SeaBattle.Abstractions;

namespace SeaBattle.CLI
{
    public class GameView
    {
        private readonly IGame _game;
        private readonly FieldDrawer _drawer;
        
        private const string Menu = "Actions:\n" +
                                    "\n\t0 - exit" +
                                    "\n\t1 - make move" +
                                    "\n\t2 - make auto move" +
                                    "\n";

        public GameView(IGame game)
        {
            _game = game;
            _drawer = new();
        }

        public void Run()
        {
            var isGameEnded = false;
            while (!isGameEnded)
            {
                Console.Clear();
                Console.WriteLine("First player field:");
                _drawer.DrawField(_game.GetPlayerOneField());
                Console.WriteLine("Second player field:");
                _drawer.DrawField(_game.GetPlayerTwoField());
                _drawer.DrawLegend();
                Console.WriteLine(Menu);

                var answer = ConsoleHelper.GetIntAnswer();
                switch (answer)
                {
                    case 0:
                        isGameEnded = true;
                        break;
                    case 1:
                        var point = ConsoleHelper.GetPointAnswer();
                        Console.WriteLine(_game.ManualShoot(point) ? "Success!" : "Missed :(");
                        Console.WriteLine("Bot's turn, press any key.");
                        Console.ReadKey();
                        Console.WriteLine(_game.AutoShoot() ? "Ouch! You're shot!" : "Bot missed!");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine(_game.AutoShoot() ? "Success!" : "Missed :(");
                        Console.WriteLine("Bot's turn, press any key.");
                        Console.ReadKey();
                        Console.WriteLine(_game.AutoShoot() ? "Ouch! You're shot!" : "Bot missed!");
                        Console.ReadKey();
                        break;
                }
            }

            Console.WriteLine("Game ended, exiting...");
        }
    }
}