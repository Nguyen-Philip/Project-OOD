using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class RestartCommand : Command
    {
        private CommandWords _words;

        public RestartCommand() : this(new CommandWords()){}

        // Designated Constructor
        public RestartCommand(CommandWords commands) : base()
        {
            _words = commands;
            this.Name = "restart";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.OutputMessage("\nRestart does not contain " + this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nSpace and time starts warping around you.\n");
                player.RestartGame();
            }
            return false;
        }
    }
}
