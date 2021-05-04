using RLNET;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using RogueLike.View;
using RogueLike.Core.Enemies;

namespace RogueLike.Core.Equipments
{
    /// <summary>
    /// This class represents the equipments designed to make damages
    /// </summary>
    public abstract class AttackEquipment : Equipment
    {

        /// <value>
        /// The attack bonus given to the player with this equipment 
        /// </value>
        public int AttackBonus { get; set; }



        /// <value>
        /// This dictionary describes the range of the attack.
        /// The key correponds to the depth range.
        /// The value corresponds to the width range (perpendicular to the attack direction) (odd number)
        /// For example, if the dictionnary is : {<\1,1>,<\2,1>} 
        /// the attack will impact the 2 cells in the attacker direction.
        /// If the dictionnary is {<\1,1>,<\2,1>,<\3,3>}
        /// the attack will looks like a T, starting in the attack direction : --|
        /// We choose to use a dictionnary even if a list would have worked, to be clearer
        /// than just using the list indexes as depth ranges.
        /// </value>
        public Dictionary<int, int> AttackRange { get; set; }


        /// <summary>
        /// This is a constructor of this class.
        /// </summary>
        /// <param name="name"> The name of the attack equipment</param>
        /// <param name="attackBonus"> The bonus of attack given by this equipment</param>
        /// <param name="cost">The cost of this equipment if it's on a merchant stall</param>
        public AttackEquipment(string name, int attackBonus, int cost) : this()
        {
            Name = name;
            AttackBonus = attackBonus;
            Cost = cost;
            //PrintedColor = RLColor.LightRed;

        }


        /// <summary>
        /// This constructor don't takes any argument, it creates the dictionnary that represent the attack range
        /// </summary>
        public AttackEquipment()
        {
            AttackRange = new Dictionary<int, int>();
        }

        /// <summary>
        /// This method allow an active character to attack with this weapon.
        /// The basic behavior of the weapons is described here but this method can be over overwritten.
        /// </summary>
        /// <param name="map">The map</param>
        /// <param name="attacker">The active character that attack</param>
        /// <param name="player">The player</param>
        /// <returns>True if the attack hits an active character</returns>
        public virtual bool Attack(CurrentMap map, ActiveCharacter attacker, Player player)
        {

            // A wide attack is composed of the adjacent attacked cell and two halves of attack width on each side of that cell
            // This is the attacked space added on each side of the adjacent cell
            int sideSpace;
            List<ICell> targetedCells = new List<ICell>(); // List of the cells targeted by the attack

            // For each cell further away from the attacker, in the attack length
            foreach (int depth in AttackRange.Keys)
            {
                // Set the side space according to the width range
                sideSpace = (int)(AttackRange[depth] - 1) / 2;
                // To collect the targeted perpendicular cell(s) in this part of the length
                IEnumerable<ICell> targetedCellInOneDepth = null;

                // Ifs to check the direction of the attack and call a method that gets the cells perpendicular to the faced direction
                if (attacker.Direction == Direction.Up)
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
                // Add widths targeted at all parts of the length to the list of targeted cells
                targetedCells.AddRange(targetedCellInOneDepth);
            }


            // Call the method that makes the attack on the targeted cells. 
            // This return true if the attack hits an active character
            return AttackTargetedCells(map, attacker, player, targetedCells);
        }

        /// <summary>
        /// This method makes the attack on the targeted cells
        /// </summary>
        /// <param name="map">The map</param>
        /// <param name="attacker">The active character that attack</param>
        /// <param name="player">The player</param>
        /// <param name="targetedCells">The cells that are targeted by the attack</param>
        /// <returns>True if the attack hits an active character</returns>
        public bool AttackTargetedCells(CurrentMap map, ActiveCharacter attacker, Player player, IEnumerable<ICell> targetedCells)
        {
            bool isSomeoneHurt = false;

            // For each targeted cell, check for enemies/player to damage/kill
            foreach (ICell cell in targetedCells)
            {
                // Change the color of the attacked cells during a short period of time (100ms)
                Thread changeColorThread = new Thread(() => ChangeColorOfAttackedCells(cell, map));
                changeColorThread.Start();

                ActiveCharacter defender = null;

                if (attacker is Player)
                {
                    // get the defenders if there's any
                    defender = map.GetEnemyAt(cell.X, cell.Y);
                }
                else if (player.PosX == cell.X && player.PosY == cell.Y)
                {
                    // else, if the player is targeted by the attack, set the defender to player
                    defender = player;
                }

                // If there's a defender
                if (defender != null)
                {

                    isSomeoneHurt = true;
                    // Deal the damages to the defender
                    DealDamage(attacker, defender);

                    if (defender is Enemy && defender.Health <= 0)
                    {
                        KillEnemy(defender as Enemy, map);
                    }
                }
            }
            return isSomeoneHurt;


        }



        /// <summary>
        /// Change the color of the attacked cell to show the attack range
        /// </summary>
        /// <param name="cell">The attacked cell</param>
        /// <param name="map"> The map</param>
        public void ChangeColorOfAttackedCells(ICell cell, CurrentMap map)
        {
            // Lock the list while w'ere modifying it to avoid multi-threading errors 
            lock (map.AttackedCells)
            {
                map.AttackedCells.Add(cell);
            }

            // Wait 100ms
            Thread.Sleep(100);
            // Lock the list while we're modfying it
            lock (map.AttackedCells)
            {
                map.AttackedCells.Remove(cell);
            }

        }

        /// <summary>
        /// Kill an enemy
        /// </summary>
        /// <param name="enemy">The enemy that is destroyed</param>
        /// <param name="map">The map</param>
        public void KillEnemy(Enemy enemy, CurrentMap map) 
        {
            // Remove the enemy from the map
            map.RemoveEnemy(enemy); 
            // Add the enemy's gold as a loot on the ground
            map.AddLoot(new Gold(enemy.Gold, enemy.PosX, enemy.PosY));
            // Tell the player that the enemy is defeated
            Game.MessageLog.AddMessage(enemy.Name + " is defeated");

            // If the enemy is a final boss, use the designed method to
            // drop the artifacts, it's weapon and show the teleporter to the spaceship
            if (enemy is FinalBoss)
            {
                FinalBoss boss = enemy as FinalBoss;
                boss.ResolveBossDeath(map);
            }
        }


        /// <summary>
        /// Deal damages between the attacker and te defender.
        /// This method also animate the damages taken by the defender with a red flash 
        /// </summary>
        /// <param name="attacker">The ActiveCharacter who attacks</param>
        /// <param name="defender">The ActiveCharacter who defends</param>
        public void DealDamage(ActiveCharacter attacker, ActiveCharacter defender) // 
        {
            // The attackbonux given by the weapon is automatically added to the base attack 
            // thanks to the ActiveCharacter's Attack property
            int damageValue = attacker.Attack - defender.Defense;
            // Deal at least one damage if damage is negative or nullified
            defender.Health -= (damageValue <= 0) ? 1 : damageValue; 
            if (attacker is Player && damageValue <= 0)
            {
                Game.MessageLog.AddMessage("That wasn't very effective...");
            }

            // Put the change color method in a thread to let the game continue during the color changement
            // Without a thread runing in background, the color changement is not visible
            Thread FlashThread = new Thread(new ThreadStart(defender.ChangeColorAfterHit));
            FlashThread.Start();
        }
    }
}