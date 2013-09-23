using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace harjoitustyo
{
    /// <summary>
    /// A default state for DFA.   
    /// </summary>
    public class DefaultState : State
    {

        IStateBehaviour defaultStateBehaviour;

        public DefaultState(string stateName, int stateID)
            : base(stateName, stateID)
        {
            defaultStateBehaviour = new DefaultStateBehaviour();
        }

        public override void onEnteringState()
        {
            Console.WriteLine("This is an example of overridden onEnteringState method");
        }


        public override void stateBehaviour()
        {
            defaultStateBehaviour.Update();
        }
    }//DefaultState
}
