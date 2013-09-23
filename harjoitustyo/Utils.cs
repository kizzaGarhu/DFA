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
            /*
            for (int i = 0; i < sets.Length; i++)
            {
                Console.WriteLine(sets[i]);
            }
            Console.WriteLine(sets.Length.ToString());
            */
            return sets;
        }//ResolveTransitionsFromString


        public static string[] ResolveTransitionFromTransitionSet(string transitionSet)
        {
            string[] result = transitionSet.Split(',');
            return result;
        }
    }//Utils
}
