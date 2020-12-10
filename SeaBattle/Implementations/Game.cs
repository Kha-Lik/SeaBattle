using System.Drawing;
using SeaBattle.Abstractions;

namespace SeaBattle.Implementations
{
    public class Game : IGame
    {
        private readonly IPlayer _playerOne;
        private readonly IPlayer _playerTwo;

        public Game(IPlayerBuilder playerBuilder, GameSettings settings)
        {
            _playerOne = playerBuilder.ConstructPlayer(settings.FirstPlayerName);
            _playerTwo = playerBuilder.ConstructPlayer(settings.SecondPlayerName);
            _playerOne.IsMyTurn = true;
        }

        public IBattlefield GetPlayerOneField()
        {
            return _playerOne.Battlefield;
        }

        public IBattlefield GetPlayerTwoField()
        {
            return _playerTwo.Battlefield;
        }

        public bool AutoShoot()
        {
            if (_playerOne.IsMyTurn)
            {
                _playerOne.IsMyTurn = false;
                _playerTwo.IsMyTurn = true;

                return _playerOne.AutoShot(_playerTwo.Battlefield);
            }

            _playerOne.IsMyTurn = true;
            _playerTwo.IsMyTurn = false;

            return _playerTwo.AutoShot(_playerOne.Battlefield);
        }

        public bool ManualShoot(Point target)
        {
            if (_playerOne.IsMyTurn)
            {
                _playerOne.IsMyTurn = false;
                _playerTwo.IsMyTurn = true;

                return _playerOne.ManualShot(target, _playerTwo.Battlefield);
            }

            _playerOne.IsMyTurn = true;
            _playerTwo.IsMyTurn = false;

            return _playerTwo.ManualShot(target, _playerOne.Battlefield);
        }
    }
}