namespace ViceCity.Models.Neghbourhoods.Contracts
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using ViceCity.Models.Players.Contracts;

    public interface INeighbourhood
    {
        void Action(IPlayer mainPlayer, ICollection<IPlayer> civilPlayers);
    }
}
