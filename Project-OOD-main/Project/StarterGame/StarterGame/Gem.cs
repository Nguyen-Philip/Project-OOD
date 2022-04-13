using System;
namespace StarterGame
{
    public class Gem : Item
    {
        private string _Name;
        private string _Location;
        private int _Value;
        private int _Weight;
        private bool _CanBeHeld = true;
        private bool _IsUsable = false;



        public string Name { set { _Name = value; } get { return _Name; } }
        public string Location { set { _Location = value; } get { return _Location; } }
        public int Value { set { _Value = value; } get { return _Value; } }
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
        public Gem(string name) : this(name, "NO LOCATION") { }
        public Gem(string name, string location) : this(name, location, 1) { }
        public Gem(string name, string location, int value) : this(name, location, value, 1) { }
        public Gem(string name, string location, int value, int weight)
        {
            _Name = name;
            _Location = location;
            _Value = value;
            _Weight = weight;
        }
    }
}
