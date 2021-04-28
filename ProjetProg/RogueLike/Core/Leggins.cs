namespace RogueLike.Core
{
    public class Leggins : DefenseEquipment
    {
        public Leggins(string name, int defBonus){
            Name= name;
            DefenseBonus = defBonus;
        }

        public static Leggins None(){
            return new Leggins("None",0);  
        }

        public static Leggins Polymer(){
            return new Leggins("Polymer Leggins", 1);
        }

        public static Leggins Carbon(){
            return new Leggins("Carbon Leggins",2);
        }

        public static Leggins Platinum(){
            return new Leggins("Platinum Leggins",3);
        }

        public static Leggins Titanium(){
            return new Leggins("Titanium Leggins",4);
        }
    }
}