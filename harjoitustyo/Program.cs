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
            
        }//Main

        public static void PrintInstructions() {
            Console.WriteLine("Press q to exit, d to create a user defined default dfa or g to build pre-defined default dfa.");
        }//PrintInstructions

        public static void PrintDFAInstructions() {
            Console.WriteLine("Press t to transit to state");
        }

        public static void HandleInput() {
            if (Console.ReadKey(true).Key == ConsoleKey.A) {
                Console.WriteLine("Pressed A");
            }
        }

        
    }
}
