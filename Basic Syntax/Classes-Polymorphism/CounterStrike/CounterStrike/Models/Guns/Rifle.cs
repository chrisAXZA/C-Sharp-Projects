namespace CounterStrike.Models.Guns
{
    using System;

    public class Rifle : Gun
    {
        private const int FIRE_RATE = 10;

        public Rifle(string name, int bulletsCount)
            : base(name, bulletsCount)
        {
        }

        public override int Fire()
        {
            // this.BulletsCount < 10 EXAM PREP DEMO
            //if (this.BulletsCount - FIRE_RATE < 0)
            if (this.BulletsCount < FIRE_RATE)
            {
                return 0;
            }

            this.BulletsCount -= FIRE_RATE;

            return FIRE_RATE;
        }
    }
}
