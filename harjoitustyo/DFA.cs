using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace harjoitustyo
{
    public class DFA
    {
        private List<State> states;
        public List<State> States { get { return this.states; } }

        private List<Transition> alphabet;
        public List<Transition> Alphabet { get { return this.alphabet; } }

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

    }//DFA
   
    
}
