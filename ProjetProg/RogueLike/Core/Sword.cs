namespace RogueLike.Core
{
    public class Sword : AttackEquipment
    {
        public Sword(string name, int attBonus){
            Name = name;
            AttackBonus = attBonus;
            RangeDepth = 1;
            RangeWidth = 3;
        }

        // TODO : expliquer Mk dans le rapport
        public static Sword Mk1(){
            return new Sword("Sword-Mk1",1);
        }

        public static Sword Mk2(){
            return new Sword("Sword-Mk2",3);
        }

        public static Sword Mk3(){
            return new Sword("Sword-Mk3",4);
        }

        public static Sword Mk4(){
            return new Sword("Sword-Mk4",6);
        }


    }
}