using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class UseCommand : Command
    {
        public UseCommand() : base()
        {
            this.Name = "use";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Use(this.SecondWord);
            }
            else
            {
                player.ErrorMessage("\nUse with what?");
            }
            player.LocationMessage("\n" + player.CurrentRoom.Description());
            return false;
        }

    }
}
