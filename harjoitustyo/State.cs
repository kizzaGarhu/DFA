using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace harjoitustyo
{
    public abstract class State
    {
        // Class variables
        protected string _stateName;
        public string StateName { get { return _stateName; } }

        protected int _stateID;
        public int StateID { get { return _stateID; } }

        protected Dictionary<int, int> stateTransitions = new Dictionary<int, int>();
        public Dictionary<int, int> StateTransitions { get { return stateTransitions; } }

        public State(string stateName, int stateID)
        {
            this._stateName = stateName;
            this._stateID = stateID;
        }

        // Class Behaviour
        public void AddTransition(Transition transition, State state)
        {
            
            // Check that the given parameters are valid.
            if (transition==null || state == null)
            {
                Console.WriteLine("ERROR in State AddTransition: given parameters are not valid!");
                return;
            }//if
            
            // Check for duplicates - a deterministic finite automata cannot have a transition with a same id multiple times.
            if (stateTransitions.ContainsKey(transition.TransitionID))
            {
                Console.WriteLine("ERROR in State AddTransition: transition with id of " + transition.TransitionID + " is already included in state's transitions!");
                return;
            }

            // Add transition
            stateTransitions.Add(transition.TransitionID, state._stateID);
        }//AddTransition

        public void RemoveTransition(Transition transition)
        {
            //Check whether given parameter is valid.
            if (isTransitionValid(transition)) { }

            //Check whether given transitions exists and remove transition from state
            if (stateTransitions.ContainsKey(transition.TransitionID))
            {
                stateTransitions.Remove(transition.TransitionID);
                Console.WriteLine("Removed transition with the id of " + transition.TransitionID.ToString() +
                    " and name of " + transition.TransitionName + " from state " + _stateName + " with id " + _stateID);
                return;
            }//if

            //If transition wasn't found, write error
            Console.WriteLine("ERROR in RemoveTransition: transition was not found in state's transitions");

        }// RemoveTransition

        public int GetStateToTransit(Transition transition)
        {
            // Check whether given parameter is valid
            if (isTransitionValid(transition)) { }

            // Check whether state has transition and return state id the transition is pointing to
            if (stateTransitions.ContainsKey(transition.TransitionID))
            {
                return stateTransitions[transition.TransitionID];
            }

            // if transition wasn't found, return -1, which is default id for non-existing state. In other words transition points to state itself.
            return -1;

        }//GetStateToTransit

        public virtual void stateBehaviour()
        {
            Console.WriteLine("This is default state behaviour!");
        }

        public virtual void onEnteringState()
        {
            Console.WriteLine("Activing state with id of " + _stateID.ToString() + " and name of " + _stateName);
        }

        public virtual void onLeavingState()
        {
            Console.WriteLine("Deactiving state with id of " + _stateID.ToString() + " and name of " + _stateName);
        }

        private bool isTransitionValid(Transition transition)
        {
            if (transition == null)
            {
                Console.WriteLine("ERROR: given transition parameter is null");
                return false;
            }//if

            return true;
        }//isTransitionValid

    }//State
}
