namespace ViceCity.Repositories.Contracts
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using ViceCity.Models.Guns.Contracts;

    public interface IRepository<T>
    {
        IReadOnlyCollection<T> Models { get; }

        void Add(IGun model);

        bool Remove(IGun model);

        IGun Find(string name);

    }
}
