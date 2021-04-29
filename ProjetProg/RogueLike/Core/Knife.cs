namespace RogueLike.Core
{
    public class Knife : AttackEquipment
    {
        public Knife(string name, int attBonus){
            Name = name;
            AttackBonus = attBonus;
            RangeDepth = 1;
            RangeWidth = 1;
        }

        public static Knife Mk1(){
            return new Knife("Knife-Mk1",0);
        }

        public static Knife Mk2(){
            return new Knife("Knife-Mk2",2);
        }

        public static Knife Mk3(){
            return new Knife("Knife-Mk3",4);
        }

        public static Knife Mk4(){
            return new Knife("Knife-Mk4",7);
        }




    }
}