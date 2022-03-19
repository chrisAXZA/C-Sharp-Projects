namespace ViceCity.IO
{
    using System;
    using System.Text;
    using ViceCity.IO.Contracts;
    using System.Collections.Generic;

    class Reader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
