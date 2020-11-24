namespace SeaBattle
{
    public abstract class AutoShotMethod
    {
        protected IPlayer Player;
        protected IBattlefield Battlefield;

        protected AutoShotMethod(IPlayer player, IBattlefield battlefield)
        {
            Player = player;
            Battlefield = battlefield;
        }

        public abstract bool Shoot();

        protected void ChangeShotMethod(AutoShotMethod method)
        {
            Player.AutoShotMethod = method;
        }
    }
}