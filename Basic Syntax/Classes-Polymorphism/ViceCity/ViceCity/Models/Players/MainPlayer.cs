namespace ViceCity.Models.Players
{
    using System;

    public class MainPlayer : Player
    {
        private const int INITIAL_HEALTH = 100;

        public MainPlayer()
            : base("Tommy Vercetti", INITIAL_HEALTH)
        {
        }
    }
}
