using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace harjoitustyo
{
    class Program
    {
        static void Main(string[] args)
        {
            Tests tests = new Tests();

            tests.DriveTests();
            
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
