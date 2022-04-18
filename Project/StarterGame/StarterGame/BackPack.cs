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
            if (item.CanBeHeld && item.Weight <= Limit)
            {
                _items.Add(item.Name, item);
                Limit -= item.Weight;
                success = true;
            }

            return success;
        }

        public bool Remove(Item item)
        {
            bool success = _items.Remove(item.Name);
            return success;
        }

        public string Inventory
        {
            get
            {
                string inventory = "";
                foreach (Item item in _items.Values)
                {
                    inventory += item + "\n";
                }

                return inventory;
            }
        }
        public BackPack()
        {
        }
    }
}
