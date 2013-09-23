using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace harjoitustyo
{
    public class Transition
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
}
