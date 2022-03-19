namespace CounterStrike.Repositories
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using CounterStrike.Utilities.Messages;
    using CounterStrike.Repositories.Contracts;
    using CounterStrike.Models.Players.Contracts;

    public class PlayerRepository : IRepository<IPlayer>
    {
        private readonly ICollection<IPlayer> models;

        public PlayerRepository()
        {
            this.models = new List<IPlayer>();
        }

        public IReadOnlyCollection<IPlayer> Models
        {
            get
            {
                return (IReadOnlyCollection<IPlayer>)this.models;
            }
        }

        public void Add(IPlayer model)
        {
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidPlayerRepository);
            }

            this.models.Add(model);
        }

        public IPlayer FindByName(string name)
        {
            return this.models.FirstOrDefault(p => p.Username == name);
        }

        public bool Remove(IPlayer model)
        {
            return this.models.Remove(model);
        }
    }
}
