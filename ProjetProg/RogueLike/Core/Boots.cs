namespace RogueLike.Core
{
    public class Boots : DefenseEquipment
    {
        public Boots(string name, int defBonus, int cost) : base(name, defBonus,cost)
        {
        }

        public Boots(string name,int defBonus) : base(name,defBonus,0){}

        public static Boots None()
        {
            return new Boots("None", 0);
        }

        public static Boots Polymer()
        {
            return new Boots("Polymer Boots", 1,30);
        }

        public static Boots Carbon()
        {
            return new Boots("Carbon Boots", 2,50);
        }

        public static Boots Platinum()
        {
            return new Boots("Platinum Boots", 3,70);
        }

        public static Boots Titanium()
        {
            return new Boots("Titanium Boots", 4,90);
        }
    }
}