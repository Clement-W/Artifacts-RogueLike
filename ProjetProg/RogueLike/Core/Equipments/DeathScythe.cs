using RogueSharp;
using System.Collections.Generic;
using System.Threading;
namespace RogueLike.Core.Equipments
{
    public class DeathScythe : AttackEquipment
    {
        public DeathScythe() : base("Death Scythe", 8, 1000)
        {
            AttackRange.Add(1, 3);
            // The attackRange is not used here because the Attack method is specific so overrided 
            Symbol = Icons.scytheSymbol; //TODO: changer symbol
        }

        public override bool Attack(CurrentMap map, ActiveCharacter attacker, Player player)
        {

            IEnumerable<ICell> targetedCells = map.GetBorderCellsInSquare(attacker.PosX, attacker.PosY, 1);

            foreach (ICell cell in targetedCells)
            {
                Thread changeColorThread = new Thread(() => ChangeColorOfAttackedCells(cell, map));
                changeColorThread.Start();
            }

            return AttackTargetedCells(map,attacker,player,targetedCells);

        }
    }
}