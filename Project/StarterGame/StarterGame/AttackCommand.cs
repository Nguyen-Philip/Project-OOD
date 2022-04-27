using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class AttackCommand : Command
    {
        public AttackCommand() : base()
        {
            this.Name = "attack";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Attack(this.SecondWord);
            }
            else
            {
                player.ErrorMessage("\nAttack who?");
                player.LocationMessage("\n" + player.CurrentRoom.Description());
            }
            return false;
        }

    }
}
