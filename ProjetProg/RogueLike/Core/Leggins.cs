namespace RogueLike.Core
{
    public class Leggins : DefenseEquipment
    {
        public Leggins(string name, int defBonus, int cost) : base(name, defBonus,cost)
        {
        }

        public Leggins(string name,int defBonus) : base(name,defBonus,0){}

        public static Leggins None(){
            return new Leggins("None",0);  
        }

        public static Leggins Polymer(){
            return new Leggins("Polymer Leggins", 1,30);
        }

        public static Leggins Carbon(){
            return new Leggins("Carbon Leggins",2,50);
        }

        public static Leggins Platinum(){
            return new Leggins("Platinum Leggins",3,70);
        }

        public static Leggins Titanium(){
            return new Leggins("Titanium Leggins",4,90);
        }
    }
}