using System;
using SeaBattle.Implementations;

namespace SeaBattle.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var setUpView = new GameSetUpView();

            var settings = setUpView.Run();
            var builder = new RandomBattlefieldBuilder(settings);
            var playerBuilder = new PlayerBuilder(builder);
            var game = new Game(playerBuilder, settings);

            var gameView = new GameView(game);
            gameView.Run();
        }
    }
}