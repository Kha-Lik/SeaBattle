using System.Drawing;

namespace SeaBattle
{
    public class Game : IGame
    {
        private IPlayer _playerOne;
        private IPlayer _playerTwo;

        public Game(IPlayerBuilder playerBuilder)
        {
            _playerOne = playerBuilder.ConstructPlayer();
            _playerTwo = playerBuilder.ConstructPlayer();
        }

        public IBattlefield GetPlayerOneField() => _playerOne.Battlefield;

        public IBattlefield GetPlayerTwoField() => _playerTwo.Battlefield;

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