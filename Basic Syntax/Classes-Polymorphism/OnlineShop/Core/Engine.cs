namespace OnlineShop.Core
{
    using System;
    
    using OnlineShop.IO;

    public class Engine : IEngine
    {
        private const string Separator = " ";

        //private readonly FileWriter fileWriter;
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IController controller;
        private readonly ICommandInterpreter commandInterpreter;

        public Engine(IReader reader, IWriter writer, ICommandInterpreter commandInterpreter, IController controller)
        {
            this.reader = reader;
            this.writer = writer;
            //this.fileWriter = (FileWriter)writer;
            this.commandInterpreter = commandInterpreter;
            this.controller = controller;
        }

        public void Run()
        {
            while (true)
            {
                string[] data = this.reader.CustomReadLine().Split(" ");
                string msg;

                try
                {
                    msg = this.commandInterpreter.ExecuteCommand(data, this.controller);
                }
                catch (ArgumentException e)
                {
                    msg = e.Message;
                }
                catch (InvalidOperationException e)
                {
                    msg = e.Message;
                }

                this.writer.CustomWriteLine(msg);
            }
        }
    }
}