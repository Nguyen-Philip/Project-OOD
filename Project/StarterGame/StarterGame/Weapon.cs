﻿using System;
namespace StarterGame
{
    public class Weapon : Item
    {
        private string _Name;
        private Room _Location;
        private int _Value;
        private int _Weight;
        private int _AR;
        private int _Num;
        private bool _CanBeHeld = true;
        private bool _IsUsable = true;

        public string Name { set { _Name = value; } get { return _Name; } }
        public Room Location { set { _Location = value; } get { return _Location; } }
        public int Value { set { _Value = value; } get { return _Value; } }
        public int Weight { set { _Weight = value; } get { return _Weight; } }
        public int AR { set { _AR = value; } get { return _AR; } }
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

        public Weapon(Room location, string name)
        {
            _Location = location;
            _Name = name;
            _CanBeHeld = true;
            _IsUsable = false;
        }

        public Weapon(Room location, string name, int value, int weight, int ar, int num)
        {
            _Location = location;
            _Name = name;
            _Value = value;
            _Weight = weight;
            _AR = ar;
            _Num = num;
            _CanBeHeld = true;
            _IsUsable = false;
        }

        public static Weapon CreateWeapon(Room location, string name)
        {
            Weapon weapon = new Weapon(location, name);
            location.AddItem(weapon);
            return weapon;
        }

        public static Weapon CreateWeapon(Room location, string name, int value, int weight, int ar, int num)
        {
            Weapon weapon = new Weapon(location, name, value, weight, ar, num);
            location.AddItem(weapon);
            return weapon;
        }
        public Item Clone()
        {
            return null;  // TODO: Fix This!!
        }
    }
}
