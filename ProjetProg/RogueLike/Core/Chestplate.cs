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

        public static Chestplate Leather()
        {
            return new Chestplate("Leather Chestplate", 1);
        }

        public static Chestplate Iron()
        {
            return new Chestplate("Iron Chestplate", 2);
        }

        public static Chestplate Steel()
        {
            return new Chestplate("Steel Chestplate", 3);
        }

        public static Chestplate Obsidian()
        {
            return new Chestplate("Obsidian Chestplate", 4);
        }
    }
}