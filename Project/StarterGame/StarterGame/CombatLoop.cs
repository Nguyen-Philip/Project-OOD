using System;
namespace StarterGame
{
    public class CombatLoop
    {
        public Player _Player;
        public Enemy _Enemy;
        public bool victory;

        public int pDamage(int damage)
        {
            _Player.HP -= damage;
            return _Player.HP;
        }

        public int eDamage(int damage)
        {
            _Enemy.HP -= damage;
            return _Enemy.HP;
        }



        public CombatLoop(Player player, Enemy enemy)
        {
            _Player = player;
            _Enemy = enemy;
        }
    }
}
