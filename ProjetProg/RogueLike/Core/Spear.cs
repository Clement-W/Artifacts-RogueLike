namespace RogueLike.Core
{
    public class Spear : AttackEquipment
    {
        public Spear(string name, int attBonus)
        {
            Name = name;
            AttackBonus = attBonus;
            RangeDepth = 2;
            RangeWidth = 1;
        }

        public static Spear Mk1()
        {
            return new Spear("Spear-Mk1", 1);
        }

        public static Spear Mk2()
        {
            return new Spear("Spear-Mk2", 2);
        }

        public static Spear Mk3()
        {
            return new Spear("Spear-Mk3", 3);
        }

        public static Spear Mk4()
        {
            return new Spear("Spear-Mk4", 5);
        }


    }
}