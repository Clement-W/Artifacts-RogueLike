using RogueLike.Core;
using RogueSharp;

using System;
using System.Collections.Generic;
using System.Threading;
using RogueLike.Interfaces;

// deal with interactions between the game and the actors

namespace RogueLike.Systems
{

    public class CommandSystem
    {


        public bool MovePlayer(Player player, Direction direction, CurrentMap map)
        {
            int x = player.PosX;
            int y = player.PosY;

            bool didPlayerAct = false;

            System.Console.WriteLine(x + " " + y);


            switch (direction)
            {
                case Direction.Up:
                    y--; 
                    player.Symbol = player.UpSymbol; 
                    player.Direction = Direction.Up;
                    break;
                case Direction.Down:
                    y++; 
                    player.Symbol = player.DownSymbol;
                    player.Direction = Direction.Up;
                    break;
                case Direction.Right:
                    x++; 
                    player.Symbol = player.RightSymbol;
                    player.Direction = Direction.Up;
                    break;
                case Direction.Left:
                    x--; 
                    player.Symbol = player.LeftSymbol;
                    player.Direction = Direction.Up;
                    break;
            }



            if (map.SetCharacterPosition(player, x, y)) //move the player if possible
            {
                didPlayerAct = true;
            }


            return didPlayerAct;
        }



        public void MoveEnemy(Enemy enemy, ICell cell, CurrentMap map, Player player)
        {

            // Change the symbol of the enemy according to it's moving direction
            // We don't have the other symbols of the enemies yet so it doesn't do anything
            enemy.ChangeDirection(enemy.PosX, enemy.PosY, cell.X, cell.Y);


            // If the player is reachable, attack it, else move the enemy if possible
            if (!Attack(map, enemy, player))
            {
                map.SetCharacterPosition(enemy, cell.X, cell.Y);
            }

        }


        public bool Attack(CurrentMap map, ActiveCharacter attacker, Player player)
        {
            return attacker.Weapon.Attack(map, attacker, player);
        }


        public void PlayerAttack(Player player, CurrentMap map)
        {
            player.Weapon.Attack(map, player,player);
        }





        //Each turn, we check if the enemy can move according to it's necessary time period to move
        // If it can't move, we decrement it's counter. When the enemy can move, the remaning time period to move
        // is set to the necessary moving time period.
        public void MoveEnemies(Game game)
        {
            List<Enemy> enemies = game.Map.GetEnemies();
            foreach (Enemy enemy in enemies)
            {

                if (enemy.RemainingTimePeriodToMove == 0)
                {
                    enemy.RemainingTimePeriodToMove = enemy.MovingTimePeriod;
                    enemy.PerformAction(game);
                }
                else
                {
                    enemy.RemainingTimePeriodToMove--;
                }



            }
        }
    }
}