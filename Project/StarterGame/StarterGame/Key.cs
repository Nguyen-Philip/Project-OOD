﻿using System;
namespace StarterGame
{
    public class Key : KeyItem
    {
        private string _Name;
        private Room _Location;
        private int _Weight;
        private bool _CanBeHeld = true;
        private bool _IsUsable = true;
        private bool _CanBeDropped = false;

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
        public bool CanBeDropped
        {
            get
            {
                return _CanBeDropped;
            }
        }

        public Key(Room location, string name)
        {
            _Location = location;
            _Name = name;
            _CanBeHeld = true;
            _IsUsable = false;
            _CanBeDropped = false;
        }

        public Key(Room location, string name, int weight)
        {
            _Location = location;
            _Weight = weight;
            _Name = name;
            _CanBeHeld = true;
            _IsUsable = false;
            _CanBeDropped = false;
        }

        public static Key CreateKey(Room location, string name)
        {
            Key key = new Key(location, name);
            location.SetKeyItem(name, key);
            return key;
        }

        public static Key CreateKey(Room location, string name, int weight)
        {
            Key key = new Key(location, name, weight);
            location.SetKeyItem(name, key);
            return key;
        }
    }
}
