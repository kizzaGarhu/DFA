using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace harjoitustyo
{
    /// <summary>
    /// Creates and returns a DFA  
    /// </summary>
    public static class DFAFactory
    {

        public static DFA BuildDefaultDFA(string alphabet, string states, string transitionTable, string startingStateName)
        {

            //Check that the given parameters are valid.
            if (alphabet == null || states == null || transitionTable == null || startingStateName == null)
            {
                Console.WriteLine("ERROR: given parameters to build default dfa are invalid");
                return null;
            }


            //Resolve alphabet from given string.
            string[] transitionNames = Utils.ResolveAlphabetFromString(alphabet);

            //Check for duplicate alphabet (transition names).
            Utils.CheckForDuplicates(transitionNames);

            //Create transition parameter for DFA from alphabet. 
            List<Transition> transitionsParameter = new List<Transition>();
            for (int i = 0; i < transitionNames.Length; i++)
            {
                transitionsParameter.Add(new Transition(transitionNames[i], i));
            }


            //Resolve states' names
            string[] stateNames = Utils.ResolveStatesFromString(states);

            //Check for duplicate state names.
            Utils.CheckForDuplicates(stateNames);

            //Create state parameter for DFA from list of state names.
            List<State> statesParameter = new List<State>();
            for (int i = 0; i < stateNames.Length; i++)
            {
                statesParameter.Add(new DefaultState(stateNames[i], i));
            }


            //Create transitions.
            string[] transitionSets = Utils.ResolveTransitionSetsFromString(transitionTable);

            //Further split transition sets
            for (int i = 0; i < transitionSets.Length; i++)
            {
                string[] transitionAndState = Utils.ResolveTransitionFromTransitionSet(transitionSets[i]);

                State stateToAddTransition = null;
                foreach (State state in statesParameter)
                {
                    if (transitionAndState[0].Equals(state.StateName))
                    {
                        stateToAddTransition = state;
                    }//if
                }//foreach


                Transition transitionToAdd = null;
                foreach (Transition transition in transitionsParameter)
                {
                    if (transitionAndState[1].Equals(transition.TransitionName))
                    {
                        transitionToAdd = transition;
                    }//if
                }//foreach

                State stateToTransit = null;
                foreach (State state in statesParameter)
                {
                    if (transitionAndState[2].Equals(state.StateName))
                    {
                        stateToTransit = state;
                    }//if
                }//foreach

                stateToAddTransition.AddTransition(transitionToAdd, stateToTransit);

            }//for

            //Define starting state
            State startingState = null;
            foreach (State state in statesParameter)
            {
                if (state.StateName.Equals(startingStateName))
                {
                    startingState = state;
                }//if
            }//foreach

            //Build default DFA
            DFA defaultDFA = new DFA(transitionsParameter, statesParameter);
            defaultDFA.CurrentState = startingState;

            return defaultDFA;
        }//BuildDefaultDFA

    }//DFAFactory
}
