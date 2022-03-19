namespace ViceCity.Models.Neghbourhoods
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using ViceCity.Models.Guns.Contracts;
    using ViceCity.Models.Players.Contracts;
    using ViceCity.Models.Neghbourhoods.Contracts;

    public class GangNeighbourhood : INeighbourhood
    {
        public void Action(IPlayer mainPlayer, ICollection<IPlayer> civilPlayers)
        {

            while (mainPlayer.IsAlive
            && civilPlayers.Any(civ => civ.IsAlive)
            && (mainPlayer.GunRepository.Models.Any(gun => gun.CanFire)
            || civilPlayers.Any(civ => civ.GunRepository.Models.Any(gun => gun.CanFire))))
            {
                while (mainPlayer.GunRepository.Models.Any(gun => gun.CanFire)
                    && civilPlayers.Any(civ => civ.IsAlive))
                {
                    IGun currentGun = mainPlayer.GunRepository.Models
                        .FirstOrDefault(gun => gun.CanFire);

                    IPlayer currentCivilian = civilPlayers.First(civ => civ.IsAlive);

                    MainPlayerFires(currentGun, currentCivilian);
                }

                while (civilPlayers.Any(civ => civ.IsAlive == true)
                    && civilPlayers.Any(civ => civ.GunRepository.Models.Any(gun => gun.CanFire))
                    && mainPlayer.IsAlive)
                {
                    IPlayer currentCivilian = civilPlayers.First(civ => civ.IsAlive);

                    IGun currentGun = currentCivilian.GunRepository.Models
                        .FirstOrDefault(gun => gun.CanFire);

                    CiviliansFire(mainPlayer, currentGun);
                }
            }

            foreach (var currentGun in mainPlayer.GunRepository.Models)
            {
                if (!currentGun.CanFire)
                {
                    continue;
                }

                foreach (var currentCivilian in civilPlayers)
                {
                    if (!currentCivilian.IsAlive)
                    {
                        continue;
                    }

                    while (currentCivilian.IsAlive && currentGun.CanFire)
                    {
                        currentCivilian.TakeLifePoints(currentGun.Fire());
                    }

                    if (!currentGun.CanFire)
                    {
                        break;
                    }
                }
            }

            foreach (var currentCivilian in civilPlayers)
            {
                if (!currentCivilian.IsAlive)
                {
                    continue;
                }

                foreach (var currentGun in currentCivilian.GunRepository.Models)
                {
                    while (currentGun.CanFire && mainPlayer.IsAlive)
                    {
                        mainPlayer.TakeLifePoints(currentGun.Fire());
                    }

                    if (!currentGun.CanFire)
                    {
                        break;
                    }
                }

                if (!mainPlayer.IsAlive)
                {
                    break;
                }
            }
        }

        private static void CiviliansFire(IPlayer mainPlayer, IGun currentGun)
        {
            while (mainPlayer.IsAlive && currentGun.CanFire)
            {
                int damage = currentGun.Fire();
                mainPlayer.TakeLifePoints(damage);
            }
        }

        private static void MainPlayerFires(IGun currentGun, IPlayer currentCivilian)
        {
            while (currentCivilian.IsAlive && currentGun.CanFire)
            {
                int damage = currentGun.Fire();
                currentCivilian.TakeLifePoints(damage);
            }
        }
    }
}
