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
        private int _maxhp = 100;
        private int _hp = 100;
        private int _ar = 5;
        private int _av = 0;
        private int _priority = 1;
        private int _xp = 0;
        private int _level = 1;
        private int _gold = 0;
        private Armor _armor;
        private Weapon _weapon;
        private CombatLoop _combatLoop;

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

        public int MaxHp { set { _maxhp = value; } get { return _maxhp; } }
        public int Hp { set { _hp = value; } get { return _hp; } }
        public int Ar { set { _ar = value; } get { return _ar; } }
        public int Av { set { _av = value; } get { return _av; } }
        public int Priority { set { _priority = value; } get { return _priority; } }
        public int Xp { set { _xp = value; } get { return _xp; } }
        public int Level { set { _level = value; } get { return _level; } }
        public int Gold { set { _gold = value; } get { return _gold; } }

        public Player(Room room)
        {
            _currentRoom = room;
        }

        public bool EquipX(Item item)
        {
            bool success = false;
            
                if (item.GetType() == typeof(Armor) && _armor == null)
                {
                    this.NotificationMessage("\nYou have equipped the " + item.Name);
                    _armor = (Armor)item;
                    _av += _armor.AV;
                    this.NotificationMessage("Armor Value: " + _av);
                    _backPack.Remove(item.Name);
                    success = true;
                }
                else if (item.GetType() == typeof(Weapon) && _weapon == null)
                {
                    this.NotificationMessage("\nYou have equipped the " + item.Name);
                    _weapon = (Weapon)item;
                    _ar += _weapon.AR;
                    this.NotificationMessage("Attack Rating: " + _ar);
                    _backPack.Remove(item.Name);
                    success = true;
                }
            return success;
        }

        public bool UnequipX(Item item)
        {
            bool success = false;
            int temp = _backPack.LIMIT - item.Weight;
            if (temp > 0)
            {
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
            }
            else
            {
                this.ErrorMessage("\nYour backpack is full");
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
            Item chestsub = CurrentRoom.GetChest(word);
            Chest chest = (Chest)chestsub;
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
                        this.ErrorMessage("\nYour backpack is full, you need to drop something to pick up the " + item.Name);
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
                    CurrentRoom.RemoveKeyItems(word);
                }
                else
                {
                    this.NotificationMessage("\nYou cannot pickup the " + keyitem.Name);
                }
            }
            else if (chest != null)
            {
                this.ErrorMessage("\nYou cannot pickup the " + chest.Name);
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
            Item item = _backPack.GetItem(word);
            KeyItem keyitem = _backPack.GetKeyItem(word);
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

        public bool Buy(string word)
        {
            bool success = false;
            Item item = CurrentRoom.Shop.GetItem(word);
            if (CurrentRoom.GetNPC("merchant") != null)
            {
                if (item != null)
                {
                    if (item.CanBeHeld && item.Value <= Gold)
                    {
                        Gold -= item.Value;
                        success = _backPack.Add(item);
                        if (success == true)
                        {
                            this.NotificationMessage("\nYou have bought the " + item.Name + " for " + item.Value + " gold");
                            CurrentRoom.RemoveItem(item.Name);
                        }
                        else
                        {
                            this.NotificationMessage("\nYour backpack is full, you need to drop something to buy the " + item.Name);
                        }
                    }
                    else
                    {
                        this.NotificationMessage("\nYou cannot afford the " + item.Name);
                    }
                }
                else
                {
                    this.ErrorMessage("\nThere is nothing named " + word + " to buy");
                }
            }
            else
            {

                this.NotificationMessage("\nThere is no merchant to buy from");
            }
            return success;
        }

        public void Browse()
        {
            if (CurrentRoom.GetNPC("merchant") != null)
            {
                this.NotificationMessage(this.CurrentRoom.Shop.GetItems());
            }
            else
            {

                this.NotificationMessage("\nThere is no merchant to with goods to browse");
            }
        }

        public bool Sell(string word)
        {
            bool success = false;
            KeyItem keyitem = _backPack.GetKeyItem(word);
            Item item = _backPack.GetItem(word);
            if (CurrentRoom.GetNPC("merchant") != null)
            {
                if (item != null)
                {
                    int sValue = item.Value / 2;
                    this.NotificationMessage("\nYou have sold the " + item.Name + " for " + sValue);
                    _gold += sValue;
                    CurrentRoom.Shop.Add(item);
                    success = _backPack.Remove(word);
                }
                else if (keyitem != null)
                {
                    if (keyitem.CanBeDropped)
                    {
                        this.NotificationMessage("\nYou have sold the " + keyitem.Name);
                    }
                    else
                    {
                        this.NotificationMessage("\nYou cannot sell the " + keyitem.Name + " as it is a key item");
                    }

                }
                else
                {
                    this.ErrorMessage("\nThere is nothing named " + word + " to sell");
                }
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
                if(success == false)
                {
                    this.ErrorMessage("\nYou are unable to equip the " + word);
                }
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
            
            if (_armor != null && word == _armor.Name)
            {
                Item item = _armor;
                success = UnequipX(item);
                if (success == false)
                {
                    this.ErrorMessage("\nYou are unable to unequip the " + word);
                }
            }
            else if (_weapon != null && word == _weapon.Name)
            {
                Item item = _weapon;
                success = UnequipX(item);
                if (success == false)
                {
                    this.ErrorMessage("\nYou are unable to unequip the " + word);
                }
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
                this.LocationMessage("\n" + this.CurrentRoom.Description());
            }
            else
            {
                ErrorMessage("\nThere is no one named " + word + " in the current room to attack");
                this.LocationMessage("\n" + this.CurrentRoom.Description());
            }
        }

        public void Heal(string word)
        {
            Item item = _backPack.GetItem(word);
            Potion p;
            if (item != null)
            {
                if(item.GetType() == typeof(Potion))
                {
                    p = (Potion)item;
                    if(p.GetHealing(p.Type))
                    {
                        this.NotificationMessage("\nYou can use the " + word + " to heal with");
                        if (MaxHp == Hp)
                        {
                            this.ErrorMessage("\nYou have full hp");
                        }
                        else
                        {
                            Hp += p.Modifier;
                            if (MaxHp < Hp)
                            {
                                int dif = (Hp - MaxHp);
                                dif = p.Modifier - dif;
                                Hp = MaxHp;
                                this.NotificationMessage("\nThe potion healed for " + dif);
                                this.NotificationMessage("\nHP: " + Hp);
                                _backPack.Remove(item.Name);
                            }
                            else
                            {
                                this.NotificationMessage("\nThe potion healed for " + p.Modifier);
                                this.NotificationMessage("\nHP: " + Hp);
                                _backPack.Remove(item.Name);
                            }
                        }
                    }
                    else
                    {
                        this.ErrorMessage("\nYou cannot use the " + word + " to heal with");
                    }
                }
                else
                {
                    this.ErrorMessage("\nYou cannot use the " + word + " to heal with");
                }
            }
            else
            {
                this.ErrorMessage("\nThere is potion named " + word + " to use");
            }
        }

        public void Battle(Enemy enemy)
        {
            Parser _parser = new Parser(new CommandWords());
            _combatLoop = new CombatLoop(this, enemy);
            bool speed = _combatLoop.comparePriority();
            if (speed == true)
            {
                NotificationMessage("\nThe enemy is slower, allowing you to strike before it");
                _combatLoop.eDamage(_ar);
                if (enemy.Hp <= 0)
                {
                    _combatLoop.Victory();
                }   
                else
                {
                    _combatLoop.pDamage(enemy.Ar);
                    if (_hp <= 0)
                    {
                        NotificationMessage("\nYou have died");
                        _combatLoop.Defeat();
                    }
                    else
                    {
                        _combatLoop.Loop(enemy);
                    }
                }
            }
            else if (speed == false)
            {
                NotificationMessage("\nThe enemy is faster, allowing it to strike before you");
                _combatLoop.pDamage(enemy.Ar);
                if (_hp <= 0)
                {
                    NotificationMessage("\nYou have died");
                    _combatLoop.Defeat();
                }
                else
                {
                    _combatLoop.eDamage(_ar);
                    if (enemy.Hp <= 0)
                    {
                        _combatLoop.Victory();
                    }
                    else
                    {
                        _combatLoop.Loop(enemy);
                    }
                }
            }
            else
            {
                NotificationMessage("\nSomething Broke");
            }
        }

        //used by InventoryCommand, display inventory
        public void Inventory()
        {
            this.NotificationMessage("\nItems:" + this._backPack.GetItems() + "\nKey Items:" + this._backPack.GetKeyItems() + "\nWeight: " + this._backPack.GetWeight() + "/50" + "\nGold: " + _gold);
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
                NotificationMessage("\n" + npc.Name + ": " + npc.Dialog);
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
            Item item = this.CurrentRoom.GetChest(name);
            Chest chest = (Chest)item;
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
                        chest.RemoveItems();
                        this.NotificationMessage("\nThe chest has been opened");
                        this.NotificationMessage("\nThe items has spilled out onto the ground and the chest disappears");
                        CurrentRoom.RemoveChest(chest.Name);
                    }
                }
                else
                {
                    this.ErrorMessage("\nThe chest is open");
                }
            }
            else
            {
                this.ErrorMessage("\nThere is nothing named " + name + " to open");
            }
        }

        //used by CloseCommand, closes doors and chests
        public void Close(string name)
        {
            Door door = this.CurrentRoom.GetExit(name);
            Item item = this.CurrentRoom.GetChest(name);
            Chest chest = (Chest)item;
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
                this.ErrorMessage("\nThere is nothing named " + name + " to close");
            }
        }

        //used by UnlockCommand, unlocks doors and chests
        public void Unlock(string name)
        {
            Door door = this.CurrentRoom.GetExit(name);
            Item item = this.CurrentRoom.GetChest(name);
            Chest chest = (Chest)item;
            KeyItem keyitem;
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
                            keyitem = _backPack.GetKeyItem(door.KeyName);
                            if (keyitem != null)
                            {
                                door.Unlock();
                                this.NotificationMessage("\nThe door has been unlocked");
                            }
                            else
                            {
                                this.ErrorMessage("\nYou do not have the item required to open this door");
                            }
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
                        keyitem = _backPack.GetKeyItem(chest.KeyName);
                        if (keyitem != null)
                        {
                            chest.Unlock();
                            this.NotificationMessage("\nThe chest has been unlocked");
                        }
                        else
                        {
                            this.ErrorMessage("\nYou do not have the item required to open this chest");
                        }
                    }
                    else
                    {
                        this.ErrorMessage("\nThe chest is not locked");
                    }
                }
            }
            else
            {
                this.ErrorMessage("\nThere is no nothing named " + name + " to unlock");
            }
        }

        //used by LockCommand, used to lock doors and chests
        public void Lock(string name)
        {
            Door door = this.CurrentRoom.GetExit(name);
            Item item = this.CurrentRoom.GetChest(name);
            Chest chest = (Chest)item;
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
                this.ErrorMessage("\nThere is nothing named " + name + " to lock");
            }
        }

        //used by SearchCommand, searches for items and chest
        public void Search()
        {
            this.NotificationMessage(this.CurrentRoom.SearchRoom());
        }

        public bool Inspect(string word)
        {
            bool success = false;
            Armor a;
            Weapon w;
            Potion p;
            Item grndItem = CurrentRoom.GetItem(word);
            Item invItem = _backPack.GetItem(word);
            KeyItem grndKeyitem = CurrentRoom.GetKeyItem(word);
            KeyItem invKeyitem = _backPack.GetKeyItem(word);
            if (invItem != null || grndItem != null)
            {
                if (invItem != null)
                {
                    this.NotificationMessage("\nValue: " + invItem.Value);
                    this.NotificationMessage("\nWeight: " + invItem.Weight);
                    if (invItem.GetType() == typeof(Armor))
                    {
                        a = (Armor)invItem;
                        this.NotificationMessage("\nArmor Value: " + a.AV);
                    }
                    else if (invItem.GetType() == typeof(Weapon))
                    {
                        w = (Weapon)invItem;
                        this.NotificationMessage("\nAttack Rating: " + w.AR);
                    }
                    else if (invItem.GetType() == typeof(Potion))
                    {
                        p = (Potion)invItem;
                        this.NotificationMessage("\nPotion Type: " + p.Type);
                        this.NotificationMessage("\nModifier: " + p.Modifier);
                    }
                }
                else if (grndItem != null)
                { 
                    this.NotificationMessage("\nValue: " + grndItem.Value);
                    this.NotificationMessage("\nWeight: " + grndItem.Weight);
                    if (grndItem.GetType() == typeof(Armor))
                    {
                        a = (Armor)grndItem;
                        this.NotificationMessage("\nArmor Value: " + a.AV);
                    }
                    else if (grndItem.GetType() == typeof(Weapon))
                    {
                        w = (Weapon)grndItem;
                        this.NotificationMessage("\nAttack Rating: " + w.AR);
                    }
                    else if (grndItem.GetType() == typeof(Potion))
                    {
                        p = (Potion)grndItem;
                        this.NotificationMessage("\nPotion Type: " + p.Type);
                        this.NotificationMessage("\nModifier: " + p.Modifier);
                    }
                }
            }
            else if (invKeyitem != null || grndKeyitem != null)
            {
                if (invKeyitem != null)
                {
                    this.NotificationMessage("\nDiscription: " + invKeyitem.Description);
                }
                else if (grndKeyitem != null)
                {
                    this.NotificationMessage("\nDiscription: " + grndKeyitem.Description);
                }
            }
            else
            {
                this.ErrorMessage("\nThere is no item named " + word + " to inspect");
            }
            return success;
        }
        public void Stats()
        {
            this.NotificationMessage(" *** HP: " + Hp + "/" + MaxHp + "\n *** Attack Rating: " + Ar + "\n *** Armor Value: " + Av);
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
                               "\n      2_7--2_8  1_7--1_5--1_6  OBJ" +
                               "\n       |              |         | " +
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
