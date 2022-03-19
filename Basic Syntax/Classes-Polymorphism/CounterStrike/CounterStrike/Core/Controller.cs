namespace CounterStrike.Core
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    using CounterStrike.Models.Maps;
    using CounterStrike.Models.Guns;
    using CounterStrike.Repositories;
    using CounterStrike.Core.Contracts;
    using CounterStrike.Models.Players;
    using CounterStrike.Utilities.Messages;
    using CounterStrike.Models.Maps.Contracts;
    using CounterStrike.Models.Guns.Contracts;
    using CounterStrike.Models.Players.Contracts;

    public class Controller : IController
    {
        private readonly GunRepository guns;
        private readonly PlayerRepository players;
        private readonly IMap map;

        public Controller()
        {
            this.guns = new GunRepository();
            this.players = new PlayerRepository();
            this.map = new Map();
        }

        public string AddGun(string type, string name, int bulletsCount)
        {
            IGun currentGun;
            if (type == "Pistol")
            {
                currentGun = new Pistol(name, bulletsCount);
            }
            else if (type == "Rifle")
            {
                currentGun = new Rifle(name, bulletsCount);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidGunType);
            }

            this.guns.Add(currentGun);

            return string.Format(OutputMessages.SuccessfullyAddedGun, name);
        }

        public string AddPlayer(string type, string username, int health, int armor, string gunName)
        {
            IGun playerGun = this.guns.FindByName(gunName);

            if (playerGun == null)
            {
                throw new ArgumentException(ExceptionMessages.GunCannotBeFound);
            }

            IPlayer currentPlayer;

            if (type == "Terrorist")
            {
                currentPlayer = new Terrorist(username, health, armor, playerGun);
            }
            else if (type == "CounterTerrorist")
            {
                currentPlayer = new CounterTerrorist(username, health, armor, playerGun);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidPlayerType);
            }

            this.players.Add(currentPlayer);

            return string.Format(OutputMessages.SuccessfullyAddedPlayer, username);
        }

        public string Report()
        {
            List<IPlayer> playerList = this.players.Models
                .OrderBy(p => p.GetType().Name)
                .ThenByDescending(p => p.Health)
                .ThenBy(p => p.Username)
                .ToList();

            //List<IPlayer> counterList = this.players.Models
            //    .Where(p => p.GetType() == typeof(CounterTerrorist))
            //    .OrderByDescending(p => p.Health)
            //    .ThenBy(p => p.Username)
            //    .ToList();

            //List<IPlayer> terrorList = this.players.Models
            //    .Where(p => p.GetType() == typeof(Terrorist))
            //    .OrderByDescending(p => p.Health)
            //    .ThenBy(p => p.Username)
            //    .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var player in playerList)
            {
                sb.AppendLine(player.ToString());
            }

            //foreach (var player in counterList)
            //{
            //    sb.AppendLine(player.ToString());
            //}

            //sb.ToString().Trim();

            //foreach (var player in terrorList)
            //{
            //    sb.AppendLine(player.ToString());
            //}

            return sb.ToString().TrimEnd();
        }

        public string StartGame()
        {
            //List<IPlayer> playerList = new List<IPlayer>();
            List<IPlayer> playerList = this.players.Models.ToList();

            string result = this.map.Start(playerList);

            return result;
        }
    }
}
