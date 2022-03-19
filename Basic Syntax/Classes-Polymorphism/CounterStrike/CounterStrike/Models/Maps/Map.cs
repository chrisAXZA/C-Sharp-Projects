namespace CounterStrike.Models.Maps
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using CounterStrike.Models.Players;
    using CounterStrike.Models.Maps.Contracts;
    using CounterStrike.Models.Players.Contracts;

    public class Map : IMap
    {
        public string Start(ICollection<IPlayer> players)
        {
            //IFighter fighter = (IFighter)machine;

            ICollection<Terrorist> terrorists = new List<Terrorist>();
            ICollection<CounterTerrorist> counterTerrorists = new List<CounterTerrorist>();

            CreateTerroristAndCounterTerroristGroups(players, terrorists, counterTerrorists);

            while (terrorists.Any(t => t.IsAlive) && counterTerrorists.Any(c => c.IsAlive))
            {
                TerroristsFire(terrorists, counterTerrorists);

                CounterTerroristsFire(terrorists, counterTerrorists);
            }

            if (terrorists.All(t => !t.IsAlive))
            {
                return "Counter Terrorist wins!";
            }
            //else if (counterTerrorists.All(c => !c.IsAlive))
            //{
                return "Terrorist wins!";
            //}
            
            //return string.Empty; 
        }

        private static void CounterTerroristsFire(ICollection<Terrorist> terrorists, ICollection<CounterTerrorist> counterTerrorists)
        {
            foreach (var counterTerrorist in counterTerrorists)
            {
                if (!counterTerrorist.IsAlive)
                {
                    continue;
                }

                int damage = counterTerrorist.Gun.Fire();

                foreach (var terrorist in terrorists)
                {
                    if (!terrorist.IsAlive)
                    {
                        continue;
                    }
                    terrorist.TakeDamage(damage);
                }
            }
        }

        private static void TerroristsFire(ICollection<Terrorist> terrorists, ICollection<CounterTerrorist> counterTerrorists)
        {
            foreach (var terrorist in terrorists)// .Where(t => t.IsAlive)
            {
                if (!terrorist.IsAlive)
                {
                    continue;
                }

                int damage = terrorist.Gun.Fire();

                foreach (var counterTerrorist in counterTerrorists)// Where(c => c.IsAlive)
                {
                    if (!counterTerrorist.IsAlive)
                    {
                        continue;
                    }
                    counterTerrorist.TakeDamage(damage);
                }
            }
        }

        private static void CreateTerroristAndCounterTerroristGroups(ICollection<IPlayer> players, ICollection<Terrorist> terrorists, ICollection<CounterTerrorist> counterTerrorists)
        {
            var terrorGroup = players.Where(p => p.GetType() == typeof(Terrorist)).ToList();

            var antiTerrorGroup = players.Where(p => p.GetType() == typeof(CounterTerrorist)).ToList();

            foreach (var player in players)
            {
                if (player.GetType() == typeof(Terrorist))
                {
                    terrorists.Add((Terrorist)player);
                }
                else if (player.GetType() == typeof(CounterTerrorist))
                {
                    counterTerrorists.Add((CounterTerrorist)player);
                }
            }
        }
    }
}
