using System;
namespace StarterGame
{
    public class Gem : Item
    {
        private string _Name;
        private Room _Location;
        private int _Value;
        private int _Weight;
        private bool _CanBeHeld = true;
        private bool _IsUsable = false;

        public string Name { set { _Name = value; } get { return _Name; } }
        public Room Location { set { _Location = value; } get { return _Location; } }
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

        public Gem(Room location, string name)
        {
            _Location = location;
            _Name = name;
            _CanBeHeld = true;
            _IsUsable = false;
        }

        public Gem(Room location, string name, int value, int weight)
        {
            _Location = location;
            _Name = name;
            _Value = value;
            _Weight = weight;
            _CanBeHeld = true;
            _IsUsable = false;
        }

        public static Gem CreateGem(Room location, string name)
        {
            Gem gem = new Gem(location, name);
            location.AddItem(gem);
            return gem;
        }

        public static Gem CreateGem(Room location, string name, int value, int weight)
        {
            Gem gem = new Gem(location, name, value, weight);
            location.AddItem(gem);
            return gem;
        }
    }
}
