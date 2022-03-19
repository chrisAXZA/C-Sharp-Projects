namespace OnlineShop.IO
{
    using System;
    
    public class ConsoleReader : IReader
    {
        public string CustomReadLine()
        {
            //string finalText = Console.ReadLine();

            //return finalText;
            return Console.ReadLine();
        }
    }
}