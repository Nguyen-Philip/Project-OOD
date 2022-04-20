using System;
namespace StarterGame
{
    public class Armor : Item
    {
        private string _Name;
        private Room _Location;
        private int _Value;
        private int _Weight;
        private int _AV;
        private bool _CanBeHeld = true;
        private bool _IsUsable = true;

        public string Name { set { _Name = value; } get { return _Name; } }
        public Room Location { set { _Location = value; } get { return _Location; } }
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

        public Armor(Room location)
        {
            _Location = location;
            _CanBeHeld = true;
            _IsUsable = false;
        }

        public Armor(Room location, int value, int weight, int av)
        {
            _Location = location;
            _Value = value;
            _Weight = weight;
            _AV = av;
            _CanBeHeld = true;
            _IsUsable = false;
        }

        public static Armor CreateArmor(Room location, string name)
        {
            Armor armor = new Armor(location);
            location.SetArmor(name, armor);
            return armor;
        }

        public static Armor CreateArmor(Room location, string name, int value, int weight, int av)
        {
            Armor armor = new Armor(location, value, weight, av);
            location.SetArmor(name, armor);
            return armor;
        }
    }
}
