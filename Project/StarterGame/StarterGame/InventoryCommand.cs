using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class InventoryCommand : Command
    {
        public InventoryCommand() : this(new CommandWords()) { }
        // Designated Constructor
        public InventoryCommand(CommandWords commands) : base()
        {
            this.Name = "inventory";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.OutputMessage("\nInventory does not contain " + this.SecondWord);
                player.OutputMessage("\n" + player.CurrentRoom.Description());
            }
            else
            {
                player.OutputMessage("\nYou look in your backpack");
                player.Inventory();
                player.OutputMessage("\n" + player.CurrentRoom.Description());
            }
            return false;
        }
    }
}
