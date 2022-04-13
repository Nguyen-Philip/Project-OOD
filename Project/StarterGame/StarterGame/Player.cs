using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class Player
    {
        private Room _currentRoom = null;
        private List<string> _log = new List<string>();
        private List<string> _movementlog = new List<string>();
        public Room CurrentRoom
        {
            get
            {
                return _currentRoom;
            }
            set
            {
                _currentRoom = value;
            }
        }

        public Player(Room room)
        {
            _currentRoom = room;
        }

        //used by GoCommand, move to next room
        public void WaltTo(string direction)
        {
            Door door = this.CurrentRoom.GetExit(direction);
            if(door != null)
            {
                if (door.IsOpen)
                {
                    Room nextRoom = door.GetRoomOnTheOtherSide(this.CurrentRoom);
                    Notification notification = new Notification("PlayerWillEnterRoom", this);
                    NotificationCenter.Instance.PostNotification(notification);
                    this.CurrentRoom = nextRoom;
                    notification = new Notification("PlayerDidEnterRoom", this);
                    NotificationCenter.Instance.PostNotification(notification);
                    this.OutputMessage("\n" + this.CurrentRoom.Description());
                }
                else
                {
                    this.OutputMessage("\nThe door is locked");
                }
            }
            else
            {
                this.OutputMessage("\nThere is no door on " + direction);
            }
        }

        //used by BackCommand, move to previous room
        public void WaltBack()
        {
            Parser _parser = new Parser(new CommandWords());
            string movementLog = "";
            if(_movementlog.Count > 0)
            {
                movementLog = _movementlog[_movementlog.Count - 1];
            }

            if(_movementlog.Count != 0)
            {
                Command command = _parser.ParseCommand(movementLog);
                switch (command.SecondWord)
                {
                    case "north":
                        command.SecondWord = "south";
                        this.WaltTo(command.SecondWord);
                        _movementlog.RemoveAt(_movementlog.Count - 1);
                        break;
                    case "south":
                        command.SecondWord = "north";
                        this.WaltTo(command.SecondWord);
                        _movementlog.RemoveAt(_movementlog.Count - 1);
                        break;
                    case "west":
                        command.SecondWord = "east";
                        this.WaltTo(command.SecondWord);
                        _movementlog.RemoveAt(_movementlog.Count - 1);
                        break;
                    case "east":
                        command.SecondWord = "west";
                        this.WaltTo(command.SecondWord);
                        _movementlog.RemoveAt(_movementlog.Count - 1);
                        break;
                    default:
                        this.OutputMessage("There is no way to go back");
                        this.OutputMessage("\n" + this.CurrentRoom.Description());
                        break;
                }
            }
            else
            {
                this.OutputMessage("There is no way to go back");
                this.OutputMessage("\n" + this.CurrentRoom.Description());
            }
        }

        //
        public void Say(string word)
        {
            OutputMessage("\n" + word);
            Dictionary<string, object> userInfo = new Dictionary<string, object>();
            userInfo["word"] = word;
            Notification notification = new Notification("PlayerSaidWord", this, userInfo);
            NotificationCenter.Instance.PostNotification(notification);
            this.OutputMessage("\n" + this.CurrentRoom.Description());
        }

        public void Open(string exitName)
        {

        }

        //prints a message
        public void OutputMessage(string message)
        {
            Console.WriteLine(message);
        }

        //gets a string input to put into _log
        public void InputLog(string command)
        {
            _log.Add(command);
        }

        //used by LogCommand, shows the log
        public void ShowLog()
        {
            foreach (string loggedCommand in _log)
            {
                Console.WriteLine(loggedCommand);
            }
            this.OutputMessage("\n" + this.CurrentRoom.Description());
        }

        public void InputMovementLog(string command)
        {
            _movementlog.Add(command);
        }

        //used by ClearLog(), clears the log
        public void ClearLog()
        {
            _log.Clear();
            this.OutputMessage(this.CurrentRoom.Description());
        }

        //used by RestartCommand, restarts the program and clears the log
        public void RestartGame()
        {
            _log.Clear();
        }
    }
}
