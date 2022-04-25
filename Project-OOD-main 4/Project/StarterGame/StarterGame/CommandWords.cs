﻿using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class CommandWords
    {
        private Dictionary<string, Command> commands;
        private static Command[] commandArray = { new GoCommand(), new BackCommand(), new QuitCommand(), new SayCommand(), new PickupCommand(), new DropCommand(), new CloseCommand(), new OpenCommand(), new LockCommand(), new UnlockCommand() };
        public CommandWords() : this(commandArray) {}

        // Designated Constructor
        public CommandWords(Command[] commandList)
        {
            commands = new Dictionary<string, Command>();
            foreach (Command command in commandList)
            {
                commands[command.Name] = command;
            }
            Command help = new HelpCommand(this);
            commands[help.Name] = help;
            Command log = new LogCommand(this);
            commands[log.Name] = log;
            Command restart = new RestartCommand(this);
            commands[restart.Name] = restart;
            Command clear = new ClearCommand(this);
            commands[clear.Name] = clear;
            Command search = new SearchCommand(this);
            commands[search.Name] = search;
        }

        public Command Get(string word)
        {
            Command command = null;
            commands.TryGetValue(word, out command);
            return command;
        }

        public string Description()
        {
            string commandNames = "";
            Dictionary<string, Command>.KeyCollection keys = commands.Keys;
            foreach (string commandName in keys)
            {
                commandNames += commandName + " ";
            }
            return commandNames;
        }
    }
}
