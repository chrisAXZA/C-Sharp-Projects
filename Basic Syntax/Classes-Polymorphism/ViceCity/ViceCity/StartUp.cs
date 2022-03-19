namespace ViceCity
{
    using System;

    using ViceCity.Core;
    using ViceCity.Core.Contracts;
    using ViceCity.Models.Players;
    using ViceCity.Models.Players.Contracts;

    public class StartUp
    {
        //IEngine engine;
        static void Main(string[] args)
        {
            //IPlayer player = new CivilPlayer("Pesho");
            //Console.WriteLine($"nameof: {nameof(player)}");
            //Console.WriteLine($"gettype: {player.GetType().Name}");

            IEngine engine = new Engine();
            engine.Run();
        }
    }
}
