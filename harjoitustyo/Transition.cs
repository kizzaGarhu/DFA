using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace harjoitustyo
{
    public class Transition
    {
        // Class variables
        private string _transitionName;
        public string TransitionName { get { return _transitionName; } }

        private int _transitionID;
        public int TransitionID { get { return _transitionID; } }

        /// <summary>
        /// Assigns given parameters to variables.
        /// </summary>
        /// <param name="transitionName"></param>
        /// <param name="transitionID"></param>
        public Transition(string transitionName, int transitionID)
        {
            _transitionName = transitionName;
            _transitionID = transitionID;
        }//Transition
    }//Transition
}
