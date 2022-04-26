using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class LockCommand : Command
    {
        public LockCommand() : base()
        {
            this.Name = "lock";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Lock(this.SecondWord);
            }
            else
            {
                player.ErrorMessage("\nOpen what?");
                player.LocationMessage("\n" + player.CurrentRoom.Description());
            }
            return false;
        }

    }
}
