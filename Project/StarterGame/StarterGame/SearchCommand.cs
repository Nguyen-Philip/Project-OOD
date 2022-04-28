using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class SearchCommand : Command
    {
        public SearchCommand() : this(new CommandWords()) { }
        // Designated Constructor
        public SearchCommand(CommandWords commands) : base()
        {
            this.Name = "search";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.ErrorMessage("\nSearch does not contain " + this.SecondWord);
            }
            else
            {
                player.NotificationMessage("\nYou look around the room\n");
                player.Search();
            }
            player.LocationMessage("\n" + player.CurrentRoom.Description());
            return false;
        }
    }
}
