namespace RogueLike.Core
{
    public class Helmet : DefenseEquipment
    {
        public Helmet(string name, int defBonus){
            Name= name;
            DefenseBonus = defBonus;
        }

        public static Helmet None(){
            return new Helmet("None",0);  
        }

        public static Helmet Polymer(){
            return new Helmet("Polymer Helmet", 1);
        }

        public static Helmet Carbon(){
            return new Helmet("Carbon Helmet",2);
        }

        public static Helmet Platinum(){
            return new Helmet("Platinum Helmet",3);
        }

        public static Helmet Titanium(){
            return new Helmet("Titanium Helmet",4);
        }
    }
}