namespace ViceCity.Models.Players
{
    using System;

    using ViceCity.Repositories;
    using ViceCity.Models.Guns.Contracts;
    using ViceCity.Repositories.Contracts;
    using ViceCity.Models.Players.Contracts;

    public abstract class Player : IPlayer
    {
        private string name;
        private int lifePoints;

        protected Player(string name, int lifePoints)
        {
            this.Name = name;
            this.LifePoints = lifePoints;
            this.GunRepository = new GunRepository();
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
                    throw new ArgumentNullException("Player's name cannot be null or a whitespace!");
                    //throw new ArgumentNullException(value, "Player's name cannot be null or a whitespace!");
                    //should be with param name to work correctly
                }
                this.name = value;
            }
        }

        public int LifePoints
        {
            get
            {
                return this.lifePoints;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Player life points cannot be below zero!");
                }
                this.lifePoints = value;
            }
        }

        public bool IsAlive => this.LifePoints > 0;

        public IRepository<IGun> GunRepository { get; }

        public void TakeLifePoints(int points)
        {
            if (this.LifePoints > points)
            {
                this.LifePoints -= points;
            }
            else
            {
                this.LifePoints = 0;
            }

            //if (this.LifePoints < 0)
            //{
            //    this.LifePoints = 0;
            //}
        }
    }
}
