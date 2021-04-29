using System.Collections.Generic;
namespace RogueLike.Core
{
    public abstract class AttackEquipment : Equipment
    {
        public int AttackBonus{get;set;}

        public int DepthRange{get;set;}

        public int WideRange{get;set;}

        public AttackEquipment(string name, int attackBonus){
            Name= name;
            AttackBonus = attackBonus;
        }
        public AttackEquipment(){}

        public abstract void Attack(CurrentMap map);

        public void KillEnemy(List<Enemy> enemies, CurrentMap map){
            foreach(Enemy enemy in enemies){
                map.RemoveEnemy(enemy);
            }
        }
    }
}