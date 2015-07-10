using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class Slot : Game
    {
        public override void ConfigureStates(StateMachine.StateMachine sm)
        {
            // TODO: Probably want to initialize base class states first.
            // and then initialize our states.
            base.ConfigureStates(sm);


        }
    }
}
