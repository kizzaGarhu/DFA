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
            //TestParameterSplitting();
            //TestStateAndTransitionsSplitting();
            //TestParameterDuplicates();
            
            //CreateTestTransitions();
            //CreateTestTransitionsFromString();
            //CreateTestStates();
            //CreateTestStatesFromString();
            TryAddingTransitionsToState();
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

        //TEST: Success.
        private void TryAddingTransitionsToState() {
            var testTransitionsFromString = Utils.CreateTransitionsFromAlphabet(_alphabet); //List of transitions
            var testStatesFromString = Utils.CreateDefaultStatesFromString(_states); //List of states

            var splittingResult = Utils.SplitParameter(_transitionFunction);
            for (int i = 0; i < splittingResult.Length; i++)
            {
                var furtherSplitResult = Utils.SplitStateTransitions(splittingResult[i]);

                Console.WriteLine("From state " + furtherSplitResult[0] + " there is transition " + furtherSplitResult[1] + " to state " + furtherSplitResult[2]);

                foreach(var state in testStatesFromString){
                    if(state.StateName.Equals(furtherSplitResult[0])){
                        
                        Transition tempTransition = null;
                        foreach (var trans in testTransitionsFromString) { 
                            if(trans.TransitionName.Equals(furtherSplitResult[1])){
                                tempTransition = trans;
                            }//if
                        }//foreach
                        
                        State tempState = null;
                        foreach (var stat in testStatesFromString) { 
                            if(stat.StateName.Equals(furtherSplitResult[2])){
                                tempState = stat;
                            }//if
                        }//foreach

                        state.AddTransition(tempTransition, tempState);
                        Console.WriteLine("Added transition " + tempTransition.TransitionName + " leading to state " + tempState.StateName + " in state " + state.StateName + " transitions.");
                    }//if
                }

            }//for
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
