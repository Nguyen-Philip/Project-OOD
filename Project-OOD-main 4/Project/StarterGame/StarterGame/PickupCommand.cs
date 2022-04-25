using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class PickupCommand : Command
    {
        public PickupCommand() : base()
        {
            this.Name = "pickup";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Pickup(this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nPick up what?");
            }
            return false;
        }

    }
}
