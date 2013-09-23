using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace harjoitustyo
{
    class Program
    {
        private static string alphabet = "a,b,c";
        private static string states = "A,B,C";
        private static string transitionTable = "{A,a,B};{B,a,C};{B,b,A};{C,c,A}";
        private static string startingState = "A";


        static void Main(string[] args)
        {
            //Console.WriteLine("Welcome to the DFA generator program!");
            //Console.WriteLine("Start using generator by determing what you want to do: ");
            //PrintInstructions();
            //HandleInput();
            //Tests tests = new Tests();

            DFA dfa = DFAFactory.BuildDefaultDFA(alphabet, states, transitionTable, startingState);
            

            

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
