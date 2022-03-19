namespace CounterStrike.Repositories
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using CounterStrike.Utilities.Messages;
    using CounterStrike.Models.Guns.Contracts;
    using CounterStrike.Repositories.Contracts;

    public class GunRepository : IRepository<IGun>
    {
        private readonly ICollection<IGun> models;

        public GunRepository()
        {
            this.models = new List<IGun>();
        }

        public IReadOnlyCollection<IGun> Models
        {
            get
            {
                return (IReadOnlyCollection<IGun>)this.models;
            }
        }

        public void Add(IGun model)
        {
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidGunRepository);
            }

            this.models.Add(model);
        }

        public IGun FindByName(string name)
        {
            return this.models.FirstOrDefault(g => g.Name == name);
        }

        public bool Remove(IGun model)
        {
            return this.models.Remove(model);
        }
    }
}
