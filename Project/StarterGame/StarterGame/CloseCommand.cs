using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class CloseCommand : Command
    {
        public CloseCommand() : base()
        {
            this.Name = "close";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Close(this.SecondWord);
            }
            else
            {
                player.ErrorMessage("\nClose what?");
            }
            player.LocationMessage("\n" + player.CurrentRoom.Description());
            return false;
        }

    }
}
