using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace StarterGame
{
    public interface IRoomDelegate
    {
        Room GetExit(string exitName);
        string GetExit();
        string Description();
        Room ContainingRoom { set; get; }
    }

    public class TrapRoom : IRoomDelegate
    {
        private string unlockword;
        public Room ContainingRoom { set; get; }
        public TrapRoom() : this("test") { }
        private Dictionary<string, Item> _items;

        public bool Add(Item item)
        {
            bool success = false;
            _items.Add(item.Name, item);

            return success;
        }
        public bool Remove(string item)
        {
            bool success = _items.Remove(item);
            return success;
        }
        public string Inventory
        {
            get
            {
                string inventory = "";
                foreach (Item item in _items.Values)
                {
                    inventory += item + "\n";
                }

                return inventory;
            }
        }

        public TrapRoom(string theword)
        {
            unlockword = theword;
            NotificationCenter.Instance.AddObserver("PlayerSaidWord", PlayerSaidWord);
        }

        public Room GetExit(string exitName)
        {
            return null;
        }

        public string GetExit()
        {
            return "You are trapped.";
        }

        public string Description()
        {
            return "You are in " + ContainingRoom.Tag + ".\nYou have entered a trap room. " + "\n" + GetExit(); ;
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
                    player.OutputMessage("The trap is cleared.");
                }
                else
                {
                    player.OutputMessage("You said the wrong word.");
                    player.OutputMessage("The trap has not been cleared.");
                }
            }
        }

    }

    public class Room
    {
        private Dictionary<string, Room> _exits;
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
            _exits = new Dictionary<string, Room>();
            this.Tag = tag;
        }

        //Set Room Exits
        public void SetExit(string exitName, Room room)
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
        public Room GetExit(string exitName)
        {
            if(Delegate == null)
            { 
                Room room = null;
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
                Dictionary<string, Room>.KeyCollection keys = _exits.Keys;
                foreach (string exitName in keys)
                {
                    exitNames += " " + exitName;
                }

                return exitNames;
            }
            else
            {
                return Delegate.GetExit();
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
