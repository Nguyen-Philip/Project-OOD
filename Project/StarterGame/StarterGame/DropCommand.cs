using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class DropCommand : Command
    {
        public DropCommand() : base()
        {
            this.Name = "drop";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Drop(this.SecondWord);
                player.OutputMessage("\n" + player.CurrentRoom.Description());
            }
            else
            {
                player.OutputMessage("\nDrop what?");
                player.OutputMessage("\n" + player.CurrentRoom.Description());
            }
            return false;
        }

    }
}
