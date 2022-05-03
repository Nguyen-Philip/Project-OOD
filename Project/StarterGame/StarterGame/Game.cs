using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class Game
    {
        private Player _player;
        private Parser _parser;
        private bool _playing;

        public Game()
        {
            _playing = false;
            _parser = new Parser(new CommandWords());
            _player = new Player(GameWorld.Instance().Entrance);
        }

        /**
        *  Main play routine.  Loops until end of play.
         */
        public void Play()
        {

            // Enter the main command loop.  Here we repeatedly read commands, input commands into a log and
            // execute them until the game is over.

            bool finished = false;
            while (!finished)
            {
                Console.Write("\n>");
                String temp = Console.ReadLine();
                Command command = _parser.ParseCommand(temp);
                if (command == null)
                {
                    Console.WriteLine("\nI don't understand...");
                }
                else
                {
                    _player.InputLog(temp);
                    if(command.Name == "go")
                    {
                        _player.InputMovementLog(temp);
                    }
                    finished = command.Execute(_player);
                }
            }
        }

        //starts the program
        public void Start()
        {
            _playing = true;
            _player.OutputMessage(Welcome());
            _player.NotificationMessage(Goal());
            _player.LocationMessage(_player.CurrentRoom.Description());

        }

        //restarts the program
        public void Restart()
        {
            _playing = false;
            _parser = new Parser(new CommandWords());
            _player = new Player(GameWorld.Instance().Entrance);
            Start();
            Play();
            End();
            
        }

        //ends the program
        public void End()
        {
            _playing = false;
            _player.OutputMessage(Goodbye());
        }

        //prints a welcome message when Start() is called
        public string Welcome()
        {
            return "Welcome to ###.\n\nThe World of ### is an exciting adventure game.\n\nType 'help' if you need help.\n";
        }

        public string Goal()
        {
            return "Your goal is to defeat the dragon and enter it's treasure room\n";
        }

        //prints a goodbye message when End() is called
        public string Goodbye()
        {
            return "\nThank you for playing, Goodbye. \n";
        }
    }
}
