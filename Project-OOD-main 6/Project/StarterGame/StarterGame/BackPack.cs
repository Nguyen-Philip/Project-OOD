using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;


namespace StarterGame
{ 
    public class BackPack
    {

        private int Limit = 50;
        private Dictionary<string, Item> _items;
        private Dictionary<string, KeyItem> _keyItems;

        public bool Add(Item item)
        {
            bool success = false;
            bool isIn = _items.ContainsKey(item.Name);
            if (item.CanBeHeld && item.Weight <= Limit)
            {
                if (isIn)
                {
                    _items[item.Name].Num += 1;
                }
                _items.Add(item.Name, item);
                Limit -= item.Weight;
                success = true;
            }
            return success;
        }

        public bool Remove(string name)
        {
            bool success = false;
            Item item = this.GetItem(name);
            item.Num -= 1;
            if (item.Num <= 0)
            {
                success = _items.Remove(name);
            }
            Limit += item.Weight;
            return success;
        }

        public Item GetItem(string name)
        {
            Item item = null;
            if (name != null)
            {
                _items?.TryGetValue(name, out item);
            }
            return item;
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
            _items = new Dictionary<string, Item>();
            _keyItems = new Dictionary<string, KeyItem>();
        }
    }
}
