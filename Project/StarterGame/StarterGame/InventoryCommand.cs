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
                player.ErrorMessage("\nInventory does not contain " + this.SecondWord);
                player.LocationMessage("\n" + player.CurrentRoom.Description());
            }
            else
            {
                player.NotificationMessage("\nYou look in your backpack");
                player.Inventory();
            }
            return false;
        }
    }
}
