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
        private int _Num;

        public int Num { set { _Num = value; } get { return _Num; } }
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

        public Potion(Room location, string name)
        {
            _Location = location;
            _Name = name;
            _CanBeHeld = true;
            _IsUsable = false;
        }

        public Potion(Room location, string name, int value, int weight, int modifier, int num)
        {
            _Location = location;
            _Name = name;
            _Value = value;
            _Weight = weight;
            _Modifier = modifier;
            _CanBeHeld = true;
            _IsUsable = false;
            _Num = num;
        }

        public Potion(Room location, string name, int value, int weight, int modifier, int num, TYPE type)
        {
            _Location = location;
            _Name = name;
            _Value = value;
            _Weight = weight;
            _Modifier = modifier;
            _Type = type;
            _Num = num;
            _CanBeHeld = true;
            _IsUsable = false;
        }

        public static Potion CreatePotion(Room location, string name)
        {
            Potion potion = new Potion(location, name);
            location.SetItem(name, potion);
            return potion;
        }

        public static Potion CreatePotion(Room location, string name, int value, int weight, int modifier, int num, TYPE type)
        {

            Potion potion = new Potion(location, name, value, weight, modifier, num, type);
            location.SetItem(name, potion);
            return potion;
        }
        public Item Clone()
        {
            Potion potion = new Potion(this.Location, this.Name, this.Value, this.Weight, this.Modifier, this.Num, this.Type);
            return potion;
        }
    }
}
