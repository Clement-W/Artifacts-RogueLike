using RogueLike.Core;
using RogueSharp;

using System;
using System.Collections.Generic;
using System.Threading;

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


            switch (direction)
            {
                case Direction.Up: y--; player.Symbol = player.UpSymbol; break;
                case Direction.Down: y++; player.Symbol = player.DownSymbol; break;
                case Direction.Right: x++; player.Symbol = player.RightSymbol; break;
                case Direction.Left: x--; player.Symbol = player.LeftSymbol; break;
            }


            // here, the condition == true if the player don't move
            if (!map.SetCharacterPosition(player, x, y)) //move the player if possible
            {
                Enemy enemy = map.GetEnemyAt(x, y);
                if (enemy != null)
                {
                    Attack(player, enemy);
                    didPlayerAct = true;
                }

            }
            else
            {
                didPlayerAct = true;
            }

            return didPlayerAct;
        }

        public void MoveEnemy(Enemy enemy, ICell cell, CurrentMap map, Player player)
        {

            // Change the symbol of the enemy according to it's moving direction
            enemy.ChangeDirectionSymbol(enemy.PosX, enemy.PosY, cell.X, cell.Y);


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
            // Fait clignoter l'enemi s'il se prend des degats
            defender.Health -= attacker.Attack; // provisoire

            Thread FlashThread = new Thread(new ThreadStart(defender.ChangeColorAfterHit));
            // Put the change color method in a thread to let the game continue during the color changement
            // Without a thread runing in background, the color changement is not visible
            FlashThread.Start();


            //proba de faire un coup critique


        }


        //provisoire
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