using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace harjoitustyo
{
    #region Transition Component
    class Transition
    {
        // Class variables
        private string transitionName;
        public string TransitionName { get { return this.transitionName; } }

        private int transitionID;
        public int TransitionID { get { return this.TransitionID; } }

        /// <summary>
        /// Assigns given parameters to variables.
        /// </summary>
        /// <param name="transitionName"></param>
        /// <param name="transitionID"></param>
        public Transition(string transitionName, int transitionID)
        {
            this.transitionName = transitionName;
            this.transitionID = transitionID;
        }//Transition
    }//Transition
    #endregion

    #region State Component
    public abstract class State { 
        // Class variables
        protected string stateName;
        public string StateName { get { return this.stateName; } }

        protected int stateID;
        public int StateID { get { return this.stateID; } }

        protected Dictionary<int, int> stateTransitions = new Dictionary<int,int>();

        public State(string stateName, int stateID) {
            this.stateName = stateName;
            this.stateID = stateID;
        }

        // Class Behaviour
        private void AddTransition(Transition transition, State state) { 
            // Check that the given parameters are valid.
            if(isTransitionValid(transition) || state == null){
                Console.WriteLine("ERROR in State AddTransition: given parameters are not valid!");
                return;
            }//if

            // Check for duplicates - a deterministic finite automata cannot have a transition with a same id multiple times.
            if (stateTransitions.ContainsKey(transition.TransitionID)) {
                Console.WriteLine("ERROR in State AddTransition: transition with id of " + transition.TransitionID + " is already included in state's transitions!");
                return;
            }

            // Add transition
            stateTransitions.Add(transition.TransitionID, state.stateID);
        }//AddTransition

        private void RemoveTransition(Transition transition) { 
            //Check whether given parameter is valid.
            if (isTransitionValid(transition)) {}

            //Check whether given transitions exists and remove transition from state
            if(stateTransitions.ContainsKey(transition.TransitionID)){
                stateTransitions.Remove(transition.TransitionID);
                Console.WriteLine("Removed transition with the id of " + transition.TransitionID.ToString() + 
                    " and name of " + transition.TransitionName + " from state " + stateName + " with id " +stateID);
            }//if
        
            //If transition wasn't found, write error
            Console.WriteLine("ERROR in RemoveTransition: transition was not found in state's transitions");

        }// RemoveTransition

        public int GetStateToTransit(Transition transition) { 
            // Check whether given parameter is valid
            if(isTransitionValid(transition)){}

            // Check whether state has transition and return state id the transition is pointing to
            if (stateTransitions.ContainsKey(transition.TransitionID)) { 
                return stateTransitions[transition.TransitionID];
            }

            // if transition wasn't found, return -1, which is default id for non-existing state. In other words transition points to state itself.
            return -1;

        }//GetStateToTransit
        
        public virtual void stateBehaviour() {
            Console.WriteLine("This is default state behaviour!");
        }

        public virtual void onEnteringState() {
            Console.WriteLine("Activing state with id of " + stateID.ToString() + " and name of " + stateName);
        }

        public virtual void onLeavingState() { 
            Console.WriteLine("Deactiving state with id of " + stateID.ToString() + " and name of " + stateName);
        }

        private bool isTransitionValid(Transition transition) { 
            if(transition == null){
                Console.WriteLine("ERROR: given transition parameter is null");
                return false;
            }//if

            return true;
        }//isTransitionValid

    }//State
    
    #endregion

    #region DFA Component#
    public class DFA
    {
        private List<State> states;
        private List<Transition> alphabet;

        private State currentState;
        public State CurrentState { get { return this.currentState; } set { this.currentState = value; } }

        public DFA(List<Transition> alphabet, List<State>states) {
            this.states = states;
            this.alphabet = alphabet;
        }//DFA

        /// <summary>
        /// Adds the given state to dfa.
        /// </summary>
        /// <param name="stateToAdd"></param>
        private void AddState(State stateToAdd) { 
            //Check whether given state already exists in DFA or the given state has same ID already as some other state in DFA 
            foreach (State existingState in states) { 
                if(existingState.StateID == stateToAdd.StateID){
                    Console.WriteLine("ERROR: Cannot add state to the dfa with same id!");
                    return;
                }//if
            }//foreach

            // Add the state
            states.Add(stateToAdd);

        }//AddState

        /// <summary>
        /// Removes the given state from dfa.
        /// </summary>
        /// <param name="StateToRemove"></param>
        private void RemoveState(State StateToRemove) { 
            // Check whether the state to be removed is current state. Removing current state is not allowed.
            if(StateToRemove.StateID == currentState.StateID){
                Console.WriteLine("ERROR: Cannot remove state that is currently active.");
            }//if

            // Loops through dfa's states and compares their id to given state's id. If match is found, state is removed from dfa.
            foreach(State existingState in states){
                if(existingState.StateID == StateToRemove.StateID){
                    states.Remove(StateToRemove);
                    Console.WriteLine("Removed state " + StateToRemove.StateName + " with an id of " + StateToRemove.StateID.ToString() + " from dfa.");
                    return;
                }//if
            }//foreach

            //If state was not found, print error
            Console.WriteLine("ERROR: state " + StateToRemove.StateName + " with an id of " + StateToRemove.StateID.ToString() + " was not found in dfa.");

        }//RemoveState

        private void PerformTransition(Transition transition) {
            int statetoTransit = currentState.GetStateToTransit(transition);

            //  
            if(statetoTransit != -1){
                foreach(State newState in states){
                    if (newState.StateID == statetoTransit) {
                        // Before transiting to new state, let the old state finish up and the new one to initialize.
                        currentState.onLeavingState();
                        currentState = newState;
                        currentState.onEnteringState();
                    }//if
                }//foreach
            }//if
        }//PerformTransition

    }
    #endregion


    /// <summary>
    /// Creates and returns a DFA  
    /// </summary>
    public class DFAFactory {



        public DFA buildDefaultDFA(string alphabet, string states, string transitionTable, string startingState) {
            
            //Resolve alphabet from given string.
            string[] transitionNames = Utils.ResolveAlphabetFromString(alphabet);
            
            //Check for duplicate alphabet (transition names).
            Utils.CheckForDuplicates(transitionNames);
            
            //Create transition parameter for DFA from alphabet. 
            List<Transition> transitionsParameter = new List<Transition>();
            for (int i = 0; i < transitionNames.Length; i++) {
                transitionsParameter.Add(new Transition(transitionNames[i], i));
            }

            //Resolve states' names
            string[] stateNames = Utils.ResolveStatesFromString(states); 

            //Check for duplicate state names.
            Utils.CheckForDuplicates(stateNames);

            //Create state parameter for DFA from list of state names.
            List<State> stateParameter = new List<State>();
            //TODO: What kind of state to use?
            //Create Default states with nothing fancy.
            
            //Create transitions.

            //Define starting state

            //resolve transitions
            
            DFA dfa = new DFA();    
        }

    }//DFAFactory

    /// <summary>
    /// A static class for a set of helper methods.
    /// </summary>
    public static class Utils{
        /// <summary>
        /// Checks given set for duplicates. DFA cannot have multiple transitions or states with same name.
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public static bool CheckForDuplicates(string[] set) {
            for (int i = 0; i < set.Length; i++) {
                for (int j = i+1; j < set.Length; j++) { 
                    if(set[i].Equals(set[j])){
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
        public static string[] ResolveAlphabetFromString(string alphabet) { 
            string[] result = alphabet.Split(',');
            return result;
        }//ResolveAlphabetFromString

        /// <summary>
        /// Resolves individual state names from given string and returns results.
        /// </summary>
        /// <param name="states"></param>
        /// <returns></returns>
        public static string[] ResolveStatesFromString(string states) { 
            string[] result = states.Split(',');
            return result;
        }//ResolveStatesFromString

        /// <summary>
        /// Resolves transitions from given string and returns results.
        /// </summary>
        /// <param name="transitions"></param>
        /// <returns></returns>
        public static string[] ResolveTransitionsFromString(string transitions) {
            string[] delimiters = { "{", "}", ";" };
            string[] sets = transitions.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            /*
            for (int i = 0; i < sets.Length; i++)
            {
                Console.WriteLine(sets[i]);
            }
            Console.WriteLine(sets.Length.ToString());
            */
            return sets;
        }
    }
}
