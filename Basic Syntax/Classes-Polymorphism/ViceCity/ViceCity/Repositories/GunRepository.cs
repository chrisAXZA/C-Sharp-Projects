namespace ViceCity.Repositories
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using ViceCity.Models.Guns.Contracts;
    using ViceCity.Repositories.Contracts;

    public class GunRepository : IRepository<IGun>
    {
        private readonly ICollection<IGun> models;

        // EXAM PREP DEMO
        //private readonly List<IGun> models;

        public GunRepository()
        {
            this.models = new List<IGun>();
        }

        public IReadOnlyCollection<IGun> Models => (IReadOnlyCollection<IGun>)this.models;
        // EXAM PREP DEMO
        //public IReadOnlyCollection<IGun> Models => this.models.AsReadOnly();

        public void Add(IGun model)
        {
            if (!this.models.Any(gun => gun.Name == model.Name))
            {
                this.models.Add(model);
            }
        }

        public IGun Find(string name)
        {
            // EXAM PREP DEMO
            //return this.models.Find(gun => gun.Name.Equals(name));

            return this.models.First(gun => gun.Name == name);
        }

        public bool Remove(IGun model)
        {
            //IGun targetGun = this.models.FirstOrDefault(gun => gun.Name == model.Name);
            return this.models.Remove(model);
        }
    }
}
