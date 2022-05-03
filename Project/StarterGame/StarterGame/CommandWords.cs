using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class CommandWords
    {
        private Dictionary<string, Command> commands;
        private static Command[] commandArray = { new GoCommand(), new BackCommand(), new QuitCommand(), new SayCommand(), new PickupCommand(), new DropCommand(), new CloseCommand(), new OpenCommand(), new LockCommand(), new UnlockCommand(), new SpeaktoCommand(), new AttackCommand(), new EquipCommand(), new UnequipCommand(), new InspectCommand(), new UseCommand(), new BuyCommand(), new SellCommand() };
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
            Command inventory = new InventoryCommand(this);
            commands[inventory.Name] = inventory;
            Command map = new MapCommand(this);
            commands[map.Name] = map;
            Command stats = new StatsCommand(this);
            commands[stats.Name] = stats;
            Command browse = new BrowseCommand(this);
            commands[browse.Name] = browse;
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
