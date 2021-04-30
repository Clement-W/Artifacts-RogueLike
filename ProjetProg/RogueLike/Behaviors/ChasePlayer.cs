using RogueLike.Interfaces;
using RogueLike.Core;
using RogueLike.Systems;
using System;

using RogueSharp;

namespace RogueLike.Behaviors
{
    public class ChasePlayer : IBehavior
    {
        public bool Act(Enemy enemy, Game game)
        {

            CurrentMap map = game.Map;
            Player player = game.Player;
            FieldOfView enemyFov = new FieldOfView(map);

            // Check if the player is in the monster fov



            if (enemy.NbTurnsAlerted == null)
            {
               
                enemyFov.ComputeFov(enemy.PosX, enemy.PosY, enemy.Awareness, true);
                if (enemyFov.IsInFov(player.PosX, player.PosY))
                {
                    Game.MessageLog.AddMessage($"A {enemy.Name} saw you.");
                    enemy.NbTurnsAlerted = 1;
                }
         
            }

            else
            { // The enemy is already alerted : move or attack

               
                // To use the roguesharp findPath, the origin and the targeted cell needs to be walkable
                map.SetCellWalkability(enemy.PosX, enemy.PosY, true);
                map.SetCellWalkability(game.Player.PosX, game.Player.PosY, true);

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
                map.SetCellWalkability(game.Player.PosX, game.Player.PosY, false);

                if (path != null)
                {
                    try
                    {
                        game.CommandSystem.MoveEnemy(enemy, path.StepForward(), map, player);
                    }
                    catch (NoMoreStepsException) { }
                }

                enemy.NbTurnsAlerted++;

                // The enemy stops to chase the player after 10 turns 
                enemyFov.ComputeFov(enemy.PosX, enemy.PosY, enemy.Awareness, true);
                if (enemy.NbTurnsAlerted > 10 && !enemyFov.IsInFov(player.PosX, player.PosY))
                {
                    enemy.NbTurnsAlerted = null;
                }
             
            }

            return true;



        }
    }
}