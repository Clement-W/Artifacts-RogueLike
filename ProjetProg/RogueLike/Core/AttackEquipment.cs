using RLNET;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using RogueLike.View;

namespace RogueLike.Core
{
    public abstract class AttackEquipment : Equipment
    {
        public int AttackBonus { get; set; }

        public int RangeDepth { get; set; }

        public int RangeWidth { get; set; } // must be an odd number

        public AttackEquipment(string name, int attackBonus,int cost)
        {
            Name = name;
            AttackBonus = attackBonus;
            Cost = cost;
            PrintedColor = RLColor.LightRed;
        }
        public AttackEquipment() { }

        public void Attack(CurrentMap map, Player player)
        {
            // a wide attack is composed of the adjacent attacked cell and two halves of attack width on each side of that cell
            int sideSpace = (int)(RangeWidth - 1) / 2; // this is the attacked space added on each side of the adjacent cell
            List<ICell> targetedCells = new List<ICell>(); // list of the sells targeted by the attack


            for (int i = 1; i <= RangeDepth; i++) // for each cell further away from the player, in the attack length
            {
                IEnumerable<ICell> targetedCellInOneDepth = null; // enumerable to collect the targeted perpendicular cell(s) in this part of the length
                if (player.Symbol == player.UpSymbol) // ifs to check the direction of the attack and call a method that gets the cells perpendicular to the faced direction
                {
                    targetedCellInOneDepth = map.GetCellsAlongLine(player.PosX - sideSpace, player.PosY - i, player.PosX + sideSpace, player.PosY - i); // get cells from start and end of attacked width
                }
                else if (player.Symbol == player.DownSymbol)
                {
                    targetedCellInOneDepth = map.GetCellsAlongLine(player.PosX - sideSpace, player.PosY + i, player.PosX + sideSpace, player.PosY + i);
                }
                else if (player.Symbol == player.LeftSymbol)
                {
                    targetedCellInOneDepth = map.GetCellsAlongLine(player.PosX - i, player.PosY - sideSpace, player.PosX - i, player.PosY + sideSpace);
                }
                else if (player.Symbol == player.RightSymbol)
                {
                    targetedCellInOneDepth = map.GetCellsAlongLine(player.PosX + i, player.PosY - sideSpace, player.PosX + i, player.PosY + sideSpace);
                }
                targetedCells.AddRange(targetedCellInOneDepth); // add widths targeted at all parts of the length to the list of targeted cells
            }

            foreach (ICell cell in targetedCells) // for each targeted cell, check for enemies to damage/kill
            {
            
                Enemy enemy = map.GetEnemyAt(cell.X, cell.Y);


                Thread changeColorThread = new Thread(() => ChangeColorOfAttackedCells(cell, map));
                changeColorThread.Start(); // Change the color of the attacked cells during a short period of time (100ms)



                if (enemy != null)
                {
                    DealDamage(player, enemy);
              
                    if (enemy.Health <= 0)
                    {
                        KillEnemy(enemy, map);
                    }
                }
            }
        }

        public void ChangeColorOfAttackedCells(ICell cell, CurrentMap map)
        {
            lock (map.AttackedCells) // Lock the list while w'ere modifying it to avoid multi-threading errors 
            {
                map.AttackedCells.Add(cell); 
            }

            Thread.Sleep(100); // Wait 100ms
            lock (map.AttackedCells) // Lock the list while we're modfying it
            {
                map.AttackedCells.Remove(cell);
            }

        }


        public void KillEnemy(Enemy enemy, CurrentMap map) // Destroy an enemy
        {
            map.RemoveEnemy(enemy);
            map.AddLoot(new Gold(enemy.Gold,enemy.PosX,enemy.PosY));
            Game.MessageLog.AddMessage(enemy.Name + " is defeated");
        }


        public void DealDamage(Player player, Enemy enemy) // Deal damage to an enemy with the players attack stat and enemy defense stat, and animate the damage
        {
            int damageValue = player.Attack + AttackBonus - enemy.Defense;
            enemy.Health -= (damageValue <= 0) ? 1 : damageValue; // deal at least one damage if damage is negative or nullified
            if (damageValue <= 0)
            {
                Game.MessageLog.AddMessage("That wasn't very effective...");
            }
            Thread FlashThread = new Thread(new ThreadStart(enemy.ChangeColorAfterHit));
            // Put the change color method in a thread to let the game continue during the color changement
            // Without a thread runing in background, the color changement is not visible
            FlashThread.Start();
        }
    }
}