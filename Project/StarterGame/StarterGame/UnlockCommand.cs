﻿using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class UnlockCommand : Command
    {
        public UnlockCommand() : base()
        {
            this.Name = "unlock";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Unlock(this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nUnlock what?");
                player.OutputMessage("\n" + player.CurrentRoom.Description());
            }
            return false;
        }

    }
}
