using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace harjoitustyo
{
    class Program
    {
        // Parameters for default dfa.
        private static string _alphabet = "a,b,c"; //names of the transitions.
        private static string _states = "A,B,C"; //names of the states.
        private static string _transitionFunction = "{A-a-B};{B-a-C};{B-b-A};{C-c-A}"; //Transition functions: for example {A,a,B} means State A has a transition a to state B.
        private static string _startingState = "A"; //stating state.

        private static DFA dfa;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello and welcome to the DFA generator!");
            PrintInstructions();

            HandleInput();

            //dfa = DFAFactory.BuildDefaultDFA(_alphabet, _states, _transitionFunction, _startingState);
            //dfa.PerformTransition("a");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            
        }//Main

        public static void PrintInstructions() {
            Console.WriteLine("Press Esc to exit, u to create a user defined default dfa or g to build pre-defined default dfa.");
        }//PrintInstructions

        public static void PrintDFAInstructions() {
            Console.WriteLine("Press t to transit to state");
        }//PrintDFAInstructions

        public static void HandleInput() {
            if (Console.ReadKey(true).Key == ConsoleKey.Escape) {
                //Don't do anything    
            }
            
            if(Console.ReadKey(true).Key == ConsoleKey.G){
                dfa = DFAFactory.BuildDefaultDFA(_alphabet, _states, _transitionFunction, _startingState);
                OperateDFA();
            }
            
            if (Console.ReadKey(true).Key == ConsoleKey.U) {
                //Query user for DFA parameters
                Console.WriteLine("Write DFA alphabet in form of symbol,symbol,symbol without spaces between symbols - for example a,b,c");
                string alphabet = Console.ReadLine();
                Console.WriteLine("Write states in form of State,State,State without spaces between state names - for example A,B,C");
                string states = Console.ReadLine();
                Console.WriteLine("Write transition function in form of {starting state-transition-goal state};{starting state-transition-goal state} - for example {A-a-B}");
                string transitionFunction = Console.ReadLine();
                Console.WriteLine("Write the name of the starting state");
                string startingState = Console.ReadLine();

                //Build default dfa
                dfa = DFAFactory.BuildDefaultDFA(alphabet, states,transitionFunction,startingState);
                OperateDFA();
            }
        }//HandleInput

        public static void OperateDFA(){
            bool isOperating = true;
            while(isOperating){
                //Print available states
                Console.WriteLine("DFA has following states: ");
                foreach(State state in dfa.States){
                    Console.WriteLine(state.StateName);
                }
                
                //Print available transitions
                Console.WriteLine("DFA has following transitions: ");
                foreach(Transition transition in dfa.Alphabet){
                    Console.WriteLine(transition.TransitionName);
                }

                //Ask for user input
                Console.WriteLine("Enter transition's name");
                dfa.PerformTransition(Console.ReadLine());
                Console.WriteLine("Current state is: " + dfa.CurrentState.StateName);
                Console.WriteLine("Press Esc to quit or enter transition's name");
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    isOperating = false;   
                }//if
            }//while
        }//OperateDFA

        
    }
}
