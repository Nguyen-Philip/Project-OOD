using System;
namespace StarterGame
{
    public class Key : KeyItems
    {
        private string _Name;
        private Room _Location;
        private int _Weight;
        private bool _CanBeHeld = true;
        private bool _IsUsable = true;

        public string Name { set { _Name = value; } get { return _Name; } }
        public Room Location { set { _Location = value; } get { return _Location; } }
        public int Weight { set { _Weight = value; } get { return _Weight; } }

        public bool CanBeHeld {
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

        public Key(Room location)
        {
            _Location = location;
            _CanBeHeld = true;
            _IsUsable = false;
        }

        public Key(Room location, int weight)
        {
            _Location = location;
            _Weight = weight;
            _CanBeHeld = true;
            _IsUsable = false;
        }

        public static Key CreateKey(Room location, string name)
        {
            Key key = new Key(location);
            location.SetKey(name, key);
            return key;
        }

        public static Key CreateKey(Room location, string name, int weight)
        {
            Key key = new Key(location, weight);
            location.SetKey(name, key);
            return key;
        }
    }
}
