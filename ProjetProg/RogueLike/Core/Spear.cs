using RLNET;

namespace RogueLike.Core
{
    public class Spear : AttackEquipment
    {

        private Spear(string name, int attBonus, int cost, RLColor color) : base(name,attBonus,cost)
        {
            AttackRange.Add(1,1);
            AttackRange.Add(2,1);
            Symbol = Icons.spearSymbol;
            PrintedColor = color;
        }

        public static Spear CreateSpearMk1()
        {
            return new Spear("Spear-Mk1", 1, 30, Colors.mk1Color);
        }

        public static Spear CreateSpearMk2()
        {
            return new Spear("Spear-Mk2", 2, 50, Colors.mk2Color);
        }

        public static Spear CreateSpearMk3()
        {
            return new Spear("Spear-Mk3", 3, 70, Colors.mk3Color);
        }

        public static Spear CreateSpearMk4()
        {
            return new Spear("Spear-Mk4", 5, 100, Colors.mk4Color);
        }


    }
}