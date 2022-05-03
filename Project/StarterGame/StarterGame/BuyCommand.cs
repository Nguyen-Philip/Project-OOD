using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class BuyCommand : Command
    {
        public BuyCommand() : base()
        {
            this.Name = "buy";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Buy(this.SecondWord);
            }
            else
            {
                player.ErrorMessage("\nBuy what?");
            }
            player.LocationMessage("\n" + player.CurrentRoom.Description());
            return false;
        }
    }
}