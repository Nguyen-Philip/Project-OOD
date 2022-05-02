using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class StatsCommand : Command
    {
        public StatsCommand() : this(new CommandWords()) { }
        // Designated Constructor
        public StatsCommand(CommandWords commands) : base()
        {
            this.Name = "stats";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.ErrorMessage("\nStats does not contain " + this.SecondWord);
            }
            else
            {
                player.NotificationMessage("\nYou look at your stats\n");
                player.Stats();
            }
            player.LocationMessage("\n" + player.CurrentRoom.Description());
            return false;
        }
    }
}
