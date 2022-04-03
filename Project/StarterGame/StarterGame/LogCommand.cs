using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class LogCommand : Command
    {
        private CommandWords _words;

        public LogCommand() : this(new CommandWords()){}

        // Designated Constructor
        public LogCommand(CommandWords commands) : base()
        {
            _words = commands;
            this.Name = "log";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.OutputMessage("\nLog does not contain " + this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nYou suddenly remember everything you have done:\n" + _words.Log());
            }
            return false;
        }
    }
}
