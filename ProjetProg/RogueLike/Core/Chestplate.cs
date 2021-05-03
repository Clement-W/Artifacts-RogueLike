using RLNET;

namespace RogueLike.Core
{
    public class Chestplate : DefenseEquipment
    {
        public Chestplate(string name, int defBonus, int cost, RLColor color) : base(name, defBonus,cost)
        {
            Symbol = Icons.chestplateSymbol;
            PrintedColor = color;
        }

        public Chestplate(string name,int defBonus) : base(name,defBonus,0) {}

        public static Chestplate None()
        {
            return new Chestplate("None", 0);
        }

        public static Chestplate Polymer()
        {
            return new Chestplate("Polymer Chestplate", 1,30, Colors.polymerColor);
        }

        public static Chestplate Carbon()
        {
            return new Chestplate("Carbon Chestplate", 2,50, Colors.carbonColor);
        }

        public static Chestplate Platinum()
        {
            return new Chestplate("Platinum Chestplate", 3,70, Colors.platinumColor);
        }

        public static Chestplate Titanium()
        {
            return new Chestplate("Titanium Chestplate", 4,90, Colors.titaniumColor);
        }
    }
}