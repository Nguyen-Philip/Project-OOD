using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;


namespace StarterGame
{
    public class Shop
    {

        private Dictionary<string, List<Item>> _items;

        public bool Add(Item item)
        {
            bool success = false;
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
            success = true;

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
                    success = _items.Remove(name);
                }
                else
                {
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
                names += " " + name + "[Quantity: " + _items[name].Count + "][Cost: " + _items[name][0].Value + "]";
            }

            return names;
        }


        public Shop()
        {
            _items = new Dictionary<string, List<Item>>();
        }
    }
}