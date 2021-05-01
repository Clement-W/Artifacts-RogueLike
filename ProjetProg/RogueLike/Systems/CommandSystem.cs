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
                case Direction.Up: y--; player.Symbol = player.UpSymbol; 
                    break;
                case Direction.Down: y++; player.Symbol = player.DownSymbol; 
                    break;
                case Direction.Right: x++; player.Symbol = player.RightSymbol; 
                    break;
                case Direction.Left: x--; player.Symbol = player.LeftSymbol; 
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
            enemy.ChangeDirectionSymbol(enemy.PosX, enemy.PosY, cell.X, cell.Y);


            // Try to move the enemy, if the enemy don't move and if the player is on the desired cell, attack the player
            if (!map.SetCharacterPosition(enemy, cell.X, cell.Y))
            {
                if (player.PosX == cell.X && player.PosY == cell.Y)
                {
                    EnemyAttack(enemy, player);
                }
            }
        }

        public void EnemyAttack(Enemy attacker, Player defender)
        {

            int damageValue = attacker.Attack - defender.Defense;

            defender.Health -= (damageValue <= 0) ? 1 : damageValue;

            Thread FlashThread = new Thread(new ThreadStart(defender.ChangeColorAfterHit));
            // Put the change color method in a thread to let the game continue during the color changement
            // Without a thread runing in background, the color changement is not visible
            FlashThread.Start();


            //proba de faire un coup critique 5%
            //TODO

        }

        public void PlayerAttack(Player player, CurrentMap map){
            //TODO
            //Get l'orientation du joueur
            //On switch sur l'arme et appelle la méthode spécifique dédiée à l'arme player.weapon.attack()
            //if weapon is Knife 

            player.Weapon.Attack(map, player);
        
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