using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class SellCommand : Command
    {
        public SellCommand() : base()
        {
            this.Name = "sell";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Sell(this.SecondWord);
            }
            else
            {
                player.ErrorMessage("\nSell what?");
            }
            player.LocationMessage("\n" + player.CurrentRoom.Description());
            return false;
        }
    }
}