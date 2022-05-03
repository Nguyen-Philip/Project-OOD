using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class Chest : ICloseable, Item
    {
        private Dictionary<string, List<Item>> _items;

        private Room _Location;
        private string _Name;
        private int _Value = 0;
        private int _Weight = 999;
        private int _Num = 1;
        private string _keyname;
        private bool _CanBeHeld = false;
        private bool _IsUsable = false;
        private bool _open;
        private ILockable _lock;

        public bool IsOpen { get { return _open; } }
        public bool IsClosed { get { return !_open; } }
        public bool CanClose { get { return _lock == null ? true : _lock.CanClose; } }
        public bool IsLocked { get { return _lock == null ? false : _lock.IsLocked; } }
        public bool IsUnlocked { get { return _lock == null ? true : _lock.IsUnlocked; } }
        public string Name { set { _Name = value; } get { return _Name; } }
        public Room Location { set { _Location = value; } get { return _Location; } }
        public int Value { set { _Value = value; } get { return _Value; } }
        public int Weight { set { _Weight = value; } get { return _Weight; } }
        public string KeyName { set { _keyname = value; } get { return _keyname; } }
        public int Num { set { _Num = value; } get { return _Num; } }

        public bool CanBeHeld
        {
            get
            {
                return _CanBeHeld;
            }
        }

        public bool IsUsable
        {
            get
            {
                return _IsUsable;
            }
        }

        public Chest(Room roomA, string name)
        {
            Location = roomA;
            Name = name;
            _open = true;
            _lock = null;
            _items = new Dictionary<string, List<Item>>();
        }
        public Chest(Room roomA, string name, string keyname)
        {
            Location = roomA;
            Name = name;
            _keyname = keyname;
            _open = true;
            _lock = null;
            _items = new Dictionary<string, List<Item>>();
        }

        public void Open()
        {
            if (IsUnlocked)
            {
                _open = true;
            }
        }

        public void Close()
        {
            if(IsOpen && CanClose)
            {
                _open = false;
            }
        }

        public void Lock()
        {
            if(_lock != null)
            {
                _lock.Lock();
            }
        }

        public void Unlock()
        {
            if(_lock != null)
            {
                _lock.Unlock();
            }
        }

        public ILockable InstallLock(ILockable theLock)
        {
            ILockable oldLock = _lock;
            _lock = theLock;
            return oldLock;
        }

        public static Chest CreateChest(Room room1, string label1)
        {
            Chest chest = new Chest(room1, label1);
            room1.SetChest(label1, chest);
            return chest;
        }

        public static Chest CreateClosedChest(Room room1, string label1)
        {
            Chest chest = new Chest(room1, label1);
            chest.Close();
            room1.SetChest(label1, chest);
            return chest;
        }

        public static Chest CreateLockedChest(Room room1, string label1, string keyname)
        {
            Chest chest = new Chest(room1, label1, keyname);
            chest.Close();
            Regularlock aLock = new Regularlock();
            chest.InstallLock(aLock);
            chest.Lock();
            room1.SetChest(label1, chest);
            return chest;
        }

        public void Add(Item item)
        {
            bool isIn = _items.ContainsKey(item.Name);
            if (isIn)
            {
                _items[item.Name].Add(item);
            }
            else
            {
                //Item newItem = item.Clone();
                List<Item> itemlist = new List<Item>();
                itemlist.Add(item);
                _items.Add(item.Name, itemlist);
                //Item newItem = (Item)item.Clone();
                //_items.Add(item.Name, newItem);
            }
        }

        public void RemoveItems()
        {
            Dictionary<string, List<Item>>.KeyCollection keys = _items.Keys;
            foreach (string item in keys)
            {
                for (int i = 0; i < _items[item].Count; i++)
                {
                    _Location.AddItem(_items[item][i]);
                }
            }

        }

        public List<Item> GetItem(string name)
        {
            List<Item> items = null;
            _items.TryGetValue(name, out items);
            return items;
        }

        public Item Clone()
        {
            return null;  // TODO: Fix This!!
        }
    }
}
