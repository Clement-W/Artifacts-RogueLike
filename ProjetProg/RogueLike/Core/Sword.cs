using RLNET;

namespace RogueLike.Core
{
    public class Sword : AttackEquipment
    {
        public Sword(string name, int attBonus, int cost, RLColor color) : base(name,attBonus,cost)
        {
            AttackRange.Add(1,3);
            Symbol = Icons.swordSymbol;
            PrintedColor = color;
        }

        

        // TODO : expliquer Mk dans le rapport
        public static Sword Mk1(){
            return new Sword("Sword-Mk1",1,40, Colors.mk1Color);
        }

        public static Sword Mk2(){
            return new Sword("Sword-Mk2",3,60, Colors.mk2Color);
        }

        public static Sword Mk3(){
            return new Sword("Sword-Mk3",4,90, Colors.mk3Color);
        }

        public static Sword Mk4(){
            return new Sword("Sword-Mk4",6,120, Colors.mk4Color);
        }


    }
}