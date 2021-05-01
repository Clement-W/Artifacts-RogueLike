namespace RogueLike.Core
{
    public class Spear : AttackEquipment
    {

        public Spear(string name, int attBonus, int cost) : base(name,attBonus,cost)
        {
            RangeDepth = 2;
            RangeWidth = 1;
            Symbol = 'Ì';
        }

        public static Spear Mk1()
        {
            return new Spear("Spear-Mk1", 1, 30);
        }

        public static Spear Mk2()
        {
            return new Spear("Spear-Mk2", 2, 50);
        }

        public static Spear Mk3()
        {
            return new Spear("Spear-Mk3", 3, 70);
        }

        public static Spear Mk4()
        {
            return new Spear("Spear-Mk4", 5, 100);
        }


    }
}