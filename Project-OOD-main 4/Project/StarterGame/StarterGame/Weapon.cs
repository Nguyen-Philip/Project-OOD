using System;
namespace StarterGame
{
    public class Weapon : Item
    {
        private string _Name;
        private Room _Location;
        private int _Value;
        private int _Weight;
        private int _AR;
        private bool _CanBeHeld = true;
        private bool _IsUsable = true;

        public string Name { set { _Name = value; } get { return _Name; } }
        public Room Location { set { _Location = value; } get { return _Location; } }
        public int Value { set { _Value = value; } get { return _Value; } }
        public int Weight { set { _Weight = value; } get { return _Weight; } }
        public int AR { set { _AR = value; } get { return _AR; } }

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

        public Weapon(Room location)
        {
            _Location = location;
            _CanBeHeld = true;
            _IsUsable = false;
        }

        public Weapon(Room location, string name, int value, int weight, int ar)
        {
            _Location = location;
            _Name = name;
            _Value = value;
            _Weight = weight;
            _AR = ar;
            _CanBeHeld = true;
            _IsUsable = false;
        }

        public static Weapon CreateWeapon(Room location, string name)
        {
            Weapon weapon = new Weapon(location);
            location.SetItem(name, weapon);
            return weapon;
        }

        public static Weapon CreateWeapon(Room location, string name, int value, int weight, int ar)
        {
            Weapon weapon = new Weapon(location, name, value, weight, ar);
            location.SetItem(name, weapon);
            return weapon;
        }
    }
}
