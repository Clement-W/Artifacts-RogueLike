using RLNET;

namespace RogueLike.Core
{
    public class Leggins : DefenseEquipment
    {
        private Leggins(string name, int defBonus, int cost, RLColor color) : base(name, defBonus,cost)
        {
            Symbol = Icons.legginsSymbol;
            PrintedColor = color;
        }

        private Leggins(string name,int defBonus) : base(name,defBonus,0)
        {
        }

        public static Leggins None(){
            return new Leggins("None",0);  
        }

        public static Leggins CreatePolymerLeggins(){
            return new Leggins("Polymer Leggins", 1,30, Colors.polymerColor);
        }

        public static Leggins CreateCarbonLeggins(){
            return new Leggins("Carbon Leggins",2,50, Colors.carbonColor);
        }

        public static Leggins CreatePlatinumLeggins(){
            return new Leggins("Platinum Leggins",3,70, Colors.platinumColor);
        }

        public static Leggins CreateTitaniumLeggins(){
            return new Leggins("Titanium Leggins",4,90, Colors.titaniumColor);
        }
    }
}