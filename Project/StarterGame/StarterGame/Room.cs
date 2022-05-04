using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace StarterGame
{
    //say a certain word to unlock traproom
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
            return "You are in " + ContainingRoom.Tag + ".\nYou have entered a trap room. " + "\n" + GetExits() + "\nSay the correct word to escape: yes no help open magic dragon unlock";
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
                    player.NotificationMessage("\nYou took " + 5 + " damage");
                    player.Hp -= 5;
                    player.NotificationMessage("\nHP: " + player.Hp);
                }
            }
        }
    }

    //whenever the player is ready, drop items
    public class TownRoom : IRoomDelegate
    {
        private string unlockword;
        public Room ContainingRoom { set; get; }
        public Dictionary<string, Door> ContainingRoomExits { set; get; }

        private Room _room;

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
            return "Say ready then pickup the keys.";
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
                    Key.CreateKey(_room, "dungeonkey", 0, "Key that unlocks entrance to the dungeon");
                    Key.CreateKey(_room, "chestkey", 0, "Key that unlocks the chest in town");
                    NPC.CreateNPC(_room, "guard", true, "Goodluck in the dungeon");
                }
                else
                {
                    player.ErrorMessage("\nSay ready when you are ready");
                }
            }
        }
    }
    //when entering a certain room, restock where ever the store is located
    public class RestockRoom : IRoomDelegate
    {
        public Room ContainingRoom { set; get; }
        public Dictionary<string, Door> ContainingRoomExits { set; get; }

        private Room _npcLocation;
        private Room _room;
        private Room _itemRoom;
        private Potion _potion;

        public RestockRoom(Room npcLocation, Room room, Room itemroom)
        {
            _npcLocation = npcLocation;
            _room = room;
            _itemRoom = itemroom;
            NotificationCenter.Instance.AddObserver("PlayerDidEnterRoom", PlayerDidEnterRoom);
        }

        public Door GetExit(string exitName)
        {
            return null;
        }
        public string GetExits()
        {
            string exitNames = "Exits: ";
            Dictionary<string, Door>.KeyCollection keys = ContainingRoomExits.Keys;
            foreach (string exitName in keys)
            {
                exitNames += " " + exitName;
            }
            return exitNames;
        }

        public string Description()
        {
            return "You are " + ContainingRoom.Tag + ".\n *** " + this.GetExits();
        }

        public void PlayerDidEnterRoom(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (player.CurrentRoom == ContainingRoom)
            {
                ContainingRoom.Delegate = null;
                _potion = Potion.CreatePotion(_itemRoom, "shealpot", 10, 1, 10, "HP");
                _npcLocation.Shop.Add(_potion);
                _potion = Potion.CreatePotion(_itemRoom, "shealpot", 10, 1, 10, "HP");
                _npcLocation.Shop.Add(_potion);
                _potion = Potion.CreatePotion(_itemRoom, "mhealpot", 25, 1, 35, "HP");
                _npcLocation.Shop.Add(_potion);
                _potion = Potion.CreatePotion(_itemRoom, "mhealpot", 25, 1, 35, "HP");
                _npcLocation.Shop.Add(_potion);
                _potion = Potion.CreatePotion(_itemRoom, "lhealpot", 50, 1, 80, "HP");
                _npcLocation.Shop.Add(_potion);
                _potion = Potion.CreatePotion(_itemRoom, "lhealpot", 50, 1, 80, "HP");
                _npcLocation.Shop.Add(_potion);
            }
        }
    }

    //echo room
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
        private Dictionary<string, List<Item>> _items;
        private Dictionary<string, NPC> _npcs;
        private Dictionary<string, Enemy> _enemies;
        public List<Room> _rooms = new List<Room>();
        public Shop _shop = new Shop();

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

        public Shop Shop
        {
            get
            {
                return _shop;
            }
            set
            {
                _shop = value;
            }
        }

        public List<Room> ListofRooms
        {
            get
            {
                return _rooms;
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
            _items = new Dictionary<string, List<Item>>();
            _npcs = new Dictionary<string, NPC>();
            _enemies = new Dictionary<string, Enemy>();
            this.Tag = tag;
        }

        //get rooms and put it into list
        public void AddRooms(Room room)
        {
            _rooms.Add(room);
        }
        //get random rooms
        public Room randomRoom()
        {
            Random rand = new Random();
            int index = rand.Next(_rooms.Count);
            return _rooms[index];
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

        //set chest in rooms
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

        //get chest in rooms
        public Item GetChest(string name)
        {
            Item chest = null;
            _chests.TryGetValue(name, out chest);
            return chest;
        }

        //remove chest in rooms
        public void RemoveChest(String name)
        {
            _chests.Remove(name);
        }
        //print string of chest in a room
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

        public bool AddItem(Item item)
        {
            bool success = false;
            bool isIn = _items.ContainsKey(item.Name);
            if (isIn)
            {
                _items[item.Name].Add(item);
            }
            else
            {
                List<Item> itemlist = new List<Item>();
                itemlist.Add(item);
                _items.Add(item.Name, itemlist);
            }
            success = true;
            return success;
        }

        public List<Item> GetItem(string name)
        {
            List<Item> items = null;
            _items.TryGetValue(name, out items);
            return items;
        }

        public void RemoveItem(String name)
        {
            List<Item> items = this.GetItem(name);
            if (items != null)
            {
                if (items.Count <= 1)
                {
                    _items.Remove(name);
                }
                else
                {
                    items.RemoveAt(0);

                }
            }
        }

        public string GetItems()
        {
            string names = "";
            Dictionary<string, List<Item>>.KeyCollection keys = _items.Keys;
            foreach (string name in keys)
            {
                names += " " + name + "[" + _items[name].Count + "]";
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
