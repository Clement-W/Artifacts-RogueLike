using RogueSharp;
using System.Collections.Generic;
using System.Threading;
namespace RogueLike.Core.Equipments
{
    /// <summary>
    /// This is the weapon of the boss from the Thaadd planet : A death Scythe
    /// </summary>
    public class DeathScythe : AttackEquipment
    {

        /// <summary>
        /// This constructor creates the death scythe.
        /// </summary>
        /// <returns></returns>
        public DeathScythe() : base("Death Scythe", 8, 1000)
        {
            AttackRange.Add(1, 3);
            // The attackRange is not used here because the Attack method is specific, so overrided 
            Symbol = Symbols.scytheSymbol; 
        }

        /// <summary>
        /// The attack method is specific, the death scythe attacks all around the active character that
        /// uses it. 
        /// </summary>
        /// <param name="map"> The map</param>
        /// <param name="attacker">The active character that attack</param>
        /// <param name="player">The player</param>
        /// <returns></returns>
        public override bool Attack(CurrentMap map, ActiveCharacter attacker, Player player)
        {
            // Get the cells around the attacker
            IEnumerable<ICell> targetedCells = map.GetBorderCellsInSquare(attacker.PosX, attacker.PosY, 1);

            // Animate each of the cells
            foreach (ICell cell in targetedCells)
            {
                Thread changeColorThread = new Thread(() => ChangeColorOfAttackedCells(cell, map));
                changeColorThread.Start();
            }
            // Attack the cells
            return AttackTargetedCells(map,attacker,player,targetedCells);

        }
    }
}