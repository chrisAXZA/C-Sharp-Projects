namespace CounterStrike.Models.Guns
{
    using System;

    public class Pistol : Gun
    {
        private const int FIRE_RATE = 1;

        public Pistol(string name, int bulletsCount)
            : base(name, bulletsCount)
        {
        }

        public override int Fire()
        {
            // <= in EXAM PREP DEMO
            if (this.BulletsCount == 0)
            {
                return 0;
            }

            this.BulletsCount -= FIRE_RATE;

            return FIRE_RATE;
        }
    }
}
