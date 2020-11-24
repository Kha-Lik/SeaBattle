namespace SeaBattle
{
    public abstract class AutoShotMethod
    {
        protected IPlayer Player;

        protected AutoShotMethod(IPlayer player)
        {
            Player = player;
        }

        public abstract bool Shoot(IBattlefield battlefield);

        protected void ChangeShotMethod(AutoShotMethod method)
        {
            Player.AutoShotMethod = method;
        }
    }
}