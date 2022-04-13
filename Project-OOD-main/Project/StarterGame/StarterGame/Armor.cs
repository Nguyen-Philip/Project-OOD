using System;
namespace StarterGame
{
    public class Armor : Item
    {
        private string _Name;
        private string _Location;
        private int _Value;
        private int _Weight;
        private int _AV;
        private bool _CanBeHeld = true;
        private bool _IsUsable = true;



        public string Name { set { _Name = value; } get { return _Name; } }
        public string Location { set { _Location = value; } get { return _Location; } }
        public int Value { set { _Value = value; } get { return _Value; } }
        public int Weight { set { _Weight = value; } get { return _Weight; } }
        public int AV { set { _AV = value; } get { return _AV; } }
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
        public Armor(string name) : this(name, "NO LOCATION") { }
        public Armor(string name, string location) : this(name, location, 0) { }
        public Armor(string name, string location, int value) : this(name, location, value, 0) { }
        public Armor(string name, string location, int value, int weight) : this(name, location, value, weight, 1) { }
        public Armor(string name, string location, int value, int weight, int av)
        {
            _Name = name;
            _Location = location;
            _Value = value;
            _Weight = weight;
            _AV = av;
        }
    }
}
