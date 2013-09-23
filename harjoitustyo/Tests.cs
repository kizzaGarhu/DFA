using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace harjoitustyo
{
    class Tests
    {
        // Test variables;
        private string _alphabet = "a,b,c"; //names of the transitions
        private string _states = "A,B,C"; //names of the states
        private string _transitionFunction = "{A-a-B};{B-a-C};{B-b-A};{C-c-A}"; //Transition functions: for example {A,a,B} means State A has a transition a to state B.
        private string _startingState = "A";
        

        /// <summary>
        /// Drives a set of tests. 
        /// </summary>
        public void DriveTests() {
            //Utils component tests
            //TestParameterSplitting();
            //TestStateAndTransitionsSplitting();
            //TestParameterDuplicates();
            
            //Transition component tests
            //CreateTestTransitions();
            //CreateTestTransitionsFromString();
            
            //State component tests
            //CreateTestStates();
            //CreateTestStatesFromString();
            //TryAddingTransitionsToState();
            //TryRemovingTransitionsFromState();
            //TryGettingStateToTransit();

            //DFA component tests
            //CreateDFA();
            //TryAddingStatesToDFA();
            //TryRemovingStatesFromDFA();
            TryChangingDFAState();
        }

        #region Utils component tests
        // TEST:Success.
        private void TestParameterSplitting() {
            // Test alphabet string
            string[] alphabetResult = Utils.SplitParameter(_alphabet);
            Console.WriteLine("Alphabet: ");
            for (int i = 0; i < alphabetResult.Length; i++) {
                Console.WriteLine(alphabetResult[i]);
            }//for

            //Test states string
            var statesResult = Utils.SplitParameter(_states);
            Console.WriteLine("States: ");
            for (int i = 0; i < statesResult.Length; i++)
            {
                Console.WriteLine(statesResult[i]);
            }//for

            //Test transitionFunction string
            var transitionFunctionResult = Utils.SplitParameter(_transitionFunction);
            Console.WriteLine("Transition sets: ");
            for (int i = 0; i < transitionFunctionResult.Length; i++)
            {
                Console.WriteLine(transitionFunctionResult[i]);
            }
        }

        //TEST:Success.
        private void TestStateAndTransitionsSplitting() {
            var splittingResult = Utils.SplitParameter(_transitionFunction);
            for (int i = 0; i < splittingResult.Length; i++) {
                var furtherSplitResult = Utils.SplitStateTransitions(splittingResult[i]);

                Console.WriteLine("From state " + furtherSplitResult[0] + " there is transition " + furtherSplitResult[1] + " to state " + furtherSplitResult[2]);
                
            }//for
        }//TestStateAndTransitionsSplitting

        //TEST:Success.
        private void TestParameterDuplicates() {
            var duplicate = "a,a,b,c";
            var resultAfterSplit = Utils.SplitParameter(duplicate);
            var isDuplicate = Utils.CheckForDuplicates(resultAfterSplit);

        }
        #endregion

        #region Transition Component tests
        private Transition _transitionA;
        private Transition _transitionB;

        //TEST: Success
        private void CreateTestTransitions() {
            _transitionA = new Transition("a", 0);
            _transitionB = new Transition("b", 0);

            Console.WriteLine("Transition name: " + _transitionA.TransitionName + " transition ID: " + _transitionA.TransitionID.ToString());
            Console.WriteLine("Transition name: " + _transitionB.TransitionName + " transition ID: " + _transitionB.TransitionID.ToString());
        }//CreateTestTransitions

        //TEST: Success.
        private void CreateTestTransitionsFromString() {
            var listOfTestTransitions = Utils.CreateTransitionsFromAlphabet(_alphabet);
            if(listOfTestTransitions != null){
                Console.WriteLine("Number of transitions: " + listOfTestTransitions.Count.ToString());
            }//if
            
        }
        #endregion

        #region State Component Tests
        private State _testStateA;
        private State _testStateB;

        //TEST: Success.
        private void CreateTestStates() {
            _testStateA = new DefaultState("A", 0);
            _testStateB = new DefaultState("B", 0);

            Console.WriteLine("State name is: " + _testStateA.StateName + " and ID: " + _testStateA.StateID.ToString());
            Console.WriteLine("State name is: " + _testStateB.StateName + " and ID: " + _testStateB.StateID.ToString());
        }//CreateTestStates

        //TEST: Success
        private void CreateTestStatesFromString() {
            var listOfTestStates = Utils.CreateDefaultStatesFromString(_states);
            if (listOfTestStates != null)
            {
                Console.WriteLine("Number of states: " + listOfTestStates.Count.ToString());
            }//if
        }

        private List<Transition> _testTransitionsFromString;
        private List<State> _testStatesFromString;

        //TEST: Success.
        private void TryAddingTransitionsToState() {
            _testTransitionsFromString = Utils.CreateTransitionsFromAlphabet(_alphabet); //List of transitions
            _testStatesFromString = Utils.CreateDefaultStatesFromString(_states); //List of states

            var splittingResult = Utils.SplitParameter(_transitionFunction);
            for (int i = 0; i < splittingResult.Length; i++)
            {
                var furtherSplitResult = Utils.SplitStateTransitions(splittingResult[i]);

                Console.WriteLine("From state " + furtherSplitResult[0] + " there is transition " + furtherSplitResult[1] + " to state " + furtherSplitResult[2]);

                foreach(var state in _testStatesFromString){
                    if(state.StateName.Equals(furtherSplitResult[0])){
                        
                        Transition tempTransition = null;
                        foreach (var trans in _testTransitionsFromString) { 
                            if(trans.TransitionName.Equals(furtherSplitResult[1])){
                                tempTransition = trans;
                            }//if
                        }//foreach
                        
                        State tempState = null;
                        foreach (var stat in _testStatesFromString) { 
                            if(stat.StateName.Equals(furtherSplitResult[2])){
                                tempState = stat;
                            }//if
                        }//foreach

                        state.AddTransition(tempTransition, tempState);
                        Console.WriteLine("Added transition " + tempTransition.TransitionName + " leading to state " + tempState.StateName + " in state " + state.StateName + " transitions.");
                    }//if
                }//foreach

            }//for
        }//TryAddingTransitionsToState

        //TEST: Success.
        private void TryRemovingTransitionsFromState() {
            TryAddingTransitionsToState();

            State state = _testStatesFromString[1];
            Console.WriteLine("State name: " + state.StateName);

            var testStatesTransitions = state.StateTransitions;

            //print state's transitions
            foreach (var entry in testStatesTransitions) {
                Console.WriteLine("Transition ID: " + entry.Key.ToString() + " State ID: " + entry.Value.ToString());
            }

            state.RemoveTransition(_testTransitionsFromString[1]);

            //print state's transitions
            foreach (var entry in testStatesTransitions)
            {
                Console.WriteLine("Transition ID: " + entry.Key.ToString() + " State ID: " + entry.Value.ToString());
            }
        }

        //TEST: success
        private void TryGettingStateToTransit() {
            TryAddingTransitionsToState();

            State state = _testStatesFromString[1];
            Console.WriteLine("State name: " + state.StateName);

            var testStatesTransitions = state.StateTransitions;

            //print state's transitions
            foreach (var entry in testStatesTransitions)
            {
                Console.WriteLine("Transition ID: " + entry.Key.ToString() + " State ID: " + entry.Value.ToString());
            }

            Console.WriteLine("Transition name: " + _testTransitionsFromString[1].TransitionName + " transition ID: " + _testTransitionsFromString[1].TransitionID.ToString());

            int stateIdToTransit = state.GetStateToTransit(_testTransitionsFromString[1]); //should return 0.
            Console.WriteLine("State to transit has id of: " + stateIdToTransit.ToString());

            stateIdToTransit = state.GetStateToTransit(_testTransitionsFromString[2]); //should return -1.
            Console.WriteLine("State to transit has id of: " + stateIdToTransit.ToString());
        } 

        /*
        private void StringSplitTest() {
            string alphabet = "a,b,c";
            string states = "A,B,C";
            string transitions = "{A,a,B};{B,a,C};{B,b,A};{C,c,A}";
            string startingState = "A";

            string testString ="{A,a,B};{B,a,C};{B,b,A};{C,c,A}"; //Transition string

            //string[] sets = testString.Split(';');
            string[] delimiters = { "{","}",";"};
            string[] sets = testString.Split(delimiters,StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < sets.Length; i++) {
                Console.WriteLine(sets[i]);
            }
            Console.WriteLine(sets.Length.ToString());

            //resolve state and transitions
            foreach(string sub in sets){
                //further split
                string[] temp = sub.Split(',');
                Console.WriteLine("From state " + temp[0] + " there is a transtion " + temp[1] + " to state " + temp[2]);
            }

        }
         */ 
        #endregion

        #region DFA Component tests
        private DFA _dfa;

        private void CreateDFA() { 
            _testTransitionsFromString = Utils.CreateTransitionsFromAlphabet(_alphabet);
            _testStatesFromString = Utils.CreateDefaultStatesFromString(_states);
            TryAddingTransitionsToState();
            _dfa = new DFA(_testTransitionsFromString, _testStatesFromString);

            Console.WriteLine("Created DFA!");
        }//CreateDFA

        private List<State> _listOfStatesInDFA; 

        //TEST:Success.
        private void TryAddingStatesToDFA() {
            CreateDFA();
            
            _listOfStatesInDFA = _dfa.States;
            
            Console.WriteLine("DFA has following states: ");
            foreach (State state in _listOfStatesInDFA) {
                Console.WriteLine(state.StateID.ToString() + " " + state.StateName);
            }//foreach

            _dfa.AddState(new DefaultState("A", 0)); //should fail because of same id and name.
            _dfa.AddState(new DefaultState("D", 3)); //should succeed.
            _dfa.AddState(new DefaultState("A", 4)); //should fail because of same name.

            Console.WriteLine("DFA has following states after adding: ");
            foreach (State state in _listOfStatesInDFA)
            {
                Console.WriteLine(state.StateID.ToString() + " " + state.StateName);
            }//foreach
            
            Console.WriteLine("Done adding states to dfa");
        }

        //TEST: Success.
        private void TryRemovingStatesFromDFA() {
            CreateDFA();
            TryAddingStatesToDFA();

            _dfa.CurrentState = _listOfStatesInDFA[0];

            foreach (State state in _listOfStatesInDFA)
            {
                Console.WriteLine(state.StateID.ToString() + " " + state.StateName);
            }//foreach

            _dfa.RemoveState(_listOfStatesInDFA[2]); //should succeed.

            foreach (State state in _listOfStatesInDFA)
            {
                Console.WriteLine(state.StateID.ToString() + " " + state.StateName);
            }//foreach

            State failTestState = new DefaultState("B", 2); //should fail.

            _dfa.RemoveState(failTestState);

            Console.WriteLine("Done removing states from DFA");
        }

        //TEST: Succes.
        private void TryChangingDFAState() {
            //Create DFA with states and transitions
            CreateDFA();
            TryAddingStatesToDFA();

            //assign one of the states active
            _dfa.CurrentState = _listOfStatesInDFA[0];

            Console.WriteLine("Currently active state: " + _dfa.CurrentState.StateName);

            //perform transition
            _dfa.PerformTransition(_testTransitionsFromString[0]); //should succeed.
            
            Console.WriteLine("Currently active state: " + _dfa.CurrentState.StateName);

            _dfa.PerformTransition(_testTransitionsFromString[2]); //should fail
            Console.WriteLine("Currently active state: " + _dfa.CurrentState.StateName);
        }
        #endregion

        /*
        private void ParameterTest() {
            string alphabet = "a,b,c";
            string states = "A,B,C";
            string transitionTable = "{A,a,B};{B,a,C};{B,b,A};{C,c,A}";
            string startingState = "A";
            
            string[] alphabetResult = Utils.ResolveAlphabetFromString(alphabet);
            string[] statesResult = Utils.ResolveStatesFromString(states);
            string[] transitionResult = Utils.ResolveTransitionSetsFromString(transitionTable);

            Console.WriteLine("Alphabet for the DFA: ");
            for (int i = 0; i < alphabetResult.Length; i++)
            {
                Console.Write(alphabetResult[i] + " ");
            }
            Console.WriteLine();

            Console.WriteLine("States for DFA: ");
            for (int i = 0; i < statesResult.Length; i++)
            {
                Console.Write(statesResult[i] + " ");
            }
            Console.WriteLine();

            Console.WriteLine("Transition sets for DFA: ");
            for (int i = 0; i < transitionResult.Length; i++)
            {
                Console.Write(transitionResult[i] + " ");
            }
            Console.WriteLine();
        }
         */ 

    }//Tests
    
}
