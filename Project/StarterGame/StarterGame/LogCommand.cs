using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class LogCommand : Command
    {
        public LogCommand() : this(new CommandWords()) { }
        // Designated Constructor
        public LogCommand(CommandWords commands) : base()
        {
            this.Name = "log";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.OutputMessage("\nLog does not contain " + this.SecondWord);
                player.OutputMessage("\n" + player.CurrentRoom.Description());
            }
            else
            {
                player.OutputMessage("\nYou suddenly remember everything you have done:\n");
                player.ShowLog();
            }
            return false;
        }
    }
}
