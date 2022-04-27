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
                    this.LocationMessage("\n" + this.CurrentRoom.Description());
                }
                else
                {
                    this.ErrorMessage("\nThe door is closed");
                    this.LocationMessage("\n" + this.CurrentRoom.Description());
                }
            }
            else
            {
                if (direction == "portal")
                {
                    this.ErrorMessage("\nThere is no " + direction);
                    this.LocationMessage("\n" + this.CurrentRoom.Description());
                }
                else
                {
                    this.ErrorMessage("\nThere is no door on " + direction);
                    this.LocationMessage("\n" + this.CurrentRoom.Description());
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
                        this.ErrorMessage("There is no way to go back");
                        this.LocationMessage("\n" + this.CurrentRoom.Description());
                        break;
                    default:
                        this.ErrorMessage("There is no way to go back");
                        this.LocationMessage("\n" + this.CurrentRoom.Description());
                        break;
                }
            }
            else
            {
                this.ErrorMessage("\nThere is no way to go back");
                this.LocationMessage("\n" + this.CurrentRoom.Description());
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
                        this.LocationMessage("\n" + this.CurrentRoom.Description());
                    }
                    else
                    {
                        this.NotificationMessage("\nYour backpack is full, you need to drop something to pick up the " + item.Name);
                        this.LocationMessage("\n" + this.CurrentRoom.Description());
                    }
                }
                else
                {
                    this.NotificationMessage("\nYou cannot pickup the " + item.Name);
                    this.LocationMessage("\n" + this.CurrentRoom.Description());
                }
            }
            else if (keyitem != null)
            {
                if (keyitem.CanBeHeld)
                {
                    this.NotificationMessage("\nYou have picked up the " + keyitem.Name);
                    success = _backPack.AddKeyItems(keyitem);
                    CurrentRoom.RemoveItem(word);
                    this.LocationMessage("\n" + this.CurrentRoom.Description());
                }
                else
                {
                    this.NotificationMessage("\nYou cannot pickup the " + keyitem.Name);
                    this.LocationMessage("\n" + this.CurrentRoom.Description());
                }
            }
            else
            {
                this.ErrorMessage("\nThere is nothing named " + word + " to pick up");
                this.LocationMessage("\n" + this.CurrentRoom.Description());
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
                this.LocationMessage("\n" + this.CurrentRoom.Description());
            }
            else if (keyitem != null)
            {
                if (keyitem.CanBeDropped)
                {
                    this.NotificationMessage("\nYou have dropped the " + keyitem.Name);
                    this.LocationMessage("\n" + this.CurrentRoom.Description());
                }
                else
                {
                    this.NotificationMessage("\nYou cannot drop the " + keyitem.Name + " as it is a key item");
                    this.LocationMessage("\n" + this.CurrentRoom.Description());
                }

            }
            else
            {
                this.ErrorMessage("\nThere is nothing named " + word + " to drop");
                this.LocationMessage("\n" + this.CurrentRoom.Description());
            }

            return success;
        }

        public void Inventory()
        {
            this.OutputMessage("\nItems:" + this._backPack.GetItems() + "\nKey Items:" + this._backPack.GetKeyItems() + "\nWeight: " + this._backPack.GetWeight() + "/50");
            this.LocationMessage("\n" + this.CurrentRoom.Description());
        }

        //used by SayCommand, allows you to say a word
        public void Say(string word)
        {
            SayMessage("\n" + word);
            Dictionary<string, object> userInfo = new Dictionary<string, object>();
            userInfo["word"] = word;
            Notification notification = new Notification("PlayerSaidWord", this, userInfo);
            NotificationCenter.Instance.PostNotification(notification);
            this.LocationMessage("\n" + this.CurrentRoom.Description());

        }

        public void Speakto(string word)
        {
            NPC npc = CurrentRoom.GetNPC(word);
            Enemy enemy = CurrentRoom.GetEnemy(word);
            if (npc != null)
            {
                NotificationMessage("\n" + npc.Dialog);
                this.LocationMessage("\n" + this.CurrentRoom.Description());
            }
            else if (enemy != null)
            {
                NotificationMessage("\nYou do not understand what the " + word + " is saying");
                this.LocationMessage("\n" + this.CurrentRoom.Description());
            }
            else
            {
                ErrorMessage("\nThere is no one named " + word + " in the current room");
                this.LocationMessage("\n" + this.CurrentRoom.Description());
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
                            this.LocationMessage("\n" + this.CurrentRoom.Description());
                        }
                        else
                        {
                            door.Open();
                            this.NotificationMessage("\nThe door has been opened");
                            this.LocationMessage("\n" + this.CurrentRoom.Description());
                        }
                    }
                    else
                    {
                        this.ErrorMessage("\nThe door is open");
                        this.LocationMessage("\n" + this.CurrentRoom.Description());
                    }
                }
                else
                {
                    this.ErrorMessage("\nYou cannot open the " + name);
                    this.LocationMessage("\n" + this.CurrentRoom.Description());
                }
            }
            else if (chest != null)
            {
                if (chest.IsClosed)
                {
                    if (chest.IsLocked)
                    {
                        this.ErrorMessage("\nThe chest is closed and locked");
                        this.LocationMessage("\n" + this.CurrentRoom.Description());
                    }
                    else
                    {
                        chest.Open();
                        this.NotificationMessage("\nThe chest has been opened");
                        this.LocationMessage("\n" + this.CurrentRoom.Description());
                    }
                }
                else
                {
                    this.ErrorMessage("\nThe chest is open");
                    this.LocationMessage("\n" + this.CurrentRoom.Description());
                }
            }
            else
            {
                this.ErrorMessage("\nThere is no door or chest named " + name + " to open");
                this.LocationMessage("\n" + this.CurrentRoom.Description());
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
                        this.LocationMessage("\n" + this.CurrentRoom.Description());
                    }
                    else
                    {
                        this.ErrorMessage("\nThe door is close");
                        this.LocationMessage("\n" + this.CurrentRoom.Description());
                    }
                }
                else
                {
                    this.ErrorMessage("\nYou cannot close the " + name);
                    this.LocationMessage("\n" + this.CurrentRoom.Description());
                }
            }
            else if (chest != null)
            {

                if (chest.IsOpen)
                {
                    chest.Close();
                    this.NotificationMessage("\nThe chest has been closed");
                    this.LocationMessage("\n" + this.CurrentRoom.Description());
                }
                else
                {
                    this.ErrorMessage("\nThe chest is close");
                    this.LocationMessage("\n" + this.CurrentRoom.Description());
                }
            }
            else
            {
                this.ErrorMessage("\nThere is no door or chest named " + name + " to close");
                this.LocationMessage("\n" + this.CurrentRoom.Description());
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
                        this.LocationMessage("\n" + this.CurrentRoom.Description());
                    }
                    else
                    {
                        if (door.IsLocked)
                        {
                            door.Unlock();
                            this.NotificationMessage("\nThe door has been unlocked");
                            this.LocationMessage("\n" + this.CurrentRoom.Description());
                        }
                        else
                        {
                            this.ErrorMessage("\nThe door is not locked");
                            this.LocationMessage("\n" + this.CurrentRoom.Description());
                        }
                    }
                }
                else
                {
                    this.ErrorMessage("\nYou cannot unlock the " + name);
                    this.LocationMessage("\n" + this.CurrentRoom.Description());
                }
            }
            else if (chest != null)
            {
                if (chest.IsOpen)
                {
                    this.ErrorMessage("\nThe chest is open");
                    this.LocationMessage("\n" + this.CurrentRoom.Description());
                }
                else
                {
                    if (chest.IsLocked)
                    {
                        chest.Unlock();
                        this.NotificationMessage("\nThe chest has been unlocked");
                        this.LocationMessage("\n" + this.CurrentRoom.Description());
                    }
                    else
                    {
                        this.ErrorMessage("\nThe chest is not locked");
                        this.LocationMessage("\n" + this.CurrentRoom.Description());
                    }
                }
            }
            else
            {
                this.ErrorMessage("\nThere is no door or chest named " + name + " to unlock");
                this.LocationMessage("\n" + this.CurrentRoom.Description());
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
                        this.LocationMessage("\n" + this.CurrentRoom.Description());
                    }
                    else
                    {
                        Regularlock aLock = new Regularlock();
                        door.InstallLock(aLock);
                        door.Lock();
                        this.NotificationMessage("\nThe door has been locked");
                        this.LocationMessage("\n" + this.CurrentRoom.Description());
                    }
                }
                else
                {
                    this.ErrorMessage("\nYou cannot lock the " + name);
                    this.LocationMessage("\n" + this.CurrentRoom.Description());
                }
            }
            else if (chest != null)
            {
                if (chest.IsOpen)
                {
                    this.ErrorMessage("\nThe chest is open");
                    this.LocationMessage("\n" + this.CurrentRoom.Description());
                }
                else
                {
                    Regularlock aLock = new Regularlock();
                    chest.InstallLock(aLock);
                    chest.Lock();
                    this.NotificationMessage("\nThe chest has been locked");
                    this.LocationMessage("\n" + this.CurrentRoom.Description());
                }
            }
            else
            {
                this.ErrorMessage("\nThere is no door or chest named " + name + " to lock");
                this.LocationMessage("\n" + this.CurrentRoom.Description());
            }
        }

        //used by SearchCommand, searches for items and chest
        public void Search()
        {
            this.NotificationMessage(this.CurrentRoom.SearchRoom());
            this.LocationMessage("\n" + this.CurrentRoom.Description());
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
            this.LocationMessage("\n" + this.CurrentRoom.Description());
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
            this.LocationMessage(this.CurrentRoom.Description());
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
            this.LocationMessage(this.CurrentRoom.Description());
        }
    }
}
