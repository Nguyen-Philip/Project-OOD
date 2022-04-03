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
                player.WaltBack(this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nGo Where?");
            }
            return false;
        }

    }
}
