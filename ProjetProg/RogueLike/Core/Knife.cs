using RLNET;

namespace RogueLike.Core
{
    public class Knife : AttackEquipment
    {
        public Knife(string name, int attBonus, int cost, RLColor color) : base(name,attBonus,cost)
        {
            AttackRange.Add(1,1);
            Symbol = Icons.knifeSymbol;
            PrintedColor = color;
        }

        public static Knife Mk1()
        {
            return new Knife("Knife-Mk1", 0,10, Colors.mk1Color);
        }

        public static Knife Mk2()
        {
            return new Knife("Knife-Mk2", 2,20, Colors.mk2Color);
        }

        public static Knife Mk3()
        {
            return new Knife("Knife-Mk3", 4,50, Colors.mk3Color);
        }

        public static Knife Mk4()
        {
            return new Knife("Knife-Mk4", 7,70, Colors.mk4Color);
        }




    }
}