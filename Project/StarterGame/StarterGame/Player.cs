using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class Player
    {


        private Room _currentRoom = null;
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

        public void OutputMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void Log()
        {
            Game _game = new Game();
            _game.GetLog();
        }
    }

}
