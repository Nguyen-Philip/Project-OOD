using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class BrowseCommand : Command
    {
        public BrowseCommand() : this(new CommandWords()) { }
        // Designated Constructor
        public BrowseCommand(CommandWords commands) : base()
        {
            this.Name = "browse";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.ErrorMessage("\nBrowse does not contain " + this.SecondWord);
            }
            else
            {
                player.NotificationMessage("\nYou browse the shop\n");
                player.Browse();
            }
            player.LocationMessage("\n" + player.CurrentRoom.Description());
            return false;
        }
    }
}
