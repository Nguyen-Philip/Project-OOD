using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class OpenCommand : Command
    {
        public OpenCommand() : base()
        {
            this.Name = "open";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Open(this.SecondWord);
            }
            else
            {
                player.ErrorMessage("\nOpen what?");
            }
            player.LocationMessage("\n" + player.CurrentRoom.Description());
            return false;
        }

    }
}
