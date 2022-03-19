namespace ViceCity.Core.Contracts
{
    using System;
    using System.Text;
    using System.Collections.Generic;

    interface IController
    {
        string AddPlayer(string name);

        string AddGun(string type, string name);

        string AddGunToPlayer(string name);

        string Fight();
    }
}
