using System;
using System.Collections.Generic;
using System.Text;
namespace StarterGame
{

    public class Enemy : IEntity
    {

        private string _Name;
        private Room _Location;
        private bool _CanTalk = false;
        private int _Hp;
        private int _Ar;
        private int _Priority;
        private Dictionary<string, Item> _Drops;
        private List<Item> _Dropped;

        public bool Add(Item item)
        {
            bool success = false;
            {
                _Drops.Add(item.Name, item);
                success = true;
            }

            return success;
        }

        public bool Drop(Item item)
        {
            foreach (Item Drop in _Drops.Values)
            {
                _Dropped.Add(Drop);
            }
            return true;
        }

        public string Drops
        {
            get
            {
                string Drops = "";
                foreach (Item item in _Drops.Values)
                {
                    Drops += item + "\n";
                }

                return Drops;
            }
        }

        public string Name { set { _Name = value; } get { return _Name; } }
        public Room Location { set { _Location = value; } get { return _Location; } }
        public bool CanTalk { set { _CanTalk = value; } get { return _CanTalk; } }
        public int Hp { set { _Hp = value; } get { return _Hp; } }
        public int Ar { set { _Ar = value; } get { return _Ar; } }
        public int Priority { set { _Priority = value; } get { return _Priority; } }
        public List<Item> Dropped { get { return _Dropped; } }

        /*public Enemy(string name) : this(name, "NO LOCATION") { }
        public Enemy(string name, string location) : this(name, location, 1, 1, 2) { }
        public Enemy(string name, string location, int hp) : this(name, location, hp, 1, 2) { }
        public Enemy(string name, string location, int hp, int ar) : this(name, location, hp, ar, 2) { }
        public Enemy(string name, string location, int hp, int ar, int priority)
        {
            _Name = name;
            _Location = location;
            _Hp = hp;
            _Ar = ar;
            _Priority = priority;
        }*/

    }
}
