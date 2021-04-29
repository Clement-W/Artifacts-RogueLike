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

        public AttackEquipment(string name, int attackBonus)
        {
            Name = name;
            AttackBonus = attackBonus;
        }
        public AttackEquipment() { }

        public void Attack(CurrentMap map, Player player)
        {
            int sideSpace = (int)(RangeWidth - 1) / 2; // space on each side of the adjacent cell of the attacked direction
            List<ICell> targetedCells = new List<ICell>();


            for (int i = 1; i <= RangeDepth; i++)
            {
                IEnumerable<ICell> targetedCellInOneDepth = null;
                if (player.Symbol == player.UpSymbol)
                {
                    targetedCellInOneDepth = map.GetCellsAlongLine(player.PosX - sideSpace, player.PosY - i, player.PosX + sideSpace, player.PosY - i);
                    //targetedCells.AddRange(map.GetCellsAlongLine(player.PosX - sideSpace, player.PosY - i, player.PosX + sideSpace, player.PosY - i));
                    Console.WriteLine("Up");
                }
                else if (player.Symbol == player.DownSymbol)
                {
                    targetedCellInOneDepth = map.GetCellsAlongLine(player.PosX - sideSpace, player.PosY + i, player.PosX + sideSpace, player.PosY + i);
                }
                else if (player.Symbol == player.LeftSymbol)
                {
                    targetedCellInOneDepth = map.GetCellsAlongLine(player.PosX - i, player.PosY - sideSpace, player.PosX - i, player.PosY + sideSpace);
                    Console.WriteLine("Left");
                }
                else if (player.Symbol == player.RightSymbol)
                {
                    targetedCellInOneDepth = map.GetCellsAlongLine(player.PosX + i, player.PosY - sideSpace, player.PosX + i, player.PosY + sideSpace);
                    Console.WriteLine("Right");
                }
                targetedCells.AddRange(targetedCellInOneDepth);
            }

            foreach (ICell cell in targetedCells)
            {
                Console.WriteLine("c :" + cell.X + ", " + cell.Y);
                Enemy enemy = map.GetEnemyAt(cell.X, cell.Y);
                //ScreenView.RootConsole.SetBackColor(cell.X, cell.Y, RLColor.Yellow);
                GameScreen.ChangeBackColor(cell);
                if (enemy != null)
                {
                    Console.WriteLine("BAM");
                    DealDamage(player, enemy);
                    Console.WriteLine(enemy.Health);
                    if (enemy.Health <= 0)
                    {
                        KillEnemy(enemy, map);
                    }
                }
            }
        }


        public void KillEnemy(Enemy enemy, CurrentMap map)
        {
            map.RemoveEnemy(enemy);
            Game.MessageLog.AddMessage(enemy.Name + " is defeated");
        }


        public void DealDamage(Player player, Enemy enemy)
        {
            int damageValue = player.Attack + AttackBonus - enemy.Defense;
            enemy.Health -= (damageValue <= 0) ? 1 : damageValue;
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