using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class Player
    {
        private Room _currentRoom = null;
        private List<string> _log = new List<string>();
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
            Room nextRoom = this.CurrentRoom.GetExit(direction);
            if (nextRoom != null)
            {
                Notification notification = new Notification("PlayerWillEnterRoom", this);
                NotificationCenter.Instance.PostNotification(notification);
                this.CurrentRoom = nextRoom;
                notification = new Notification("PlayerDidEnterRoom", this);
                NotificationCenter.Instance.PostNotification(notification);
                this.OutputMessage("\n" + this.CurrentRoom.Description());
            }
            else
            {
                this.OutputMessage("\nThere is no door on " + direction);
            }
        }

        //used by BackCommand, move to previous room
        public void WaltBack(string direction)
        {
            Room nextRoom = this.CurrentRoom.GetExit(direction);
            if (nextRoom != null)
            {
                Notification notification = new Notification("PlayerWillEnterRoom", this);
                NotificationCenter.Instance.PostNotification(notification);
                this.CurrentRoom = nextRoom;
                notification = new Notification("PlayerDidEnterRoom", this);
                NotificationCenter.Instance.PostNotification(notification);
                this.OutputMessage("\n" + this.CurrentRoom.Description());
            }
            else
            {
                this.OutputMessage("\nThere is no door on " + direction);
            }
        }

        //
        public void Say(string word)
        {
            OutputMessage("\n" + word + "\n");
            Dictionary<string, object> userInfo = new Dictionary<string, object>();
            userInfo["word"] = word;
            Notification notification = new Notification("PlayerSaidWord", this, userInfo);
            NotificationCenter.Instance.PostNotification(notification);
            this.OutputMessage("\n" + this.CurrentRoom.Description());
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

        //used by ClearLog(), clears the log
        public void ClearLog()
        {
            _log.Clear();
            this.OutputMessage("\n" + this.CurrentRoom.Description());
        }

        //used by RestartCommand, restarts the program and clears the log
        public void RestartGame()
        {
            Game _game = new Game();
            _log.Clear();
        }
    }
}
