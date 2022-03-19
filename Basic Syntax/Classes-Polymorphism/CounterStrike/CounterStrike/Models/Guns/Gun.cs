namespace CounterStrike.Models.Guns
{
    using System;

    using CounterStrike.Utilities.Messages;
    using CounterStrike.Models.Guns.Contracts;

    public abstract class Gun : IGun
    {
        private const int MINIMUM_BULLETS = 0;

        private string name;
        private int bulletsCount;

        protected Gun(string name, int bulletsCount)
        {
           this.Name = name;
           this.BulletsCount = bulletsCount;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGunName);
                }
                this.name = value;
            }
        }

        public int BulletsCount
        {
            get
            {
                return this.bulletsCount;
            }
            protected set
            {
                if (value < MINIMUM_BULLETS)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGunBulletsCount);
                }
                this.bulletsCount = value;
            }
        }

        public abstract int Fire();
    }
}
