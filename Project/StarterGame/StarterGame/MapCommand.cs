using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class MapCommand : Command
    {
        public MapCommand() : this(new CommandWords()) { }
        // Designated Constructor
        public MapCommand(CommandWords commands) : base()
        {
            this.Name = "map";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.ErrorMessage("\nMap does not contain " + this.SecondWord);
                player.LocationMessage("\n" + player.CurrentRoom.Description());
            }
            else
            {
                player.NotificationMessage("\nYou open the map\n");
                player.Map();
            }
            return false;
        }
    }
}
