using System;
namespace StarterGame
{
   
    public interface Item : IEntity
    {
        int Value { set; get; }
        int Weight { set; get; }
        bool CanBeHeld { get; }
        bool IsUsable { get; }
        /*
        private string _Name;
        private string _Location;
        private int _Value;
        private bool _CanBeHeld;
        private bool _IsUsable;
        
        public string Name { set { _Name = value; } get { return _Name; } }
        public string Location { set { _Location = value; } get { return _Location; } }
        public int Value { set { _Value = value; } get { return _Value; } }
        public bool CanBeHeld{ set { _CanBeHeld = value; } get { return _CanBeHeld; } }
        public bool IsUsable { set { _IsUsable = value; } get { return _IsUsable; } }
        public Item(string name) : this(name, "NO LOCATION", 0) { }
        public Item(string name, string location) : this(name, location, 0) { }
        public Item(string name, string location, int value) : this(name, location, value, true, true) { }
        public Item(string name, string location, int value, bool canBeUsed, bool isUsable)
        
        {
            _Name = name;
            _Location = location;
            _Value = value;
            _CanBeHeld = canBeUsed;
            _IsUsable = isUsable;
        }
        */
    }
}
