namespace RogueLike.Core
{
    public abstract class AttackEquipment : Equipment
    {
        public int AttackBonus{get;set;}

        public int DepthRange{get;set;}

        public int WideRange{get;set;}
    }
}