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
        private int _hp = 50;
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

        public int HP
        {
            get
            {
                return _hp;
            }
            set
            {
                _hp = value;
            }
        }

        public bool EquipX(Item item)
        {
            bool success = false;
            if (item.GetType() == typeof(Armor))
            {
                _armor = (Armor)item;
                _backPack.RemoveItem(item.Name);
                success = true;
            }
            else if (item.GetType() == typeof(Weapon))
            {
                _weapon = (Weapon)item;
                _backPack.RemoveItem(item.Name);
                success = true;
            }
            return success;
        }

        public bool UnequipX(Item item)
        {
            bool success = false;
            if (item.GetType() == typeof(Armor))
            {
                _backPack.Add(item);
                _armor = null;
                success = true;
            }
            else if (item.GetType() == typeof(Weapon))
            {
                _backPack.Add(item);
                _weapon = null;
                success = true;
            }
            return success;
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
                    this.OutputMessage("\nThe door is closed");
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

        public bool Pickup(string word)
        {
            bool success = false;
            Item item = CurrentRoom.GetItem(word);
            if (item != null)
            {
                this.OutputMessage("\n" + item.Name);
                success = _backPack.Add(item);
                CurrentRoom.RemoveItem(word);
                this.OutputMessage("\n" + this._backPack.GetItems());
            }

            return success;
        }

        public bool Equip(string word)
        {
            bool success = false;
            Item item = _backPack.GetItem(word);

                this.OutputMessage("\n" + this._backPack.GetItems());
                success = EquipX(item);


            return success;
        }

        public bool Drop(string word)
        {
            bool success = false;
            Item item = _backPack.GetItem(word);
            if (item != null)
            {
                CurrentRoom.SetItem(item.Name, item);
                _backPack.RemoveItem(word);
                success = true;
            }

            return success;
        }

        public bool Unequip(string word)
        {
            bool success = false;
            Item item = _armor;
            if (item.Name == word)
            {
                this.OutputMessage("\n" + this._backPack.GetItems());
                success = UnequipX(item);
                
            }

            return success;
        }

        public void Say(string word)
        {
            OutputMessage("\n" + word);
            Dictionary<string, object> userInfo = new Dictionary<string, object>();
            userInfo["word"] = word;
            Notification notification = new Notification("PlayerSaidWord", this, userInfo);
            NotificationCenter.Instance.PostNotification(notification);
            this.OutputMessage("\n" + this.CurrentRoom.Description());
        }

        public void Open(string name)
        {
            Door door = this.CurrentRoom.GetExit(name);
            Chest chest = this.CurrentRoom.GetChest(name);
            if (name != "portal" && name != "chest")
            {
                if (door != null)
                {
                    if (door.IsClosed)
                    {
                        if (door.IsLocked)
                        {
                            this.OutputMessage("\nThe door is closed and locked");
                            this.OutputMessage("\n" + this.CurrentRoom.Description());
                        }
                        else
                        {
                            door.Open();
                            this.OutputMessage("\nThe door has been opened");
                            this.OutputMessage("\n" + this.CurrentRoom.Description());
                        }
                    }
                    else
                    {
                        this.OutputMessage("\nThe door is open");
                        this.OutputMessage("\n" + this.CurrentRoom.Description());
                    }
                }
                else
                {
                    this.OutputMessage("\nThere is no door " + name + " to close");
                    this.OutputMessage("\n" + this.CurrentRoom.Description());
                }
            }
            else if (name == "chest")
            {
                if (chest != null)
                {
                    if (chest.IsClosed)
                    {
                        if (chest.IsLocked)
                        {
                            this.OutputMessage("\nThe chest is closed and locked");
                            this.OutputMessage("\n" + this.CurrentRoom.Description());
                        }
                        else
                        {
                            chest.Open();
                            this.OutputMessage("\nThe chest has been opened");
                            this.OutputMessage("\n" + this.CurrentRoom.Description());
                        }
                    }
                    else
                    {
                        this.OutputMessage("\nThe chest is open");
                        this.OutputMessage("\n" + this.CurrentRoom.Description());
                    }
                }
                else
                {
                    this.OutputMessage("\nThere is no " + name + " to open");
                    this.OutputMessage("\n" + this.CurrentRoom.Description());
                }
            }
            else
            {
                this.OutputMessage("\nYou cannot open a portal");
                this.OutputMessage("\n" + this.CurrentRoom.Description());
            }
        }

        public void Close(string name)
        {
            Door door = this.CurrentRoom.GetExit(name);
            Chest chest = this.CurrentRoom.GetChest(name);
            if (name != "portal" && name != "chest")
            {
                if (door != null)
                {
                    if (door.IsOpen)
                    {
                        door.Close();
                        this.OutputMessage("\nThe door has been closed");
                        this.OutputMessage("\n" + this.CurrentRoom.Description());
                    }
                    else
                    {
                        this.OutputMessage("\nThe door is close");
                        this.OutputMessage("\n" + this.CurrentRoom.Description());
                    }
                }
                else
                {
                    this.OutputMessage("\nThere is no door " + name + " to close");
                    this.OutputMessage("\n" + this.CurrentRoom.Description());
                }
            }
            else if (name == "chest")
            {
                if (chest != null)
                {
                    if (chest.IsOpen)
                    {
                        chest.Close();
                        this.OutputMessage("\nThe chest has been closed");
                        this.OutputMessage("\n" + this.CurrentRoom.Description());
                    }
                    else
                    {
                        this.OutputMessage("\nThe chest is close");
                        this.OutputMessage("\n" + this.CurrentRoom.Description());
                    }
                }
                else
                {
                    this.OutputMessage("\nThere is no " + name + " to close");
                    this.OutputMessage("\n" + this.CurrentRoom.Description());
                }
            }
            else
            {
                this.OutputMessage("\nYou cannot close a portal");
                this.OutputMessage("\n" + this.CurrentRoom.Description());
            }
        }

        public void Unlock(string name)
        {
            Door door = this.CurrentRoom.GetExit(name);
            Chest chest = this.CurrentRoom.GetChest(name);
            if (name != "portal" && name != "chest")
            {
                if (door != null)
                {
                    if (door.IsOpen)
                    {
                        this.OutputMessage("\nThe door is open");
                        this.OutputMessage("\n" + this.CurrentRoom.Description());
                    }
                    else
                    {
                        if (door.IsLocked)
                        {
                            door.Unlock();
                            this.OutputMessage("\nThe door has been unlocked");
                            this.OutputMessage("\n" + this.CurrentRoom.Description());
                        }
                        else
                        {
                            this.OutputMessage("\nThe door is not locked");
                            this.OutputMessage("\n" + this.CurrentRoom.Description());
                        }
                    }
                }
                else
                {
                    this.OutputMessage("\nThere is no door " + name + " to unlock");
                    this.OutputMessage("\n" + this.CurrentRoom.Description());
                }
            }
            else if (name == "chest")
            {
                if (chest != null)
                {
                    if (chest.IsOpen)
                    {
                        this.OutputMessage("\nThe chest is open");
                        this.OutputMessage("\n" + this.CurrentRoom.Description());
                    }
                    else
                    {
                        if (chest.IsLocked)
                        {
                            chest.Unlock();
                            this.OutputMessage("\nThe chest has been unlocked");
                            this.OutputMessage("\n" + this.CurrentRoom.Description());
                        }
                        else
                        {
                            this.OutputMessage("\nThe chest is not locked");
                            this.OutputMessage("\n" + this.CurrentRoom.Description());
                        }
                    }
                }
                else
                {
                    this.OutputMessage("\nThere is no " + name + " to unlock");
                    this.OutputMessage("\n" + this.CurrentRoom.Description());
                }
            }
            else
            {
                this.OutputMessage("\nYou cannot unlock a portal");
                this.OutputMessage("\n" + this.CurrentRoom.Description());
            }
        }

        public void Lock(string name)
        {
            Door door = this.CurrentRoom.GetExit(name);
            Chest chest = this.CurrentRoom.GetChest(name);
            if (name != "portal" && name != "chest")
            {
                if (door != null)
                {
                    if (door.IsOpen)
                    {
                        this.OutputMessage("\nThe door is open");
                        this.OutputMessage("\n" + this.CurrentRoom.Description());
                    }
                    else
                    {
                        Regularlock aLock = new Regularlock();
                        door.InstallLock(aLock);
                        door.Lock();
                        this.OutputMessage("\nThe door has been locked");
                        this.OutputMessage("\n" + this.CurrentRoom.Description());
                    }
                }
                else
                {
                    this.OutputMessage("\nThere is no door " + name + " to lock");
                    this.OutputMessage("\n" + this.CurrentRoom.Description());
                }
            }
            else if (name == "chest")
            {
                if (chest != null)
                {
                    if (chest.IsOpen)
                    {
                        this.OutputMessage("\nThe chest is open");
                        this.OutputMessage("\n" + this.CurrentRoom.Description());
                    }
                    else
                    {
                        Regularlock aLock = new Regularlock();
                        chest.InstallLock(aLock);
                        chest.Lock();
                        this.OutputMessage("\nThe chest has been locked");
                        this.OutputMessage("\n" + this.CurrentRoom.Description());
                    }
                }
                else
                {
                    this.OutputMessage("\nThere is no " + name + " to lock");
                    this.OutputMessage("\n" + this.CurrentRoom.Description());
                }
            }
            else
            {
                this.OutputMessage("\nYou cannot lock a portal");
                this.OutputMessage("\n" + this.CurrentRoom.Description());
            }
        }

        public void Search()
        {
            this.OutputMessage("\n" + this.CurrentRoom.SearchRoom());
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
