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

        private int GetStateToTransit(Transition transition) { 
            // Check whether given parameter is valid
            if(isTransitionValid(transition)){}

            // Check whether state has transition and return state id the transition is pointing to
            if (stateTransitions.ContainsKey(transition.TransitionID)) { 
                return stateTransitions[transition.TransitionID];
            }

            // if transition wasn't found, return -1, which is default id for non-existing state. In other words transition points to state itself.
            return -1;

        }//PerformTransition
        
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
        private List<Transition> transitions;

        private State currentState;
        public State CurrentState { get { return this.currentState; } set { this.currentState = value; } }

    }
    #endregion

    /// <summary>
    /// A static class for a set of helper methods.
    /// </summary>
    public static class Utils{
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
        }
    }
}
