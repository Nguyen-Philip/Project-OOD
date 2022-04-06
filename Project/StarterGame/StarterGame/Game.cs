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
        private List<string> _log = new List<string>();

        public Game()
        {
            _playing = false;
            _parser = new Parser(new CommandWords());
            _player = new Player(GameWorld.Instance().Entrance);
            _log = new List<string>();
        }

        /**
     *  Main play routine.  Loops until end of play.
     */
        public void Play()
        {

            // Enter the main command loop.  Here we repeatedly read commands and
            // execute them until the game is over.

            bool finished = false;
            while (!finished)
            {
                Console.Write("\n>");
                String temp = Console.ReadLine();
                Command command = _parser.ParseCommand(temp);
                if (command == null)
                {
                    Console.WriteLine("I don't understand...");
                }
                else
                {
                    _log.Add(temp);
                    finished = command.Execute(_player);
                }
            }
        }


        public void Start()
        {
            _playing = true;
            _player.OutputMessage(Welcome());

        }

        public void Restart()
        {
            _playing = false;
            Start();
        }
        public void End()
        {
            _playing = false;
            _player.OutputMessage(Goodbye());
        }
        public void GetLog()
        {
            foreach (string loggedCommand in _log)
            {
                Console.WriteLine(loggedCommand);
            }
        }

        public string Welcome()
        {
            return "Welcome to ###.\n\nThe World of ### is an exciting adventure game.\n\nType 'help' if you need help. " + _player.CurrentRoom.Description();
        }

        public string Goodbye()
        {
            return "\nThank you for playing, Goodbye. \n";
        }
    }
}
