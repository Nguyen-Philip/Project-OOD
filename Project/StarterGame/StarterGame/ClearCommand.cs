using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class ClearCommand : Command
    {
        public ClearCommand() : this(new CommandWords()) { }
        // Designated Constructor
        public ClearCommand(CommandWords commands) : base()
        {
            this.Name = "clear";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.OutputMessage("\nClear does not contain " + this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nYou suddenly forget forgot everything you have done:\n");
                player.ClearLog();
            }
            return false;
        }
    }
}
