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
                _items?.Add(item.Name, item);
                Limit -= item.Weight;
                success = true;
            }

            return success;
        }

        public Item Remove(string item)
        {
            Item thing = _items?[item];
            if (thing != null)
            {
                _items?.Remove(item);
                return thing;
            }
            else
            {
                return null;
            }
        }

        public string Inventory
        {
            get
            {
                string inventory = "";
                foreach (Item item in _items?.Values)
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
