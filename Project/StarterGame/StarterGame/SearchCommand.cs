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
                player.OutputMessage("\nSearch does not contain " + this.SecondWord);
                player.OutputMessage("\n" + player.CurrentRoom.Description());
            }
            else
            {
                player.OutputMessage("\nYou look around the room\n");
                player.Search();
            }
            return false;
        }
    }
}
