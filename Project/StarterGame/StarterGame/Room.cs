using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace StarterGame
{
    /*public interface IRoomDelegate
    {
        Door GetExit(string exitName);
        string GetExits();
        string Description();
        Room ContainingRoom { set; get; }
        Dictionary<string, Door> ContainingRoomExits { set; get; }
    }*/

    public class TrapRoom : IRoomDelegate
    {
        private string unlockword;
        public Room ContainingRoom { set; get; }
        public Dictionary<string, Door> ContainingRoomExits { set; get; }
        public TrapRoom() : this("test") { }

        public TrapRoom(string theword)
        {
            unlockword = theword;
            NotificationCenter.Instance.AddObserver("PlayerSaidWord", PlayerSaidWord);
        }

        public Door GetExit(string exitName)
        {
            return null;
        }

        public string GetExits()
        {
            return "You are trapped.";
        }

        public string Description()
        {
            return "You are in " + ContainingRoom.Tag + ".\nYou have entered a trap room. " + "\n" + GetExits();
        }

        public void PlayerSaidWord(Notification notification)
        {
            Player player = (Player)notification.Object;
            if(player.CurrentRoom == ContainingRoom)
            {
                Dictionary<string, Object> userInfo = notification.UserInfo;
                string word = (string)userInfo["word"];
                if(word.Equals(unlockword))
                {
                    ContainingRoom.Delegate = null;
                    player.OutputMessage("You said the correct word.");
                }
                else
                {
                    player.OutputMessage("You said the wrong word.");
                }
            }
        }
    }

    public class EchoRoom : IRoomDelegate
    {
        public Room ContainingRoom { set; get; }
        public Dictionary<string, Door> ContainingRoomExits { set; get; }

        public EchoRoom()
        {
            NotificationCenter.Instance.AddObserver("PlayerSaidWord", PlayerSaidWord);
        }

        public Door GetExit(string exitName)
        {
            Door room = null;
            ContainingRoomExits.TryGetValue(exitName, out room);
            return room;
        }

        public string GetExits()
        {
            string exitNames = "Exits: ";
            Dictionary<string, Door>.KeyCollection keys = ContainingRoomExits.Keys;
            foreach(string exitName in keys)
            {
                exitNames += " " + exitName;
            }

            return exitNames;
        }

        public string Description()
        {
            return "You are in " + ContainingRoom.Tag + ".\nYou have entered an echo room. " + "\n" + GetExits();
        }

        public void PlayerSaidWord(Notification notification)
        {
            Player player = (Player)notification.Object;
            if(ContainingRoom == player.CurrentRoom)
            {
                Dictionary<string, object> userInfo = notification.UserInfo;
                string word = (string)userInfo["word"];
                player.OutputMessage("\n" + word + "... " + word + "... " + word + "...\n");
            }
        }
    } 

    public class Room
    {
        private Dictionary<string, Door> _exits;
        private string _tag;

        public string Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }

        private IRoomDelegate _delegate;

        public IRoomDelegate Delegate
        {
            set
            {
                _delegate = value;
                if(value != null)
                {
                    _delegate.ContainingRoom = this;
                    _delegate.ContainingRoomExits = _exits;
                }
            }
            get
            {
                return _delegate;
            }
        }

        public Room() : this("No Tag"){}

        // Designated Constructor
        public Room(string tag)
        {
            Delegate = null;
            _exits = new Dictionary<string, Door>();
            this.Tag = tag;
        }

        //Set Room Exits
        public void SetExit(string exitName, Door room)
        {
            if (room != null)
            {
                _exits[exitName] = room;
            }
            else
            {
                _exits.Remove(exitName);
            }
        }

        //Get Room Exits
        public Door GetExit(string exitName)
        {
            if(Delegate == null)
            {
                Door room = null;
                _exits.TryGetValue(exitName, out room);
                return room;
            }
            else
            {
                return Delegate.GetExit(exitName);
            }
        }

        //Get Room Exits
        public string GetExits()
        {
            if (Delegate == null)
            {
                string exitNames = "Exits:";
                Dictionary<string, Door>.KeyCollection keys = _exits.Keys;
                foreach (string exitName in keys)
                {
                    exitNames += " " + exitName;
                }

                return exitNames;
            }
            else
            {
                return Delegate.GetExits();
            }
        }
        
        //Get Room Description
        public string Description()
        {
            if (Delegate == null)
            {
                return "You are " + this.Tag + ".\n *** " + this.GetExits();
            }
            else
            {
                return Delegate.Description();
            }
        }
    }
}
