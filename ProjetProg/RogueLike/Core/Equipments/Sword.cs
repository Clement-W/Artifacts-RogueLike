using RLNET;

namespace RogueLike.Core.Equipments
{
    public class Sword : AttackEquipment
    {
        private Sword(string name, int attBonus, int cost, RLColor color) : base(name,attBonus,cost)
        {
            AttackRange.Add(1,3);
            Symbol = Icons.swordSymbol;
            PrintedColor = color;
        }

        

        // TODO : expliquer Mk dans le rapport
        public static Sword CreateSwordMk1(){
            return new Sword("Sword-Mk1",1,40, Colors.mk1Color);
        }

        public static Sword CreateSwordMk2(){
            return new Sword("Sword-Mk2",3,60, Colors.mk2Color);
        }

        public static Sword CreateSwordMk3(){
            return new Sword("Sword-Mk3",4,90, Colors.mk3Color);
        }

        public static Sword CreateSwordMk4(){
            return new Sword("Sword-Mk4",6,120, Colors.mk4Color);
        }


    }
}