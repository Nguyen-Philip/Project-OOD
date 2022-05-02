using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace StarterGame
{

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
                    player.NotificationMessage("\nYou said the correct word.");
                }
                else
                {
                    player.ErrorMessage("\nYou said the wrong word.");
                }
            }
        }
    }

    public class TownRoom : IRoomDelegate
    {
        private string unlockword;
        public Room ContainingRoom { set; get; }
        public Dictionary<string, Door> ContainingRoomExits { set; get; }

        private Room _room;
        public Key _key;

        public TownRoom(string theword, Room room)
        {
            unlockword = theword;
            _room = room;
            NotificationCenter.Instance.AddObserver("PlayerSaidWord", PlayerSaidWord);
        }

        public Door GetExit(string exitName)
        {
            return null;
        }
        public string GetExits()
        {
            return "You are in town, the door of the dungeon is locked. Say ready then pickup the key";
        }

        public string Description()
        {
            return "You are in " + ContainingRoom.Tag + ".\nThe door to the dungeon is locked." + "\n" + GetExits();
        }

        public void PlayerSaidWord(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (player.CurrentRoom == ContainingRoom)
            {
                Dictionary<string, Object> userInfo = notification.UserInfo;
                string word = (string)userInfo["word"];
                if (word.Equals(unlockword))
                {
                    ContainingRoom.Delegate = null;
                    player.NotificationMessage("\nYou are ready, the guard dropped the key.");
                    Key.CreateKey(_room, "masterdoorkey");

                }
                else
                {
                    player.ErrorMessage("\nSay ready when you are ready");
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
                player.SayMessage("\n" + word + "... " + word + "... " + word + "...\n");
            }
        }
    } 

    public class Room
    {
        private Dictionary<string, Door> _exits;
        private Dictionary<string, Item> _chests;
        private Dictionary<string, KeyItem> _keyitems;
        private Dictionary<string, Item> _items;
        private Dictionary<string, NPC> _npcs;
        private Dictionary<string, Enemy> _enemies;

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
            _chests = new Dictionary<string, Item>();;
            _keyitems = new Dictionary<string, KeyItem>();
            _items = new Dictionary<string, Item>();
            _npcs = new Dictionary<string, NPC>();
            _enemies = new Dictionary<string, Enemy>();
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

        public Item GetChest(string name)
        {
            Item chest = null;
            _chests.TryGetValue(name, out chest);
            return chest;
        }

        public void RemoveChest(String name)
        {
            _chests.Remove(name);
        }

        public string GetChests()
        {
            string names = "";
            Dictionary<string, Item>.KeyCollection keys = _chests.Keys;
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

        public void RemoveItem(String name)
        {
            Item item = this.GetItem(name);
            item.Num -= 1;
            if (item.Num <= 0)
            {
                _items.Remove(name);
            }
        }

        public string GetItems()
        {
            string names = "";
            Dictionary<string, Item>.KeyCollection keys = _items.Keys;
            foreach (string name in keys)
            {
                names += " " + name + "[" + _items[name].Num + "]";
            }

            return names;
        }

        public void SetNPC(string name, NPC npc)
        {
            if (npc != null)
            {
                _npcs[name] = npc;
            }
            else
            {
                _npcs.Remove(name);
            }
        }

        public NPC GetNPC(string name)
        {
            NPC npc = null;
            _npcs.TryGetValue(name, out npc);
            return npc;
        }

        public string GetNPCs()
        {
            string names = "";
            Dictionary<string, NPC>.KeyCollection keys = _npcs.Keys;
            foreach (string name in keys)
            {
                names += " " + name;
            }

            return names;
        }

        public void SetEnemy(string name, Enemy enemy)
        {
            if (enemy != null)
            {
                _enemies[name] = enemy;
            }
            else
            {
                _enemies.Remove(name);
            }
        }

        public Enemy GetEnemy(string name)
        {
            Enemy enemy = null;
            _enemies.TryGetValue(name, out enemy);
            return enemy;
        }

        public string GetEnemies()
        {
            string names = "";
            Dictionary<string, Enemy>.KeyCollection keys = _enemies.Keys;
            foreach (string name in keys)
            {
                names += " " + name;
            }

            return names;
        }

        public void RemoveEnemy(String name)
        {
            _enemies.Remove(name);
        }

        public void SetKeyItem(string name, KeyItem keyitem)
        {
            if (keyitem != null)
            {
                _keyitems[name] = keyitem;
            }
            else
            {
                _keyitems.Remove(name);
            }
        }

        public KeyItem GetKeyItem(string name)
        {
            KeyItem keyitem = null;
            _keyitems.TryGetValue(name, out keyitem);
            return keyitem;
        }

        public void RemoveKeyItems(string name)
        {
            _keyitems.Remove(name);
        }

        public string GetKeyItems()
        {
            string names = "";
            Dictionary<string, KeyItem>.KeyCollection keys = _keyitems.Keys;
            foreach (string name in keys)
            {
                names += " " + name;
            }

            return names;
        }

        public string SearchRoom()
        {
            return " *** Chests:" + this.GetChests() + "\n *** Items:" + this.GetItems() + "\n *** Key Items:" + this.GetKeyItems() + "\n *** NPCs:" + this.GetNPCs() + "\n *** Enemies:" + this.GetEnemies();
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
