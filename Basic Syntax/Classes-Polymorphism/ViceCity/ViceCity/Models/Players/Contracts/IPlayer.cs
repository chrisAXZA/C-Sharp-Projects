namespace ViceCity.Models.Players.Contracts
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using ViceCity.Models.Guns.Contracts;
    using ViceCity.Repositories.Contracts;

    public interface IPlayer
    {
        string Name { get; }

        bool IsAlive { get; }

        IRepository<IGun> GunRepository { get; }

        int LifePoints { get; }

        void TakeLifePoints(int points);
            
    }
}
