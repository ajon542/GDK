using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformComms
{
    // Communication between the GameServer and the Platform.
    // There are going to be two types of communication between the game server and the platform.
    // The first is when the game server makes a request and the platform responds. For example,
    // the game server may request random numbers or tell the platform that it wishes to start a
    // new wager game.
    // The second is when the platform notifies the game server of some event such as a suspend
    // or when credits have been updated.

    // Register game server with platform
    // Game configuration -> paytable, button layout
    // Wager start
    // Wager end
    // Base game start
    // Base game end
    // Bonus start (could include free games since this is really just a bonus)
    // Bonus end
    // Get random numbers
    // Suspend / Resume
    // Insert demo credits
    // Credits updated
    // Pay win
    // Enter / Exit History


    public class Class1
    {
    }
}
