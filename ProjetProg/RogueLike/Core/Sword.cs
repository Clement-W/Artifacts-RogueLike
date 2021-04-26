namespace RogueLike.Core
{
    public class Sword : AttackEquipment
    {
        public Sword(string name, int attBonus){
            Name = name;
            AttackBonus = attBonus;
            DepthRange = 1;
            WideRange = 2;
        }

        public static Sword Wood(){
            return new Sword("Wood Sword",1);
        }

        public static Sword Copper(){
            return new Sword("Copper Sword",3);
        }

        public static Sword Iron(){
            return new Sword("Iron Sword",4);
        }

        public static Sword Titanium(){
            return new Sword("Titanium Sword",6);
        }

    }
}