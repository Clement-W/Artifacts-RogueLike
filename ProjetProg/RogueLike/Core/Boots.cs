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

        public static Boots Leather()
        {
            return new Boots("Leather Boots", 1);
        }

        public static Boots Iron()
        {
            return new Boots("Iron Boots", 2);
        }

        public static Boots Steel()
        {
            return new Boots("Steel Boots", 3);
        }

        public static Boots Obsidian()
        {
            return new Boots("Obsidian Boots", 4);
        }
    }
}