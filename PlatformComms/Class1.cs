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

    // There are also different scenarios when we may or may not want to communicate with the platform.
    // For example during normal game play, we are always communicating with the platform. Normal game play
    // occurs between Wager start and Wager end.
    // Once a game is completed, it is possible that we wish to recover the game as in history or if a power
    // hit occurs. There will be no comms with the platform and all information will come from the Replay log.
    // If a powerhit occurs before the Wager end is specified, then the game will communicate with the platform
    // as per normal, except for when it draws random numbers. Why?
    // Why can't platform also store the random numbers. That way it will handle the recovery of those too??
    // No because we want to control infinite free games in our game stream. That way, we handle the random numbers
    // and we can still call functions

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
