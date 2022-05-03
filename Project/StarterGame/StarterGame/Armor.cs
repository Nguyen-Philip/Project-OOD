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
        public int _Num;

        public string Name { set { _Name = value; } get { return _Name; } }
        public Room Location { set { _Location = value; } get { return _Location; } }
        public int Value { set { _Value = value; } get { return _Value; } }
        public int Weight { set { _Weight = value; } get { return _Weight; } }
        public int AV { set { _AV = value; } get { return _AV; } }
        public int Num { set { _Num = value; } get { return _Num; } }

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

        public Armor(Room location, string name)
        {
            _Location = location;
            _Name = name;
            _CanBeHeld = true;
            _IsUsable = false;
        }

        public Armor(Room location, string name, int value, int weight, int av, int num)
        {
            _Location = location;
            _Name = name;
            _Value = value;
            _Weight = weight;
            _AV = av;
            _CanBeHeld = true;
            _IsUsable = false;
            _Num = num;
        }

        public static Armor CreateArmor(Room location, string name)
        {
            Armor armor = new Armor(location, name);
            location.AddItem(armor);
            return armor;
        }

        public static Armor CreateArmor(Room location, string name, int value, int weight, int av, int num)
        {
            Armor armor = new Armor(location, name, value, weight, av, num);
            location.AddItem(armor);
            return armor;
        }

        public Item Clone()
        {
            return null;  // TODO: Fix This!!
        }
    }
}