using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class EquipCommand : Command
    {
        public EquipCommand() : base()
        {
            this.Name = "equip";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Equip(this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nEquip what?");
            }
            return false;
        }

    }
}
