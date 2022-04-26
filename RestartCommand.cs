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
                player.ErrorMessage("\nRestart does not contain " + this.SecondWord);
                player.LocationMessage("\n" + player.CurrentRoom.Description());
            }
            else
            {
                player.NotificationMessage("\nSpace and time starts warping around you.\n");
                player.RestartGame();
            }
            return false;
        }
    }
}
