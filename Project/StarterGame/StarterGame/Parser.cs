using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class Parser
    {
        private CommandWords _commands;

        public Parser() : this(new CommandWords()){}

        // Designated Constructor
        public Parser(CommandWords newCommands)
        {
            _commands = newCommands;
        }

        public Command ParseCommand(string commandString)
        {
            Command command = null;
            string[] words = commandString.Split(' ');
            if (words.Length > 0)
            {
                command = _commands.Get(words[0]);
                if (command != null)
                {
                    if (words.Length > 1)
                    {
                        command.SecondWord = words[1];
                    }
                    else
                    {
                        command.SecondWord = null;
                    }
                }
            }
            else
            {
                // This is a debug line of code
                Console.WriteLine("\nNo words parsed!");
            }
            return command;
        }

        public string Description()
        {
            return _commands.Description();
        }
    }
}
