using System;
namespace StarterGame
{
    public class Potion : Item
    {
        public enum TYPE { HP, AR, BAD }
        private string _Name;
        private Room _Location;
        private int _Value;
        private int _Weight;
        private int _Modifier;
        public TYPE _Type;
        private bool _CanBeHeld = true;
        private bool _IsUsable = true;



        public string Name { set { _Name = value; } get { return _Name; } }
        public Room Location { set { _Location = value; } get { return _Location; } }
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

        public Potion(Room location)
        {
            _Location = location;
            _CanBeHeld = true;
            _IsUsable = false;
        }

        public Potion(Room location, int value, int weight, int modifier)
        {
            _Location = location;
            _Value = value;
            _Weight = weight;
            _Modifier = modifier;
            _CanBeHeld = true;
            _IsUsable = false;
        }

        public Potion(Room location, int value, int weight, int modifier, string type)
        {
            _Location = location;
            _Value = value;
            _Weight = weight;
            _Modifier = modifier;
            _Type = (TYPE) Enum.Parse(typeof(TYPE), type, true);
            _CanBeHeld = true;
            _IsUsable = false;
        }

        public static Potion CreatePotion(Room location, string name)
        {
            Potion potion = new Potion(location);
            location.SetPotion(name, potion);
            return potion;
        }

        public static Potion CreatePotion(Room location, string name, int value, int weight, int modifier, string type)
        {

            Potion potion = new Potion(location, value, weight, modifier, type);
            location.SetPotion(name, potion);
            return potion;
        }
    }
}
