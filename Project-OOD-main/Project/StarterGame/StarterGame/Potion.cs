using System;
namespace StarterGame
{
    public class Potion : Item
    {
        public enum TYPE { HP, AR, BAD }
        private string _Name;
        private string _Location;
        private int _Value;
        private int _Weight;
        private int _Modifier;
        public TYPE _Type;
        private bool _CanBeHeld = true;
        private bool _IsUsable = true;



        public string Name { set { _Name = value; } get { return _Name; } }
        public string Location { set { _Location = value; } get { return _Location; } }
        public int Value { set { _Value = value; } get { return _Value; } }
        public int Weight { set { _Weight = value; } get { return _Weight; } }
        public int Modifier { set { _Modifier = value; } get { return _Modifier; } }
        public TYPE Type { set { _Type = value; } get { return _Type; } }
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
        public Potion(string name) : this(name, "NO LOCATION") { }
        public Potion(string name, string location) : this(name, location, 0) { }
        public Potion(string name, string location, int value) : this(name, location, value, 0) { }
        public Potion(string name, string location, int value, int weight) : this(name, location, value, weight, 1) { }
        public Potion(string name, string location, int value, int weight, int modifier) : this(name, location, value, weight, modifier, TYPE.HP) { }
        public Potion(string name, string location, int value, int weight, int modifier, TYPE type)
        {
            _Name = name;
            _Location = location;
            _Value = value;
            _Weight = weight;
            _Modifier = modifier;
            _Type = type;
        }
    }
}
