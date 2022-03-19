namespace ViceCity.IO.Contracts
{
    using System;
    using System.Text;
    using System.Collections.Generic;

    interface IWriter
    {
        void WriteLine(string line);

        void Write(string line);
      
    }
}
