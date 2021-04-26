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

        public static Helmet Leather(){
            return new Helmet("Leather Helmet", 1);
        }

        public static Helmet Iron(){
            return new Helmet("Iron Helmet",2);
        }

        public static Helmet Steel(){
            return new Helmet("Steel Helmet",3);
        }

        public static Helmet Obsidian(){
            return new Helmet("Obsidian Helmet",4);
        }
    }
}