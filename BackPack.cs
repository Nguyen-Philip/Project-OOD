using System;
using System.Collections.Generic;
using System.Text;

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
            if (item.CanBeHeld && item.Weight <= Limit)
            {
                _items.Add(item.Name, item);
                Limit -= item.Weight;
                success = true;
            }
            return success;
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

        public bool Remove(string name)
        {
            Item item = this.GetItem(name);
            bool success = _items.Remove(name);
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

        public KeyItem GetKeyItem(string name)
        {
            KeyItem keyitem = null;
            if (name != null)
            {
                _keyItems?.TryGetValue(name, out keyitem);
            }
            return keyitem;
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
