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
                case Direction.Up: y--; player.Symbol = player.UpSymbol; break;
                case Direction.Down: y++; player.Symbol = player.DownSymbol; break;
                case Direction.Right: x++; player.Symbol = player.RightSymbol; break;
                case Direction.Left: x--; player.Symbol = player.LeftSymbol; break;
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

            // Change the symbol of the enemy according to it's moving direction
            enemy.ChangeDirectionSymbol(enemy.PosX,enemy.PosY,cell.X,cell.Y);
                
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