namespace RogueLike.Core.Equipments
{
    public abstract class DefenseEquipment : Equipment
    {
        public int DefenseBonus{get;set;}

        public DefenseEquipment(string name, int defBonus,int cost){
            Name= name;
            DefenseBonus = defBonus;
            Cost = cost;
        }

    }
}