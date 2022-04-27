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
        private Dictionary<string, Chest> _chests;
        /*private Dictionary<string, Gem> _gem;
        private Dictionary<string, Armor> _armor;
        private Dictionary<string, Enemy> _enemy;
        private Dictionary<string, NPC> _npc;
        private Dictionary<string, Potion> _potion;
        private Dictionary<string, Weapon> _weapon;
        */
        private Dictionary<string, Item> _items;

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
            _chests = new Dictionary<string, Chest>();
            /*_gem = new Dictionary<string, Gem>();
            _armor = new Dictionary<string, Armor>();
            _enemy = new Dictionary<string, Enemy>();
            _npc = new Dictionary<string, NPC>();
            _potion = new Dictionary<string, Potion>();
            _weapon = new Dictionary<string, Weapon>();
            */
            _items = new Dictionary<string, Item>();
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

        public void SetChest(string name, Chest chest)
        {
            if (chest != null)
            {
                _chests[name] = chest;
            }
            else
            {
                _chests.Remove(name);
            }
        }

        public Chest GetChest(string name)
        {
            Chest chest = null;
            _chests.TryGetValue(name, out chest);
            return chest;
        }

        public string GetChests()
        {
            string names = "";
            Dictionary<string, Chest>.KeyCollection keys = _chests.Keys;
            foreach (string name in keys)
            {
                names += " " + name;
            }

            return names;
        }

        public void SetItem(string name, Item item)
        {
            if (item != null)
            {
                _items[name] = item;
            }
            else
            {
                _items.Remove(name);
            }
        }

        public Item GetItem(string name)
        {
            Item item = null;
            _items.TryGetValue(name, out item);
            return item;
        }

        public void RemoveItem(string name)
        {
            _items.Remove(name);
        }

        public string GetItems()
        {
            string names = "";
            Dictionary<string, Item>.KeyCollection keys = _items.Keys;
            foreach (string name in keys)
            {
                names += " " + name;
            }

            return names;
        }

        /*public void SetArmor(string name, Armor armor)
        {
            if (armor != null)
            {
                _armor[name] = armor;
            }
            else
            {
                _armor.Remove(name);
            }
        }

        public Armor GetArmor(string name)
        {
            Armor armor = null;
            _armor.TryGetValue(name, out armor);
            return armor;
        }

        public string GetArmors()
        {
            string names = "";
            Dictionary<string, Armor>.KeyCollection keys = _armor.Keys;
            foreach (string name in keys)
            {
                names += " " + name;
            }

            return names;
        }

        public void SetPotion(string name, Potion potion)
        {
            if (potion != null)
            {
                _potion[name] = potion;
            }
            else
            {
                _potion.Remove(name);
            }
        }

        public Potion GetPotion(string name)
        {
            Potion potion = null;
            _potion.TryGetValue(name, out potion);
            return potion;
        }

        public string GetPotions()
        {
            string names = "";
            Dictionary<string, Potion>.KeyCollection keys = _potion.Keys;
            foreach (string name in keys)
            {
                names += " " + name;
            }

            return names;
        }

        public void SetWeapon(string name, Weapon weapon)
        {
            if (weapon != null)
            {
                _weapon[name] = weapon;
            }
            else
            {
                _weapon.Remove(name);
            }
        }

        public Weapon GetWeapon(string name)
        {
            Weapon weapon = null;
            _weapon.TryGetValue(name, out weapon);
            return weapon;
        }

        public string GetWeapons()
        {
            string names = "";
            Dictionary<string, Weapon>.KeyCollection keys = _weapon.Keys;
            foreach (string name in keys)
            {
                names += " " + name;
            }

            return names;
        }
        */
        public string SearchRoom()
        {
            return "You are " + this.Tag + ".\n *** Findings:" + this.GetChests() + this.GetItems(); 
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
