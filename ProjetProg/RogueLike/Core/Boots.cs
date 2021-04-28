namespace RogueLike.Core
{
    public class Boots : DefenseEquipment
    {
        public Boots(string name, int defBonus)
        {
            Name = name;
            DefenseBonus = defBonus;
        }

        public static Boots None()
        {
            return new Boots("None", 0);
        }

        public static Boots Polymer()
        {
            return new Boots("Polymer Boots", 1);
        }

        public static Boots Carbon()
        {
            return new Boots("Carbon Boots", 2);
        }

        public static Boots Platinum()
        {
            return new Boots("Platinum Boots", 3);
        }

        public static Boots Titanium()
        {
            return new Boots("Titanium Boots", 4); 
        }
    }
}