using RLNET;

namespace RogueLike.Core
{
    //factory method
    public class Boots : DefenseEquipment
    {
        private Boots(string name, int defBonus, int cost, RLColor color) : base(name, defBonus,cost)
        {
            Symbol = Icons.bootsSymbol;
            PrintedColor = color;
        }

        private Boots(string name,int defBonus) : base(name,defBonus,0)
        {

        }

        public static Boots None()
        {
            return new Boots("None", 0);
        }

        public static Boots CreatePolymerBoots()
        {
            return new Boots("Polymer Boots", 1,30, Colors.polymerColor);
        }

        public static Boots CreateCarbonBoots()
        {
            return new Boots("Carbon Boots", 2,50, Colors.carbonColor);
        }

        public static Boots CreatePlatinumBoots()
        {
            return new Boots("Platinum Boots", 3,70, Colors.platinumColor);
        }

        public static Boots CreateTitaniumBoots()
        {
            return new Boots("Titanium Boots", 4,90, Colors.titaniumColor);
        }
    }
}