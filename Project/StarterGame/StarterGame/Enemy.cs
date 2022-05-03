using System;
using System.Collections.Generic;
using System.Text;
namespace StarterGame
{

    public class Enemy : IEntity
    {

        private string _Name;
        private Room _Location;
        private bool _CanTalk = false;
        private int _Hp;
        private int _Ar;
        private int _Priority;
        private int _Xp;
        private int _Gold;
        private Dictionary<string, Item> _Drops;

        public void Add(Item item)
        {
            _Drops.Add(item.Name, item);
            _Location.RemoveItem(item.Name);
        }

        public void RemoveItems()
        {
            Dictionary<string, Item>.KeyCollection keys = _Drops.Keys;
            foreach (string item in keys)
            {
                _Location.SetItem(item, _Drops[item]);
            }
        }

        public Item GetItem(string name)
        {
            Item item = null;
            if (name != null)
            {
                _Drops?.TryGetValue(name, out item);
            }
            return item;
        }

        public string Name { set { _Name = value; } get { return _Name; } }
        public Room Location { set { _Location = value; } get { return _Location; } }
        public bool CanTalk { set { _CanTalk = value; } get { return _CanTalk; } }
        public int Hp { set { _Hp = value; } get { return _Hp; } }
        public int Ar { set { _Ar = value; } get { return _Ar; } }
        public int Priority { set { _Priority = value; } get { return _Priority; } }
        public int XP { set { _Xp = value; } get { return _Xp; } }
        public int Gold { set { _Gold = value; } get { return _Gold; } }

        public Enemy(Room location, string name)
        {
            _Location = location;
            _Name = name;
            _Drops = new Dictionary<string, Item>();
        }

        public Enemy(Room location, string name, int hp, int ar, int priority)
        {
            _Location = location;
            _Name = name;
            _Hp = hp;
            _Ar = ar;
            _Priority = priority;
            _Drops = new Dictionary<string, Item>();
        }

        public Enemy(Room location, string name, int hp, int ar, int priority, int xp, int gold)
        {
            _Location = location;
            _Name = name;
            _Hp = hp;
            _Ar = ar;
            _Xp = xp;
            _Gold = gold;
            _Priority = priority;
            _Drops = new Dictionary<string, Item>();
        }

        public static Enemy CreateEnemy(Room location, string name)
        {
            Enemy enemy = new Enemy(location, name);
            location.SetEnemy(name, enemy);
            return enemy;
        }

        public static Enemy CreateEnemy(Room location, string name, int hp, int ar, int priority)
        {
            Enemy enemy = new Enemy(location, name, hp, ar, priority);
            location.SetEnemy(name, enemy);
            return enemy;
        }

        public static Enemy CreateEnemy(Room location, string name, int hp, int ar, int priority, int xp, int gold)
        {
            Enemy enemy = new Enemy(location, name, hp, ar, priority, xp, gold);
            location.SetEnemy(name, enemy);
            return enemy;
        }
    }
}
