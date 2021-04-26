namespace RogueLike.Core
{
    public class Dagger : AttackEquipment
    {
        public Dagger(string name, int attBonus){
            Name = name;
            AttackBonus = attBonus;
            DepthRange = 1;
            WideRange = 1;
        }

        public static Dagger Wood(){
            return new Dagger("Wood Dagger",0);
        }

        public static Dagger Copper(){
            return new Dagger("Copper Dagger",2);
        }

        public static Dagger Iron(){
            return new Dagger("Iron Dagger",4);
        }

        public static Dagger Titanium(){
            return new Dagger("Titanium Dagger",7);
        }



    }
}