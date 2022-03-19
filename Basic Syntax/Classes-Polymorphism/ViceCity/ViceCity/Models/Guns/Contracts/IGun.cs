namespace ViceCity.Models.Guns.Contracts
{
    using System;
    using System.Text;
    using System.Collections.Generic;

    public interface IGun
    {
        string Name { get; }

        int BulletsPerBarrel { get; }

        int TotalBullets { get; }

        bool CanFire { get; }

        int Fire();
    }
}
