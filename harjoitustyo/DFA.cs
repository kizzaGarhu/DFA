using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace harjoitustyo
{
    public class DFA
    {
        private List<State> _states;
        public List<State> States { get { return _states; } }

        private List<Transition> _alphabet;
        public List<Transition> Alphabet { get { return _alphabet; } }

        private State _currentState;
        public State CurrentState { get { return _currentState; } set { _currentState = value; } }

        public DFA(List<Transition> alphabet, List<State>states) {
            this._states = states;
            this._alphabet = alphabet;
        }//Constructor

        /// <summary>
        /// Adds the given state to dfa.
        /// </summary>
        /// <param name="stateToAdd"></param>
        public void AddState(State stateToAdd) { 
            //Check whether given state already exists in DFA or the given state has same ID or name as some other state in DFA 
            foreach (State existingState in _states) { 
                if(existingState.StateID == stateToAdd.StateID){
                    Console.WriteLine("ERROR: Cannot add state to the dfa with same id!");
                    return;
                }//if

                if (existingState.StateName.Equals(stateToAdd.StateName))
                {
                    Console.WriteLine("ERROR: Cannot add state to the dfa with same name!");
                    return;
                }//if

            }//foreach

            // Add the state
            _states.Add(stateToAdd);

        }//AddState

        /// <summary>
        /// Removes the given state from dfa.
        /// </summary>
        /// <param name="StateToRemove"></param>
        public void RemoveState(State StateToRemove) { 
            // Check whether the state to be removed is current state. Removing current state is not allowed.
            if(StateToRemove.StateID == _currentState.StateID){
                Console.WriteLine("ERROR: Cannot remove state that is currently active.");
                return;
            }//if

            // Loops through dfa's states and compares their id to given state's id. If match is found, state is removed from dfa.
            foreach(State existingState in _states){
                if(existingState.StateID == StateToRemove.StateID){
                    _states.Remove(StateToRemove);
                    Console.WriteLine("Removed state " + StateToRemove.StateName + " with an id of " + StateToRemove.StateID.ToString() + " from dfa.");
                    return;
                }//if
            }//foreach

            //If state was not found, print error
            Console.WriteLine("ERROR: state " + StateToRemove.StateName + " with an id of " + StateToRemove.StateID.ToString() + " was not found in dfa.");

        }//RemoveState

        public void PerformTransition(Transition transition) {
            int statetoTransit = _currentState.GetStateToTransit(transition);

            //  
            if(statetoTransit != -1){
                foreach(State newState in _states){
                    if (newState.StateID == statetoTransit) {
                        // Before transiting to new state, let the old state finish up and the new one to initialize.
                        _currentState.onLeavingState();
                        _currentState = newState;
                        _currentState.onEnteringState();
                    }//if
                }//foreach
            }//if
        }//PerformTransition

    }//DFA
   
    
}
