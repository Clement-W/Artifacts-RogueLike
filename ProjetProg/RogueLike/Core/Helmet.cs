namespace RogueLike.Core
{
    public class Helmet : DefenseEquipment
    {
        public Helmet(string name, int defBonus, int cost) : this(name, defBonus)
        {
            Cost = cost;
        }
        public Helmet(string name, int defBonus) : base(name, defBonus) { }

        public static Helmet None(){
            return new Helmet("None",0);  
        }

        public static Helmet Polymer(){
            return new Helmet("Polymer Helmet", 1,30);
        }

        public static Helmet Carbon(){
            return new Helmet("Carbon Helmet",2,50);
        }

        public static Helmet Platinum(){
            return new Helmet("Platinum Helmet",3,70);
        }

        public static Helmet Titanium(){
            return new Helmet("Titanium Helmet",4,90);
        }
    }
}