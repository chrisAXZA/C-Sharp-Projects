namespace ViceCity.Models.Guns
{
    using System;

    public class Pistol : Gun
    {
        private const int FIRE_RATE = 1;
        private const int INITIAL_BARREL_COUNT = 10;
        private const int INITIAL_TOTAL_BULLETS = 100;
        
        public Pistol(string name)
            : base(name, INITIAL_BARREL_COUNT, INITIAL_TOTAL_BULLETS)
        {
        }

        public override int Fire()
        {
            if (this.BulletsPerBarrel >= FIRE_RATE)
            {
                this.BulletsPerBarrel -= FIRE_RATE;
                if (this.BulletsPerBarrel == 0)
                {
                    if (this.TotalBullets >= INITIAL_BARREL_COUNT)
                    {
                        this.BulletsPerBarrel = INITIAL_BARREL_COUNT;
                        this.TotalBullets -= INITIAL_BARREL_COUNT;
                    }
                }
                return FIRE_RATE;
            }
            else if(this.TotalBullets >= INITIAL_BARREL_COUNT)
            {
                this.BulletsPerBarrel = INITIAL_BARREL_COUNT;
                this.TotalBullets -= INITIAL_BARREL_COUNT;
                return FIRE_RATE;
            }

            return 0;
        }
    }
}
