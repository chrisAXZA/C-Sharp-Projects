namespace ViceCity.Models.Players
{
    using System;

    public class CivilPlayer : Player
    {
        private const int INITIAL_HEALTH = 50;

        public CivilPlayer(string name)
            : base(name, INITIAL_HEALTH)
        {
        }
    }
}
