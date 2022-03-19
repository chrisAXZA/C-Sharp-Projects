namespace ViceCity.Core
{
    using System;
    using System.Text;
    using System.Collections.Generic;

    using ViceCity.IO;
    using ViceCity.IO.Contracts;
    using ViceCity.Core.Contracts;

    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;
        private readonly IController controller;

        public Engine()
        {
            this.reader = new Reader();
            this.writer = new Writer();
            this.controller = new Controller();
        }
        
        public void Run()
        {
            while (true)
            {
                var input = reader.ReadLine().Split();
                if (input[0] == "Exit")
                {
                    Environment.Exit(0);
                }
                try
                {
                    string result = string.Empty;

                    if (input[0] == "AddPlayer")
                    {
                        string playerName = input[1];

                        result = controller.AddPlayer(playerName);
                    }
                    else if (input[0] == "AddGun")
                    {
                        string gunType = input[1];
                        string gunName = input[2];

                        result = controller.AddGun(gunType, gunName);
                    }
                    else if (input[0] == "AddGunToPlayer")
                    {
                        string playerName = input[1];

                        result = controller.AddGunToPlayer(playerName);
                    }
                    else if (input[0] == "Fight")
                    {
                        result = controller.Fight();
                    }
                    writer.WriteLine(result);
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }
        }
    }
}
