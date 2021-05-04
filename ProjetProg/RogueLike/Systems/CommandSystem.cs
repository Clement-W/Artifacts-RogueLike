using RogueLike.Core;
using RogueSharp;

using System;
using System.Collections.Generic;
using System.Threading;
using RogueLike.Interfaces;

// deal with interactions between the game and the actors

namespace RogueLike.Systems
{

    /// <summary>
    /// This class deal with interactions between the game and the active characters
    /// that need to take action on the map
    /// </summary>
    public class CommandSystem
    {

        /// <summary>
        /// This method allows to move the player on the map
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="direction">The direction that the player wants to move</param>
        /// <param name="map">The map</param>
        /// <returns>Return true if the player move</returns>
        public bool MovePlayer(Player player, Direction direction, CurrentMap map)
        {
            // Get the coordinates of the player
            int x = player.PosX;
            int y = player.PosY;

            bool didPlayerAct = false;

            // Move the player according to the direction
            switch (direction)
            {
                case Direction.Up:
                    y--; // Change the saved coordinate of the player
                    player.Symbol = player.UpSymbol; // Change the symbol of the player so it looks upward
                    player.Direction = Direction.Up; // Change the player direction
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


            // Change the player position if possible with the new x and y coordinates
            if (map.SetCharacterPosition(player, x, y))
            {
                didPlayerAct = true;
            }


            return didPlayerAct;
        }


        /// <summary>
        /// Move an enemy on the map
        /// </summary>
        /// <param name="enemy">The moving enemy</param>
        /// <param name="cell">The cell that the enemy wants to reach</param>
        /// <param name="map">The map</param>
        /// <param name="player">The player instance is needed to know if the enemy can attack it</param>
        public void MoveEnemy(Enemy enemy, ICell cell, CurrentMap map, Player player)
        {

            // Change the symbol of the enemy according to it's moving direction
            // We don't have the other symbols of the enemies in the sprites yet so it doesn't do anything
            // With more time, the sprites would have been included in the game.
            enemy.ChangeDirection(enemy.PosX, enemy.PosY, cell.X, cell.Y);


            // If the player is reachable, attack it, else move the enemy if possible
            if (!Attack(map, enemy, player))
            {
                map.SetCharacterPosition(enemy, cell.X, cell.Y);
            }

        }

        /// <summary>
        /// This method allows an active character to attack
        /// </summary>
        /// <param name="map"> The map</param>
        /// <param name="attacker"> The attacker (could be the player or the enemy)</param>
        /// <param name="player"> We need the player instance if the attacker is the enemy</param>
        /// <returns></returns>
        public bool Attack(CurrentMap map, ActiveCharacter attacker, Player player)
        {
            return attacker.Weapon.Attack(map, attacker, player);
        }

    

        /// <summary>
        /// Each turn, we check if the enemy can move according to it's necessary time period to move
        /// If it can't move, we decrement it's counter. When the enemy can move, the remaning time period to move
        /// is set to the necessary moving time period. 
        /// </summary>
        /// <param name="game">The game instance is needed to get the player, the map, the enemies and the command system</param>
        public void MoveEnemies(Game game)
        {
            // Get the enemies alive in the map
            List<Enemy> enemies = game.Map.GetEnemies();

            // Loop into those enemies
            foreach (Enemy enemy in enemies)
            {
                // If the remaining time period to move is equal to 0, move the enemy
                // and reset it's remaining time period to it's necessary moviging time period
                if (enemy.RemainingTimePeriodToMove == 0)
                {
                    enemy.RemainingTimePeriodToMove = enemy.MovingTimePeriod;
                    enemy.PerformAction(game.Player, game.Map, game.CommandSystem);
                }
                else
                {
                    // else, decrements the remaining time period to move
                    enemy.RemainingTimePeriodToMove--;
                }
            }
        }
    }
}