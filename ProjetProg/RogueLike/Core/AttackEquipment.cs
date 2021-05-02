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


        public Dictionary<int, int> AttackRange { get; set; }
        // the key correponds to the depth range
        // the value corresponds to the width range (perpendicular to the attack direction) (odd number)
        // For example, if the dictionnary is : {<1,1>,<2,1>} 
        // the attack will impact the 2 cells in the attacker direction
        // if the dictionnary is {<1,1>,<2,1>,<3,3>}
        // the attack will looks like a T, starting in the attack direction : --|

        // We choose to use a dictionnary even if a list would have worked, to be clearer
        // than just using the list indexes as depth range.


        public AttackEquipment(string name, int attackBonus, int cost) : this()
        {
            Name = name;
            AttackBonus = attackBonus;
            Cost = cost;
            //PrintedColor = RLColor.LightRed;

        }
        public AttackEquipment()
        {
            AttackRange = new Dictionary<int, int>();
        }

        public virtual bool Attack(CurrentMap map, ActiveCharacter attacker, Player player)
        {

            // a wide attack is composed of the adjacent attacked cell and two halves of attack width on each side of that cell
            int sideSpace;  // this is the attacked space added on each side of the adjacent cell
            List<ICell> targetedCells = new List<ICell>(); // list of the sells targeted by the attack



            foreach (int depth in AttackRange.Keys)
            {// for each cell further away from the attacker, in the attack length

                sideSpace = (int)(AttackRange[depth] - 1) / 2;
                IEnumerable<ICell> targetedCellInOneDepth = null; // enumerable to collect the targeted perpendicular cell(s) in this part of the length
                if (attacker.Direction == Direction.Up) // ifs to check the direction of the attack and call a method that gets the cells perpendicular to the faced direction
                {
                    targetedCellInOneDepth = map.GetCellsAlongLine(attacker.PosX - sideSpace, attacker.PosY - depth, attacker.PosX + sideSpace, attacker.PosY - depth); // get cells from start and end of attacked width
                }
                else if (attacker.Direction == Direction.Down)
                {
                    targetedCellInOneDepth = map.GetCellsAlongLine(attacker.PosX - sideSpace, attacker.PosY + depth, attacker.PosX + sideSpace, attacker.PosY + depth);
                }
                else if (attacker.Direction == Direction.Left)
                {
                    targetedCellInOneDepth = map.GetCellsAlongLine(attacker.PosX - depth, attacker.PosY - sideSpace, attacker.PosX - depth, attacker.PosY + sideSpace);
                }
                else if (attacker.Direction == Direction.Right)
                {
                    targetedCellInOneDepth = map.GetCellsAlongLine(attacker.PosX + depth, attacker.PosY - sideSpace, attacker.PosX + depth, attacker.PosY + sideSpace);
                }
                targetedCells.AddRange(targetedCellInOneDepth); // add widths targeted at all parts of the length to the list of targeted cells
            }



            return AttackTargetedCells(map,attacker,player,targetedCells);
        }

        public bool AttackTargetedCells(CurrentMap map, ActiveCharacter attacker, Player player,IEnumerable<ICell> targetedCells)
        {
            bool isSomeoneHurt = false;

            foreach (ICell cell in targetedCells) // for each targeted cell, check for enemies to damage/kill
            {
                // Change the color of the attacked cells during a short period of time (100ms)
                Thread changeColorThread = new Thread(() => ChangeColorOfAttackedCells(cell, map));
                changeColorThread.Start();


                ActiveCharacter defender = null;

                if (attacker is Player)
                {

                    defender = map.GetEnemyAt(cell.X, cell.Y); // get the defenders if there's any
                }
                else if (player.PosX == cell.X && player.PosY == cell.Y)
                {
                    defender = player;
                }


                if (defender != null)
                {


                    isSomeoneHurt = true;
                    DealDamage(attacker, defender);

                    if (defender is Enemy && defender.Health <= 0)
                    {
                        KillEnemy(defender as Enemy, map);
                    }
                }
            }
            return isSomeoneHurt;


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
            map.AddLoot(new Gold(enemy.Gold, enemy.PosX, enemy.PosY));
            Game.MessageLog.AddMessage(enemy.Name + " is defeated");

            if (enemy is FinalBoss)
            {
                FinalBoss boss = enemy as FinalBoss;
                boss.ResolveBossDeath(map);
            }
        }


        public void DealDamage(ActiveCharacter attacker, ActiveCharacter defender) // Deal damage to an enemy with the players attack stat and enemy defense stat, and animate the damage
        {
            // The attackbonux is added to the base attack thanks to the Attack property
            int damageValue = attacker.Attack - defender.Defense;
            defender.Health -= (damageValue <= 0) ? 1 : damageValue; // deal at least one damage if damage is negative or nullified
            if (attacker is Player && damageValue <= 0)
            {
                Game.MessageLog.AddMessage("That wasn't very effective...");
            }
            Thread FlashThread = new Thread(new ThreadStart(defender.ChangeColorAfterHit));
            // Put the change color method in a thread to let the game continue during the color changement
            // Without a thread runing in background, the color changement is not visible
            FlashThread.Start();
        }
    }
}