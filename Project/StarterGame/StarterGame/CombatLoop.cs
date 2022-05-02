using System;
namespace StarterGame
{
    public class CombatLoop
    {
        public Player _Player;
        public Enemy _Enemy;
        public bool victory;
        public int limit = 10;

        public int pDamage(int damage)
        {
            damage -= _Player.Av;
            if (damage < 0)
            {
                damage = 0;
            }
            _Player.Hp -= damage;
            _Player.NotificationMessage("\nThe " + _Enemy.Name + " have attack you and dealt " + damage + " damage");
            _Player.NotificationMessage("\nPlayer Hp: " + _Player.Hp);
            return _Player.Hp;
        }

        public int eDamage(int damage)
        {
            _Enemy.Hp -= damage;
            _Player.NotificationMessage("\nYou have attack the " + _Enemy.Name + " and dealt " + _Player.Ar + " damage");
            _Player.NotificationMessage("\nEnemy Hp: " + _Enemy.Hp);
            return _Enemy.Hp;
        }

        public void Victory()
        {
            _Player.NotificationMessage("\n" + _Enemy.Name + " has died");
            _Enemy.RemoveItems();
            _Enemy.RemoveKeyItems();
            _Player.NotificationMessage("\nThe " + _Enemy.Name + " has dropped everything it was carrying");
            _Player.NotificationMessage("\nYou have picked up " + _Enemy.Gold + " gold");
            _Player.NotificationMessage("\nYou have won");
            _Player.Gold += _Enemy.Gold;

            _Player.Xp += _Enemy.XP;
            while(_Player.Xp >= limit)
            {
                _Player.Level += 1;
                _Player.Xp -= limit;
                limit += 10;
                _Player.NotificationMessage("\nYou have leveled up! Level: " + _Player.Level);
                _Player.MaxHp += 5;
                _Player.NotificationMessage("\nYou have gained 5 maximum hp. Max Hp: " + _Player.MaxHp);
                _Player.Hp = _Player.MaxHp;
                _Player.Ar += 1;
                _Player.NotificationMessage("\nYou have gain 1 attack rating. Ar: " + _Player.Ar);
            }
            _Player.CurrentRoom.RemoveEnemy(_Enemy.Name);
            _Player.LocationMessage("\n" + _Player.CurrentRoom.Description());
        }

        public bool comparePriority()
        {
            bool isPlayerFaster = false;
            Random random = new Random();
            while (_Player.Priority == _Enemy.Priority)
            {
                _Player.Priority = random.Next(0, 3);
            }
            if (_Player.Priority < _Enemy.Priority)
            {
                _Player.Priority = 1;
                isPlayerFaster = true;
            }
            else if (_Player.Priority > _Enemy.Priority)
            {
                _Player.Priority = 1;
                isPlayerFaster = false;
            }
            return isPlayerFaster;
        }

        public void Loop(Enemy enemy)
        {
            Parser _parser = new Parser(new CommandWords());
            bool finished = false;
            while (finished == false)
            {
                Console.Write("\n>");
                String temp = Console.ReadLine();
                Command command = _parser.ParseCommand(temp);
                if (command != null)
                {
                    if (command.Name == "attack" || command.Name == "heal" || command.Name == "inventory" || command.Name == "inspect")
                    {
                        if (command.Name == "attack" && command.SecondWord == enemy.Name)
                        {
                            command.Execute(_Player);
                            finished = true;
                        }
                        else if (command.Name == "heal")
                        {
                            command.Execute(_Player);
                            _Player.NotificationMessage("\nThe " + _Enemy.Name + " begins to stare at you");
                        }
                        else if (command.Name == "inventory" || command.Name == "inspect" || command.Name == "stats")
                        {
                            command.Execute(_Player);
                            _Player.NotificationMessage("\nThe " + _Enemy.Name + " begins to stare at you");
                        }
                        else
                        {
                            _Player.ErrorMessage("\nYou need to type the enemy's name correctly");
                            _Player.NotificationMessage("\nThe " + _Enemy.Name + " begins to stare at you");
                        }
                    }
                    else
                    {
                        _Player.ErrorMessage("\nYou are unable to do anything but attack, heal, inspect your items, look at your stats, and look in your inventory");
                        finished = false;
                    }
                }
                else
                {
                    _Player.ErrorMessage("\nYou are unable to do anything but attack, use items, or run");
                    finished = false;
                }
            }
        }

        public CombatLoop(Player player, Enemy enemy)
        {
            _Player = player;
            _Enemy = enemy;
        }
    }
}
