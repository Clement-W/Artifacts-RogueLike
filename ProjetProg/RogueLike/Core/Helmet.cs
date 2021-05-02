using RLNET;

namespace RogueLike.Core
{
    public class Helmet : DefenseEquipment
    {
        public Helmet(string name, int defBonus, int cost, RLColor color) : base(name, defBonus, cost)
        {
            Symbol = Icons.helmetSymbol;
            PrintedColor = color;
        }

        public Helmet(string name, int defBonus) : base(name, defBonus, 0)
        {
        }

        public static Helmet None()
        {
            return new Helmet("None", 0);
        }

        public static Helmet Polymer()
        {
            return new Helmet("Polymer Helmet", 1, 30, Colors.polymerColor);
        }

        public static Helmet Carbon()
        {
            return new Helmet("Carbon Helmet", 2, 50, Colors.carbonColor);
        }

        public static Helmet Platinum()
        {
            return new Helmet("Platinum Helmet", 3, 70, Colors.platinumColor);
        }

        public static Helmet Titanium()
        {
            return new Helmet("Titanium Helmet", 4, 90, Colors.titaniumColor);
        }
    }
}