using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace harjoitustyo
{
    /// <summary>
    /// A static class for a set of helper methods.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Splits the given string.
        /// </summary>
        /// <param name="stringToSplit"></param>
        /// <returns></returns>
        public static string[] SplitParameter(string stringToSplit) { 
            string[] delimiters = {"{","}",";",","};
            string[] result = stringToSplit.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            return result;
        }//SplitParameter

        public static string[] SplitStateTransitions(string stringToSplit) {
            var result = stringToSplit.Split('-');
            return result;
        }
        
        /// <summary>
        /// Checks given set for duplicates. DFA cannot have multiple transitions or states with same name.
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public static bool CheckForDuplicates(string[] set)
        {
            for (int i = 0; i < set.Length; i++)
            {
                for (int j = i + 1; j < set.Length; j++)
                {
                    if (set[i].Equals(set[j]))
                    {
                        Console.WriteLine("ERROR: can't have duplicate values!");
                        return false;
                    }//if    
                }//for
            }//for
            return true;
        }//CheckForDuplicates

        public static List<Transition> CreateTransitionsFromAlphabet(string alphabet) {
            //Check whether given parameter is valid
            if(alphabet == null){
                Console.WriteLine("ERROR: parameter is invalid!");
                return null;
            }

            //Split transition names from given string.
            var splittingResult = SplitParameter(alphabet);

            //Check for duplicates.
            if(CheckForDuplicates(splittingResult)){
                var transitions = new List<Transition>();

                for (int i = 0; i < splittingResult.Length; i++) {
                    transitions.Add(new Transition(splittingResult[i], i));
                }//for
                
                return transitions;
            }

            Console.WriteLine("Couldn't create transitions");
            return null;
        }//CreateTransitionsFromAlphabet

        public static List<State> CreateDefaultStatesFromString(string statesString)
        {
            //Check whether given parameter is valid
            if (statesString == null)
            {
                Console.WriteLine("ERROR: parameter is invalid!");
                return null;
            }

            //Split transition names from given string.
            var splittingResult = SplitParameter(statesString);

            //Check for duplicates.
            if (CheckForDuplicates(splittingResult))
            {
                var states = new List<State>();

                for (int i = 0; i < splittingResult.Length; i++)
                {
                    states.Add(new DefaultState(splittingResult[i], i));
                }//for

                return states;
            }

            Console.WriteLine("Couldn't create transitions");
            return null;
        }//CreateTransitionsFromAlphabet



        //#####################################################################
        /*
        /// <summary>
        /// Splits individual transition names from given string and returns results.
        /// </summary>
        /// <param name="alphabet"></param>
        /// <returns></returns>
        public static string[] ResolveAlphabetFromString(string alphabet)
        {
            string[] result = alphabet.Split(',');
            return result;
        }//ResolveAlphabetFromString


        /// <summary>
        /// Resolves individual state names from given string and returns results.
        /// </summary>
        /// <param name="states"></param>
        /// <returns></returns>
        public static string[] ResolveStatesFromString(string states)
        {
            string[] result = states.Split(',');
            return result;
        }//ResolveStatesFromString


        /// <summary>
        /// Resolves transitions from given string and returns results.
        /// </summary>
        /// <param name="transitionTable"></param>
        /// <returns></returns>
        public static string[] ResolveTransitionSetsFromString(string transitionTable)
        {
            string[] delimiters = { "{", "}", ";" };
            string[] sets = transitionTable.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            
            //for (int i = 0; i < sets.Length; i++)
            //{
                //Console.WriteLine(sets[i]);
            //}
            //Console.WriteLine(sets.Length.ToString());
            
            return sets;
        }//ResolveTransitionsFromString


        public static string[] ResolveTransitionFromTransitionSet(string transitionSet)
        {
            string[] result = transitionSet.Split(',');
            return result;
        }
        */
    }//Utils
}
