using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{ 
    public class BackPack
    {

        private int Limit = 50;
        private Dictionary<string, List<Item>> _items;
        private Dictionary<string, KeyItem> _keyItems;

        public int LIMIT { set { Limit = value; } get { return Limit; } }

        public bool Add(Item item)
        {
            bool success = false;
            bool isIn = _items.ContainsKey(item.Name);
            if (item.CanBeHeld && item.Weight <= Limit)
            {
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
                Limit -= item.Weight;
                success = true;
            }
            return success;
        }

        public bool Remove(string name)
        {
            bool success = false;
            List<Item> items = this.GetItem(name);
            if (items != null)
            {
                if (items.Count <= 1)
                {
                    Limit += items[0].Weight;
                    success = _items.Remove(name);
                }
                else
                {
                    Limit += items[0].Weight;
                    items.RemoveAt(0);
                    success = true;
                }
            }

            return success;
        }

        public List<Item> GetItem(string name)
        {
            List<Item> items = null;
            if (name != null)
            {
                _items?.TryGetValue(name, out items);
            }
            return items;
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

        public bool AddKeyItems(KeyItem keyitems)
        {
            bool success = false;
            if (keyitems.CanBeHeld && keyitems.Weight <= Limit)
            {
                _keyItems.Add(keyitems.Name, keyitems);
                Limit -= keyitems.Weight;
                success = true;
            }
            return success;
        }

        public KeyItem GetKeyItem(string name)
        {
            KeyItem keyitem = null;
            if (name != null)
            {
                _keyItems?.TryGetValue(name, out keyitem);
            }
            return keyitem;
        }

        public string GetKeyItems()
        {
            string names = "";
            Dictionary<string, KeyItem>.KeyCollection keys = _keyItems.Keys;
            foreach (string name in keys)
            {
                names += " " + name;
            }

            return names;
        }

        public int GetWeight()
        {
            return Limit;
        }

        public BackPack()
        {
            _items = new Dictionary<string, List<Item>>();
            _keyItems = new Dictionary<string, KeyItem>();
        }
    }
}
