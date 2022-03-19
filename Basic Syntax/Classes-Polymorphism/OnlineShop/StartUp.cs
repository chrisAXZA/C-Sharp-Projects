namespace OnlineShop
{
    using System.IO;
    using OnlineShop.IO;
    using OnlineShop.Core;

    public class StartUp
    {
        static void Main()
        {
            // Clears output.txt file
            string pathFile = Path.Combine("..", "..", "..", "output.txt");
            File.Create(pathFile).Close();

            IWriter writerFile = new FileWriter(pathFile);
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();
            ICommandInterpreter commandInterpreter = new CommandInterpreter();
            IController controller = new Controller();

            //IEngine engine = new Engine(reader, writerFile, commandInterpreter, controller);
            IEngine engine = new Engine(reader, writer, commandInterpreter, controller);
            engine.Run();
        }
    }
}
