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

        public static Leggins Leather(){
            return new Leggins("Leather Leggins", 1);
        }

        public static Leggins Iron(){
            return new Leggins("Iron Leggins",2);
        }

        public static Leggins Steel(){
            return new Leggins("Steel Leggins",3);
        }

        public static Leggins Obsidian(){
            return new Leggins("Obsidian Leggins",4);
        }
    }
}