using RogueLike.Core;
using RogueSharp;

using System;
using System.Collections.Generic;

namespace RogueLike.Systems
{

    public class CommandSystem
    {


        public bool MovePlayer(Player player, Direction direction, CurrentMap map)
        {
            int x = player.PosX;
            int y = player.PosY;

            switch (direction)
            {
                default: return false;
                case Direction.Up: y--; break;
                case Direction.Down: y++; break;
                case Direction.Right: x++; break;
                case Direction.Left: x--; break;
            }

            // return true if the player move
            if (map.SetCharacterPosition(player, x, y))
            { // move the player if possible
                return true;
            }

            Enemy enemy = map.GetEnemyAt(x, y);
            if (enemy != null)
            {
                Attack(player, enemy);
                return true;
            }

            return false;
        }

        public void MoveEnemy(Enemy enemy, ICell cell, CurrentMap map, Player player)
        {
            // Try to move the enemy, if the enemy don't move and if the player is on the desired cell, attack the player
            if (!map.SetCharacterPosition(enemy, cell.X, cell.Y))
            {
                if (player.PosX == cell.X && player.PosY == cell.Y)
                {
                    Attack(enemy, player);
                }
            }
        }

        public void Attack(ActiveCharacter attacker, ActiveCharacter defender)
        {
            //TODO: trouver un mecanisme d'attaque en s'inspirant du système des tests et en prenant en compte la défense
        }


        //provisoire
        public void MoveEnemies(Game game)
        {
            List<Enemy> enemies = game.Map.GetEnemies();
            foreach (Enemy enemy in enemies)
            {

                enemy.PerformAction(game);



            }
        }
    }
}