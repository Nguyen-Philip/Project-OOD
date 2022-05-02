using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class UnequipCommand : Command
    {
        public UnequipCommand() : base()
        {
            this.Name = "unequip";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Unequip(this.SecondWord);
            }
            else
            {
                player.ErrorMessage("\nEquip what?");
            }
            player.LocationMessage("\n" + player.CurrentRoom.Description());
            return false;
        }

    }
}