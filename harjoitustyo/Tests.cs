﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace harjoitustyo
{
    class Tests
    {
        // Test variables;
        private string[] transitionTestSet1 = {"a","b","c","d"}; //should be valid set
        private string[] transitionTestSet2 = { "a", "a", "b", "c" }; //should be invalid set because of duplicate transition names
        private Transition a2;
        private List<Transition> testTransitions = new List<Transition>();

        private string[] stateTestSet1 = { "A","B","C","D"}; //should be valid set
        private string[] stateTestSet2 = { "A","A","B","C"}; //should be invalid set because of duplicate state names.
        private State A2;
        private List<State> testStates = new List<State>();


        /// <summary>
        /// Drives a set of tests. 
        /// </summary>
        public void DriveTests() {
            
            // Transition Tests
            // CreateTestTransitions(transitionTestSet1); //should succeed. OK.
            // CreateTestTransitions(transitionTestSet2); //should fail. OK.
            //CreateTestStates(stateTestSet1); //should succeed. OK.
            //CreateTestStates(stateTestSet2); //should fail. OK.

            StringSplitTest();
        }

        #region Transition Component tests
        /// <summary>
        /// Creates a set of test transitions.
        /// TEST: Succeed.
        /// </summary>
        /// <param name="transitionSet"></param>
        private void CreateTestTransitions(string[] transitionSet) {
            if (Utils.CheckForDuplicates(transitionSet)) {

                for (int i = 0; i < transitionSet.Length; i++)
                {
                    testTransitions.Add(new Transition(transitionSet[i],i));
                    a2 = new Transition("a2", 0); //creates a single transition with a same id as one from above set.
                }//for
                Console.WriteLine("Test transitions created!");
            }//if

        }//CreateTransitions
        #endregion

        #region State Component Tests
        private void CreateTestStates(string[] stateSet) { 
            if(Utils.CheckForDuplicates(stateSet)){
                for (int i = 0; i < stateSet.Length; i++) {
                    testStates.Add(new TestState(stateSet[i], i));
                    A2 = new TestState("A2", 0); //creates a single state with a same id as one from above set.
                
                }//for
                Console.WriteLine("Test states created!");
                
            }//if
        }

        private void AddTransitionsToTestStates(List<Transition> transitionSet, List<State> stateSet) { 
            //Create transition table
            string[,] transitionTable = new string[stateSet.Count, transitionSet.Count];
            transitionTable[0, 0] = "B";
            transitionTable[1, 0] = "C";
            transitionTable[1, 1] = "A";
            transitionTable[2, 2] = "C";

            //{A,a,B},{B,a,C},{B,b,A},{C,c,A}

            //Transform state list to ordered array so there's no need to  

            //Loop through transition table
           
            
            //Create Transitions

            //Create States
        }


        private void StringSplitTest() { 
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
        #endregion

    }//Tests

    public class TestState : State {
        IStateBehaviour testBehaviour;
        public TestState(string stateName, int stateID) : base(stateName, stateID){
            testBehaviour = new TestStateBehaviour();
        }

        public override void onEnteringState()
        {
            Console.WriteLine("This is an example of overridden onEnteringState method"); 
        }


        public override void stateBehaviour()
        {
            testBehaviour.Update();    
        }
    }

    public interface IStateBehaviour{
        void Update();
    }

    public class TestStateBehaviour : IStateBehaviour {
        public void Update() {
            Console.WriteLine("This TestStateBehaviour Update!");
        }
    }
}
