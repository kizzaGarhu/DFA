using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace harjoitustyo
{
    public class DefaultStateBehaviour : IStateBehaviour
    {
        public void Update()
        {
            Console.WriteLine("This is an example of default state behaviour Update!");
        }
    }
}
