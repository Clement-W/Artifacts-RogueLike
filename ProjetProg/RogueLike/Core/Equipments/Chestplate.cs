using RLNET;

namespace RogueLike.Core.Equipments
{
    public class Chestplate : DefenseEquipment
    {
        //factory
        private Chestplate(string name, int defBonus, int cost, RLColor color) : base(name, defBonus,cost)
        {
            Symbol = Icons.chestplateSymbol;
            PrintedColor = color;
        }

        private Chestplate(string name,int defBonus) : base(name,defBonus,0) {}

        public static Chestplate None()
        {
            return new Chestplate("None", 0);
        }

        public static Chestplate CreatePolymerChestplate()
        {
            return new Chestplate("Polymer Chestplate", 1,30, Colors.polymerColor);
        }

        public static Chestplate CreateCarbonChestplate()
        {
            return new Chestplate("Carbon Chestplate", 2,50, Colors.carbonColor);
        }

        public static Chestplate CreatePlatinumChestplate()
        {
            return new Chestplate("Platinum Chestplate", 3,70, Colors.platinumColor);
        }

        public static Chestplate CreateTitaniumChestplate()
        {
            return new Chestplate("Titanium Chestplate", 4,90, Colors.titaniumColor);
        }
    }
}