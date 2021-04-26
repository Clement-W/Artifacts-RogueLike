namespace RogueLike.Core
{
    public class Spear : AttackEquipment
    {
        public Spear(string name, int attBonus)
        {
            Name = name;
            AttackBonus = attBonus;
            DepthRange = 2;
            WideRange = 1;
        }

        public static Spear Wood()
        {
            return new Spear("Wood Spear", 1);
        }

        public static Spear Copper()
        {
            return new Spear("Copper Spear", 2);
        }

        public static Spear Iron()
        {
            return new Spear("Iron Spear", 3);
        }

        public static Spear Titanium()
        {
            return new Spear("Titanium Spear", 5);
        }

    }
}