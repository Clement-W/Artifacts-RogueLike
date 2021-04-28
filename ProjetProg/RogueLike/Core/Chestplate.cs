namespace RogueLike.Core
{
    public class Chestplate : DefenseEquipment
    {
        public Chestplate(string name, int defBonus)
        {
            Name = name;
            DefenseBonus = defBonus;
        }

        public static Chestplate None()
        {
            return new Chestplate("None", 0);
        }

        public static Chestplate Polymer()
        {
            return new Chestplate("Polymer Chestplate", 1);
        }

        public static Chestplate Carbon()
        {
            return new Chestplate("Carbon Chestplate", 2);
        }

        public static Chestplate Platinum()
        {
            return new Chestplate("Platinum Chestplate", 3);
        }

        public static Chestplate Titanium()
        {
            return new Chestplate("Titanium Chestplate", 4);
        }
    }
}