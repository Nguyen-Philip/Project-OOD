using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{ 
    public class BackPack
    {

        private int Limit = 50;
        private Dictionary<string, Item> _items;

        public bool Add(Item item)
        {
            bool success = false;
            if (item.CanBeHeld && item.Weight <= Limit && item.Name != null)
            {
                _items.Add(item.Name, item);
                Limit -= item.Weight;
                success = true;
            }

            return success;
        }

        public void RemoveItem(string name)
        {
            _items.Remove(name);
        }

        public Item GetItem(string name)
        {
            Item item = null;
            _items.TryGetValue(name, out item);
            return item;

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
        public BackPack()
        {
            _items = new Dictionary<string, Item>();
        }
    }
}
