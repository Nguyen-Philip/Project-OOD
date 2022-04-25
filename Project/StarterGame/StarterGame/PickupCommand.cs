﻿using System.Collections;
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
                player.Inventory();
                player.OutputMessage("\n" + player.CurrentRoom.Description());
            }
            else
            {
                player.OutputMessage("\nPick up what?");
                player.OutputMessage("\n" + player.CurrentRoom.Description());
            }
            return false;
        }

    }
}
