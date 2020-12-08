using SeaBattle.Abstractions;

namespace SeaBattle.Implementations
{
    public class PlayerBuilder : IPlayerBuilder
    {
        private readonly BattlefieldBuilder _battlefieldBuilder;

        public PlayerBuilder(BattlefieldBuilder battlefieldBuilder)
        {
            _battlefieldBuilder = battlefieldBuilder;
        }

        public IPlayer ConstructPlayer()
        {
            Player player = new();
            player.Battlefield = ConstructBattlefield();
            player.AutoShotMethod = new RandomShot(player);

            return player;
        }

        private IBattlefield ConstructBattlefield()
        {
            _battlefieldBuilder.BuildBattlefield();
            _battlefieldBuilder.PlaceShips();
            return _battlefieldBuilder.GetResult();
        }
    }
}