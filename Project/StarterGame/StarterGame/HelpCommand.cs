using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class HelpCommand : Command
    {
        private CommandWords _words;

        public HelpCommand() : this(new CommandWords()){}

        // Designated Constructor
        public HelpCommand(CommandWords commands) : base()
        {
            _words = commands;
            this.Name = "help";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.OutputMessage("\nHelp does not contain " + this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nYou are lost. You are alone. You are not ready to face the dungeon, \n\nYour available commands are: " + _words.Description() + "\n");
                player.OutputMessage(player.CurrentRoom.Description());
            }
            return false;
        }
    }
}
