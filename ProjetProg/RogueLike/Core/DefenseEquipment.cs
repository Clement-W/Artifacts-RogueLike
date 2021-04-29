namespace RogueLike.Core
{
    public abstract class DefenseEquipment : Equipment
    {
        public int DefenseBonus{get;set;}

        public DefenseEquipment(string name, int defBonus){
            Name= name;
            DefenseBonus = defBonus;
        }

    }
}