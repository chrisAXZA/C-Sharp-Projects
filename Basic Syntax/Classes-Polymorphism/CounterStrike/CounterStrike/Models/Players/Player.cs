namespace CounterStrike.Models.Players
{
    using System;
    using System.Text;

    using CounterStrike.Utilities.Messages;
    using CounterStrike.Models.Guns.Contracts;
    using CounterStrike.Models.Players.Contracts;

    public abstract class Player : IPlayer
    {
        private string username;
        private int health;
        private int armor;
        private IGun gun;
        //private bool isAlive;

        protected Player(string username, int health, int armor, IGun gun)
        {
            this.Username = username;
            this.Health = health;
            this.Armor = armor;
            this.Gun = gun;
            //this.IsAlive = true;
        }

        public string Username
        {
            get
            {
                return this.username;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlayerName);
                }
                this.username = value;
            }
        }

        // !!! FOR EXAM BOTH NEEDED TO BE PRIVATE, check with builder if runs with private
        // otherwise protected or public!!!!
        public int Health
        {
            get
            {
                return this.health;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlayerHealth);
                }
                this.health = value;
            }
        }

        public int Armor
        {
            get
            {
                return this.armor;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlayerArmor);
                }
                this.armor = value;
            }
        }

        public IGun Gun
        {
            get
            {
                return this.gun;
            }
            // Private set for gun,should not be able to be changed
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGun);
                }
                this.gun = value;
            }
        }

        public bool IsAlive => this.Health > 0;
        //public bool IsAlive
        //{
        //    get
        //    {
        //        return this.Health > 0;
        //    }
        //}
        //public bool IsAlive { get; protected set; }
        //{
        //    get
        //    {
        //        return this.isAlive;
        //    }
        //    private set
        //    {
        //        this.isAlive = value;
        //    }
        //}

        public void TakeDamage(int points)
        {
            if (this.Armor > points)
            {
                this.Armor -= points;
            }
            else
            {
                int remainingDamage = points - this.Armor;
                //this.armor -= (points - remainingDamage);
                this.Armor = 0;

                if (this.Health > remainingDamage)
                {
                    this.Health -= remainingDamage;
                }
                else
                {
                    this.Health = 0;
                    //this.IsAlive = false;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.GetType().Name}: {this.Username}");
            sb.AppendLine($"--Health: {this.Health}");
            sb.AppendLine($"--Armor: {this.Armor}");
            sb.AppendLine($"--Gun: {this.Gun.Name}");

            return sb.ToString().TrimEnd();
        }
    }
}
