namespace ViceCity.IO
{
    using System;
    using System.Text;
    using ViceCity.IO.Contracts;
    using System.Collections.Generic;

    public class Writer : IWriter
    {
        public void Write(string line)
        {
            Console.Write(line);
        }

        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}
