using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class BackCommand : Command
    {
        public BackCommand() : base()
        {
            this.Name = "back";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.OutputMessage("\nBack does not contain " + this.SecondWord);
                player.OutputMessage("\n" + player.CurrentRoom.Description());
            }
            else
            {
                player.WaltBack();
            }
            return false;
        }

    }
}
