using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class Player
    {
        private Room _currentRoom = null;
        private List<string> _log = new List<string>();
        private Stack<string> _movementlog = new Stack<string>();
        private BackPack _backPack = new BackPack();
        private int _hp = 100;
        private int _ar = 5;
        private int _av = 0;
        private int _priority = 1;
        private Armor _armor;
        private Weapon _weapon;

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

        public bool EquipX(Item item)
        {
            bool success = false;
            if (item.GetType() == typeof(Armor))
            {
                this.NotificationMessage("\nYou have equipped the " + item.Name);
                _armor = (Armor)item;
                _av += _armor.AV;
                this.NotificationMessage("" + _av);
                _backPack.Remove(item.Name);
                success = true;
            }
            else if (item.GetType() == typeof(Weapon))
            {
                this.NotificationMessage("\nYou have equipped the " + item.Name);
                _weapon = (Weapon)item;
                _ar += _weapon.AR;
                _backPack.Remove(item.Name);
                success = true;
            }
            return success;
        }

        public bool UnequipX(Item item)
        {
            bool success = false;
            if (item.GetType() == typeof(Armor))
            {
                this.NotificationMessage("\nYou have unequipped the " + item.Name);
                _armor = (Armor)item;
                _av -= _armor.AV;
                _backPack.Add(item);
                _armor = null;
                success = true;
            }
            else if (item.GetType() == typeof(Weapon))
            {
                this.NotificationMessage("\nYou have unequipped the " + item.Name);
                _weapon = (Weapon)item;
                _ar -= _weapon.AR;
                _backPack.Add(item);
                _weapon = null;
                success = true;
            }
            return success;
        }

        //used by GoCommand, move to next room
        public void WaltTo(string direction)
        {
            Door door = this.CurrentRoom.GetExit(direction);
            if (door != null)
            {
                if (door.IsOpen)
                {
                    Room nextRoom = door.GetRoomOnTheOtherSide(this.CurrentRoom);
                    Notification notification = new Notification("PlayerWillEnterRoom", this);
                    NotificationCenter.Instance.PostNotification(notification);
                    this.CurrentRoom = nextRoom;
                    notification = new Notification("PlayerDidEnterRoom", this);
                    NotificationCenter.Instance.PostNotification(notification);
                }
                else
                {
                    this.ErrorMessage("\nThe door is closed");                }
            }
            else
            {
                if (direction == "portal")
                {
                    this.ErrorMessage("\nThere is no " + direction);
                }
                else
                {
                    this.ErrorMessage("\nThere is no door on " + direction);
                }
            }
        }

        //used by BackCommand, move to previous room
        public void WaltBack()
        {
            Parser _parser = new Parser(new CommandWords());
            if (_movementlog.Count != 0)
            {
                Command command = _parser.ParseCommand(_movementlog.Pop());
                switch (command.SecondWord)
                {
                    case "north":
                        command.SecondWord = "south";
                        this.WaltTo(command.SecondWord);
                        break;
                    case "south":
                        command.SecondWord = "north";
                        this.WaltTo(command.SecondWord);
                        break;
                    case "west":
                        command.SecondWord = "east";
                        this.WaltTo(command.SecondWord);
                        break;
                    case "east":
                        command.SecondWord = "west";
                        this.WaltTo(command.SecondWord);
                        break;
                    case "portal":
                        _movementlog.Clear();
                        this.ErrorMessage("\nThere is no way to go back");
                        break;
                    default:
                        this.ErrorMessage("\nThere is no way to go back");
                        break;
                }
            }
            else
            {
                this.ErrorMessage("\nThere is no way to go back");
            }
        }

        //used by PickupCommand, pick ups items
        public bool Pickup(string word)
        {
            bool success = false;
            KeyItem keyitem = CurrentRoom.GetKeyItem(word);
            Item item = CurrentRoom.GetItem(word);
            if (item != null)
            {
                if (item.CanBeHeld)
                {
                    success = _backPack.Add(item);
                    if (success == true)
                    {
                        this.NotificationMessage("\nYou have picked up the " + item.Name);
                        CurrentRoom.RemoveItem(item.Name);
                    }
                    else
                    {
                        this.NotificationMessage("\nYour backpack is full, you need to drop something to pick up the " + item.Name);
                    }
                }
                else
                {
                    this.NotificationMessage("\nYou cannot pickup the " + item.Name);
                }
            }
            else if (keyitem != null)
            {
                if (keyitem.CanBeHeld)
                {
                    this.NotificationMessage("\nYou have picked up the " + keyitem.Name);
                    success = _backPack.AddKeyItems(keyitem);
                    CurrentRoom.RemoveItem(word);
                }
                else
                {
                    this.NotificationMessage("\nYou cannot pickup the " + keyitem.Name);
                }
            }
            else
            {
                this.ErrorMessage("\nThere is nothing named " + word + " to pick up");
            }
            return success;
        }

        //used by DropCommand, drops items
        public bool Drop(string word)
        {
            bool success = false;
            KeyItem keyitem = _backPack.GetKeyItem(word);
            Item item = _backPack.GetItem(word);
            if (item != null)
            {
                this.NotificationMessage("\nYou have dropped the " + item.Name);
                CurrentRoom.SetItem(item.Name, item);
                success = _backPack.Remove(word);
            }
            else if (keyitem != null)
            {
                if (keyitem.CanBeDropped)
                {
                    this.NotificationMessage("\nYou have dropped the " + keyitem.Name);
                }
                else
                {
                    this.NotificationMessage("\nYou cannot drop the " + keyitem.Name + " as it is a key item");
                }

            }
            else
            {
                this.ErrorMessage("\nThere is nothing named " + word + " to drop");
            }
            return success;
        }

        //used by EquipCommand, equip items
        public bool Equip(string word)
        {
            bool success = false;
            Item item = _backPack.GetItem(word);
            if (item != null)
            {
                success = EquipX(item);
            }
            else
            {
                this.ErrorMessage("\nThere is nothing named " + word + " to equip");
            }
            return success;
        }

        //used by UnequipCommand, unequip items
        public bool Unequip(string word)
        {
            bool success = false;
            
            if (_armor != null)
            {
                Item armor = _armor;
                success = UnequipX(armor);
            }
            else if (_weapon != null)
            {
                Item weapon = _weapon;
                success = UnequipX(weapon);
            }
            else
            {
                this.ErrorMessage("\nThere is nothing named " + word + " to unequip");
            }
            return success;
        }

        //used by AttackCommand, attack enemies
        public void Attack(string word)
        {
            NPC npc = CurrentRoom.GetNPC(word);
            Enemy enemy = CurrentRoom.GetEnemy(word);
            if(enemy != null)
            {
                this.Battle(enemy);
            }
            else if (npc != null)
            {
                NotificationMessage("\nYou are unable to attack " + npc.Name);
            }
            else
            {
                ErrorMessage("\nThere is no one named " + word + " in the current room to attack");
            }
        }

        public void Battle(Enemy enemy)
        {
            Parser _parser = new Parser(new CommandWords());
            if (_priority == enemy.Priority)
            {
                Random random = new Random();
                while (_priority == 1)
                {
                    _priority = random.Next(0, 3);
                }
            }
            if(_priority < enemy.Priority)
            {
                _priority = 1;
                NotificationMessage("\nThe enemy is slower, allowing you to strike before it");
                enemy.Hp -= _ar;
                NotificationMessage("\nYou have attack the " + enemy.Name + " and dealt " + _ar + " damage");
                NotificationMessage("\nEnemy Hp: " + enemy.Hp);
                if (enemy.Hp <= 0)
                {
                    NotificationMessage("\n" + enemy.Name + " has died");
                    NotificationMessage("\nYou have won");
                    this.CurrentRoom.RemoveEnemy(enemy.Name);
                }   
                else
                {
                    _hp -= enemy.Ar;
                    NotificationMessage("\nThe " + enemy.Name + " have attack you and dealt " + enemy.Ar + " damage");
                    NotificationMessage("\nPlayer Hp: " + _hp);
                    if (_hp <= 0)
                    {
                        NotificationMessage("\nYou have died");
                    }
                    else
                    {
                        Console.Write("\n>");
                        String temp = Console.ReadLine();
                        Command command = _parser.ParseCommand(temp);
                        if (command.Name == "attack")
                        {
                            command.Execute(this);
                        }
                        else
                        {
                            ErrorMessage("\nYou are unable to do anything but attack, use items, or run");
                        }
                    }
                }
            }
            else if (_priority > enemy.Priority)
            {
                _priority = 1;
                NotificationMessage("\nThe enemy is faster, allowing it to strike before you");
                _hp -= enemy.Ar;
                NotificationMessage("\nThe " + enemy.Name + " have attack you and dealt " + enemy.Ar + " damage");
                NotificationMessage("\nPlayer Hp: " + _hp);
                if (_hp <= 0)
                {
                    NotificationMessage("\nYou have died");
                }
                else
                {
                    enemy.Hp -= _ar;
                    NotificationMessage("\nYou have attack the " + enemy.Name + " and dealt " + _ar + " damage");
                    NotificationMessage("\nEnemy Hp: " + enemy.Hp);
                    if (enemy.Hp <= 0)
                    {
                        NotificationMessage("\n" + enemy.Name + " has died");
                        NotificationMessage("\nYou have won");
                        this.CurrentRoom.RemoveEnemy(enemy.Name);
                    }
                    else
                    {
                        Console.Write("\n>");
                        String temp = Console.ReadLine();
                        Command command = _parser.ParseCommand(temp);
                        if (command.Name == "attack")
                        {
                            command.Execute(this);
                        }
                        else
                        {
                            ErrorMessage("\nYou are unable to do anything but attack, use items, or run");
                        }
                    }
                }
            }
        }

        //used by InventoryCommand, display inventory
        public void Inventory()
        {
            this.OutputMessage("\nItems:" + this._backPack.GetItems() + "\nKey Items:" + this._backPack.GetKeyItems() + "\nWeight: " + this._backPack.GetWeight() + "/50");
        }

        //used by SayCommand, allows you to say a word
        public void Say(string word)
        {
            SayMessage("\n" + word);
            Dictionary<string, object> userInfo = new Dictionary<string, object>();
            userInfo["word"] = word;
            Notification notification = new Notification("PlayerSaidWord", this, userInfo);
            NotificationCenter.Instance.PostNotification(notification);
        }

        //used by SpeaktoCommand, used to interact with NPCs
        public void Speakto(string word)
        {
            NPC npc = CurrentRoom.GetNPC(word);
            Enemy enemy = CurrentRoom.GetEnemy(word);
            if (npc != null)
            {
                NotificationMessage("\n" + npc.Dialog);
            }
            else if (enemy != null)
            {
                NotificationMessage("\nYou do not understand what the " + word + " is saying");
            }
            else
            {
                ErrorMessage("\nThere is no one named " + word + " in the current room to speak to");
            }
        }

        //used by OpenCommand, opens doors and chests
        public void Open(string name)
        {
            Door door = this.CurrentRoom.GetExit(name);
            Chest chest = this.CurrentRoom.GetChest(name);
            if (door != null)
            {
                if (name != "portal")
                {
                    if (door.IsClosed)
                    {
                        if (door.IsLocked)
                        {
                            this.ErrorMessage("\nThe door is closed and locked");
                        }
                        else
                        {
                            door.Open();
                            this.NotificationMessage("\nThe door has been opened");
                        }
                    }
                    else
                    {
                        this.ErrorMessage("\nThe door is open");
                    }
                }
                else
                {
                    this.ErrorMessage("\nYou cannot open the " + name);
                }
            }
            else if (chest != null)
            {
                if (chest.IsClosed)
                {
                    if (chest.IsLocked)
                    {
                        this.ErrorMessage("\nThe chest is closed and locked");
                    }
                    else
                    {
                        chest.Open();
                        this.NotificationMessage("\nThe chest has been opened");
                    }
                }
                else
                {
                    this.ErrorMessage("\nThe chest is open");
                }
            }
            else
            {
                this.ErrorMessage("\nThere is no door or chest named " + name + " to open");
            }
        }

        //used by CloseCommand, closes doors and chests
        public void Close(string name)
        {
            Door door = this.CurrentRoom.GetExit(name);
            Chest chest = this.CurrentRoom.GetChest(name);
            if (door != null)
            {
                if (name != "portal")
                {
                    if (door.IsOpen)
                    {
                        door.Close();
                        this.NotificationMessage("\nThe door has been closed");
                    }
                    else
                    {
                        this.ErrorMessage("\nThe door is close");
                    }
                }
                else
                {
                    this.ErrorMessage("\nYou cannot close the " + name);
                }
            }
            else if (chest != null)
            {

                if (chest.IsOpen)
                {
                    chest.Close();
                    this.NotificationMessage("\nThe chest has been closed");
                }
                else
                {
                    this.ErrorMessage("\nThe chest is close");
                }
            }
            else
            {
                this.ErrorMessage("\nThere is no door or chest named " + name + " to close");
            }
        }

        //used by UnlockCommand, unlocks doors and chests
        public void Unlock(string name)
        {
            Door door = this.CurrentRoom.GetExit(name);
            Chest chest = this.CurrentRoom.GetChest(name);
            if (door != null)
            {
                if (name != "portal")
                {
                    if (door.IsOpen)
                    {
                        this.ErrorMessage("\nThe door is open");
                    }
                    else
                    {
                        if (door.IsLocked)
                        {
                            door.Unlock();
                            this.NotificationMessage("\nThe door has been unlocked");
                        }
                        else
                        {
                            this.ErrorMessage("\nThe door is not locked");
                        }
                    }
                }
                else
                {
                    this.ErrorMessage("\nYou cannot unlock the " + name);
                }
            }
            else if (chest != null)
            {
                if (chest.IsOpen)
                {
                    this.ErrorMessage("\nThe chest is open");
                }
                else
                {
                    if (chest.IsLocked)
                    {
                        chest.Unlock();
                        this.NotificationMessage("\nThe chest has been unlocked");
                    }
                    else
                    {
                        this.ErrorMessage("\nThe chest is not locked");
                    }
                }
            }
            else
            {
                this.ErrorMessage("\nThere is no door or chest named " + name + " to unlock");
            }
        }

        //used by LockCommand, used to lock doors and chests
        public void Lock(string name)
        {
            Door door = this.CurrentRoom.GetExit(name);
            Chest chest = this.CurrentRoom.GetChest(name);
            if (door != null)
            {
                if (name != "portal")
                {
                    if (door.IsOpen)
                    {
                        this.ErrorMessage("\nThe door is open");
                    }
                    else
                    {
                        Regularlock aLock = new Regularlock();
                        door.InstallLock(aLock);
                        door.Lock();
                        this.NotificationMessage("\nThe door has been locked");
                    }
                }
                else
                {
                    this.ErrorMessage("\nYou cannot lock the " + name);
                }
            }
            else if (chest != null)
            {
                if (chest.IsOpen)
                {
                    this.ErrorMessage("\nThe chest is open");
                }
                else
                {
                    Regularlock aLock = new Regularlock();
                    chest.InstallLock(aLock);
                    chest.Lock();
                    this.NotificationMessage("\nThe chest has been locked");
                }
            }
            else
            {
                this.ErrorMessage("\nThere is no door or chest named " + name + " to lock");
            }
        }

        //used by SearchCommand, searches for items and chest
        public void Search()
        {
            this.NotificationMessage(this.CurrentRoom.SearchRoom());
        }

        //prints a message
        public void OutputMessage(string message)
        {
            Console.WriteLine(message);
        }
        public void NotificationMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void LocationMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void SayMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
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
        }

        public void InputMovementLog(string movementCommand)
        {
            Parser _parser = new Parser(new CommandWords());
            Command command = _parser.ParseCommand(movementCommand);
            if (command.SecondWord != null)
            {
                Door door = this.CurrentRoom.GetExit(command.SecondWord);
                if (door != null)
                {
                    _movementlog.Push(movementCommand);
                }
            }
        }

        //used by ClearLog(), clears the log
        public void ClearLog()
        {
            _log.Clear();
        }

        //used by RestartCommand, restarts the program and clears the log
        public void RestartGame()
        {
            _log.Clear();
            Game game = new Game();
            game.Restart();
        }

        public void Map()
        {
            this.OutputMessage("\n                1_8               " + 
                               "\n                 |                " + 
                               "\n      2_7--2_8  1_7--1_5--1_6     " +
                               "\n       |              |           " +
                               "\n      2_5--2_4  1_2--1_3--1_4  3_9" +
                               "\n       |    |    |              | " +
                               "\n      2_6  2_3  1_1  3_2  3_4--3_5" +
                               "\n            |    |    |    |    | " +
                               "\n      2_2--2_1  1_0  3_1--3_3  3_6" +
                               "\n            |    |    |         | " +
                               "\n           2_0--ENT--3_0  3_8--3_7" +
                               "\n                 |                " +
                               "\n                TWN             \n");
        }
    }
}
