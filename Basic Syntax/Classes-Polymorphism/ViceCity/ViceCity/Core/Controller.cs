namespace ViceCity.Core
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    using ViceCity.Models.Guns;
    using ViceCity.Repositories;
    using ViceCity.Models.Players;
    using ViceCity.Core.Contracts;
    using ViceCity.Models.Neghbourhoods;
    using ViceCity.Models.Guns.Contracts;
    using ViceCity.Repositories.Contracts;
    using ViceCity.Models.Players.Contracts;
    using ViceCity.Models.Neghbourhoods.Contracts;

    public class Controller : IController
    {
        private MainPlayer mainPlayer;
        private INeighbourhood neighbourhood;
        private IRepository<IGun> gunRepository;
        private readonly ICollection<IPlayer> civilPlayers;

        private const int INITIAL_CIVILIAN_HEALTH = 50;
        private const int INITIAL_MAIN_PLAYER_HEALTH = 100;

        //private readonly ICollection<IGun> guns; // or Queue<IGun> since we are always working with the
        //first gun added to the collection

        public Controller()
        {
            this.civilPlayers = new List<IPlayer>();
            this.mainPlayer = new MainPlayer();
            this.gunRepository = new GunRepository();
            this.neighbourhood = new GangNeighbourhood();

            //this.guns = new List<IGun>(); // EXAM PREP DEMO
        }

        public string AddGun(string type, string name)
        {
            IGun newGun;

            if (type == nameof(Pistol))
            {
                newGun = new Pistol(name);
            }
            else if (type == nameof(Rifle))
            {
                newGun = new Rifle(name);
            }
            else
            {
                return "Invalid gun type!";
            }

            this.gunRepository.Add(newGun);

            return $"Successfully added {name} of type: {type}";
        }

        public string AddGunToPlayer(string name)
        {
            // or IGun gun = this.gunRepository.Models.FirstOrDefault();
            // if(gun == null) -> return error

            if (this.gunRepository.Models.Count == 0)
            {
                return "There are no guns in the queue!";
            }

            IGun currentGun = this.gunRepository.Models.First();

            if (name == "Vercetti")
            {
                this.mainPlayer.GunRepository.Add(currentGun);
                this.gunRepository.Remove(currentGun);
                return $"Successfully added {currentGun.Name} to the Main Player: Tommy Vercetti";
            }

            // OR: IPlayer currentCivilian = this.civilPlayers.First(civ => civ.Name == name);
            // if (currentCivilian == null) -> throw exception

            if (this.civilPlayers.All(civ => civ.Name != name))
            {
                return "Civil player with that name doesn't exists!";
            }

            IPlayer currentCivilian = this.civilPlayers.First(civ => civ.Name == name);

            currentCivilian.GunRepository.Add(currentGun);

            this.gunRepository.Remove(currentGun);

            return $"Successfully added {currentGun.Name} to the Civil Player: {name}";
        }

        public string AddPlayer(string name)
        {
            IPlayer newPlayer = new CivilPlayer(name);

            if (!this.civilPlayers.Contains(newPlayer))
            {
                this.civilPlayers.Add(newPlayer);
            }

            return $"Successfully added civil player: {name}!";
        }

        public string Fight()
        {
            if (this.mainPlayer.GunRepository.Models.Any(gun => gun.CanFire)
               || this.civilPlayers.Any(civ => civ.GunRepository.Models.Any(gun => gun.CanFire)) )
            {
                this.neighbourhood.Action(this.mainPlayer, this.civilPlayers);
            }
            //this.neighbourhood.Action(this.mainPlayer, this.civilPlayers);

            if (this.mainPlayer.LifePoints == INITIAL_MAIN_PLAYER_HEALTH
                && this.civilPlayers.All(civ => civ.LifePoints == INITIAL_CIVILIAN_HEALTH))
            {
                return "Everything is okay!";
            }

            StringBuilder sb = new StringBuilder();

            int deadCivilianCount = this.civilPlayers.Count(civ => civ.IsAlive == false);
            int liveCivilPlayers = this.civilPlayers.Count(civ => civ.IsAlive == true);

            sb.AppendLine("A fight happened:");
            sb.AppendLine($"Tommy live points: {mainPlayer.LifePoints}!");
            sb.AppendLine($"Tommy has killed: {deadCivilianCount} players!");
            sb.AppendLine($"Left Civil Players: {liveCivilPlayers}!");

            return sb.ToString().TrimEnd();
        }
    }
}
