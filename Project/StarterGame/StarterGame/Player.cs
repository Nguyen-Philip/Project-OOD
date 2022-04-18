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
        private BackPack _backPack = new BackPack();

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
                    this.OutputMessage("\n" + this.CurrentRoom.Description());
                }
            }
            else
            {
                if (direction == "portal")
                {
                    this.OutputMessage("\nThere is no " + direction);
                    this.OutputMessage("\n" + this.CurrentRoom.Description());
                }
                else
                {
                    this.OutputMessage("\nThere is no door on " + direction);
                    this.OutputMessage("\n" + this.CurrentRoom.Description());
                }
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

        public void Pickup(string word)
        {
            Item pItem = this.CurrentRoom.Remove(word);
            if (pItem != null)
            {
                _backPack.Add(pItem);
                this.OutputMessage("\nYou have picked up " + word);

            }
            else
            {
                this.OutputMessage("\nThere is no " + word);
            }
        }

        public void Drop(string word)
        {
            Item pItem = _backPack.Remove(word);
            if (pItem != null)
            {
                this.CurrentRoom.Add(pItem);
                this.OutputMessage("\nYou have droped " + word + " on the ground of the room");

            }
            else
            {
                this.OutputMessage("\nThere is no " + word);
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
            Door door = this.CurrentRoom.GetExit(exitName);
            if (exitName != "portal")
            {
                if (door != null)
                {
                    if (door.IsClosed)
                    {
                        door.Open();
                        this.OutputMessage("\nThe door has been opened");
                        this.OutputMessage("\n" + this.CurrentRoom.Description());
                    }
                    else
                    {
                        this.OutputMessage("\nThe door is not locked");
                        this.OutputMessage("\n" + this.CurrentRoom.Description());
                    }
                }
            }
            else if (exitName == "portal")
            {
                this.OutputMessage("\nYou cannot open a portal");
            }
            else
            {
                this.OutputMessage("\nThere is no door on " + exitName + " to open");
            }
        }

        public void Close(string exitName)
        {
            Door door = this.CurrentRoom.GetExit(exitName);
            if (exitName != "portal")
            {
                if (door != null)
                {
                    if (door.IsOpen)
                    {
                        door.Close();
                        this.OutputMessage("\nThe door has been locked");
                        this.OutputMessage("\n" + this.CurrentRoom.Description());
                    }
                    else
                    {
                        this.OutputMessage("\nThe door is not open");
                        this.OutputMessage("\n" + this.CurrentRoom.Description());
                    }
                }
            }
            else if(exitName == "portal")
            {
                this.OutputMessage("\nYou cannot close a portal");
            }
            else
            {
                this.OutputMessage("\nThere is no door on " + exitName + " to close");
            }
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
