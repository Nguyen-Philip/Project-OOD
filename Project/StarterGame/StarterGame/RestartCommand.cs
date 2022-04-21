using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class RestartCommand : Command
    {
        public RestartCommand() : this(new CommandWords()){}
        // Designated Constructor
        public RestartCommand(CommandWords commands) : base()
        {
            this.Name = "restart";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.OutputMessage("\nRestart does not contain " + this.SecondWord);
                player.OutputMessage("\n" + player.CurrentRoom.Description());
            }
            else
            {
                player.OutputMessage("\nSpace and time starts warping around you.\n");
                player.RestartGame();
            }
            return false;
        }
    }
}
