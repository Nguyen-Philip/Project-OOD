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
                player.ErrorMessage("\nBack does not contain " + this.SecondWord);
            }
            else
            {
                player.WaltBack();
            }
            player.LocationMessage("\n" + player.CurrentRoom.Description());
            return false;
        }

    }
}
