using RogueLike.Interfaces;
using RogueLike.Core;
using RogueLike.Systems;
using System;
using RogueLike.Core.Enemies;

using RogueSharp;

namespace RogueLike.Core.Behaviors
{
    /// <summary>
    /// This class describes a specific behavior where the enemy will chase the player with A* pathfinding
    /// </summary>
    public class ChasePlayer : IBehavior
    {

        /// <summary>
        /// Allow an enemy to act when it has to make a move, here it chase the player
        /// </summary>
        /// <param name="enemy">The enemy that needs to acts</param>
        /// <param name="player">The player to attack if encountered</param>
        /// <param name="map">The map where the enemy and the player are situated</param>
        /// <param name="command">The command system is used to take an action on the game</param>
        public void Act(Enemy enemy, Player player, CurrentMap map, CommandSystem commandSystem)
        {

            // Create a FieldOfView object to compute the fov of the enemy
            FieldOfView enemyFov = new FieldOfView(map);

            // If the enemy is not alerted
            if (enemy.NbTurnsAlerted == 0)
            {
                //Compute it's FOV
                enemyFov.ComputeFov(enemy.PosX, enemy.PosY, enemy.Awareness, true);
                // If the player is in the player fov
                if (enemyFov.IsInFov(player.PosX, player.PosY))
                {
                    // The enemy is now alerted
                    enemy.NbTurnsAlerted = 1;
                }
         
            }

            else
            { // The enemy is already alerted : move towards the player

               
                // To use the roguesharp findPath, the origin and the destination needs to be walkable
                map.SetCellWalkability(enemy.PosX, enemy.PosY, true);
                map.SetCellWalkability(player.PosX, player.PosY, true);

                PathFinder pathFinder = new PathFinder(map); // Create a path finder object 

                // Shortest path between the player and the enemy
                Path path = null;
                try
                {
                    path = pathFinder.ShortestPath(map.GetCell(enemy.PosX, enemy.PosY), map.GetCell(player.PosX, player.PosY));
                    // uses A* algorithm
                }
                catch (PathNotFoundException) { }

                // We can now set the cells walkable
                map.SetCellWalkability(enemy.PosX, enemy.PosY, false);
                map.SetCellWalkability(player.PosX, player.PosY, false);

                if (path != null)
                {
                    try
                    {
                        // Move the enemy thanks to the command system
                        // This method will also check if it's possible to attack the player
                        // and do it if so
                        commandSystem.MoveEnemy(enemy, path.StepForward(), map, player);
                    }
                    catch (NoMoreStepsException) { }
                }

                // Increment the number of alerted turns
                enemy.NbTurnsAlerted++;

                // The enemy stops chasing the player after 10 turns if the player is not in it's fov anymore
                enemyFov.ComputeFov(enemy.PosX, enemy.PosY, enemy.Awareness, true);
                if (enemy.NbTurnsAlerted > 10 && !enemyFov.IsInFov(player.PosX, player.PosY))
                {
                    enemy.NbTurnsAlerted = 0;
                }
             
            }

        }
    }
}