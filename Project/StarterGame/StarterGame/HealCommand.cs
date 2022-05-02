using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class HealCommand : Command
    {
        public HealCommand() : base()
        {
            this.Name = "heal";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Heal(this.SecondWord);
            }
            else
            {
                player.ErrorMessage("\nHeal with what?");
                player.LocationMessage("\n" + player.CurrentRoom.Description());
            }
            return false;
        }

    }
}
