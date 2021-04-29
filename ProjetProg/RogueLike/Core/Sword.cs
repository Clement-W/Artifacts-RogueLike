namespace RogueLike.Core
{
    public class Sword : AttackEquipment
    {
        public Sword(string name, int attBonus, int cost) : this(name, attBonus)
        {
            Name = name;
            AttackBonus = attBonus;
            DepthRange = 1;
            WideRange = 3;
        }
        public Sword(string name, int attBonus) : base(name, attBonus) { }

        

        // TODO : expliquer Mk dans le rapport
        public static Sword Mk1(){
            return new Sword("Sword-Mk1",1,40);
        }

        public static Sword Mk2(){
            return new Sword("Sword-Mk2",3,60);
        }

        public static Sword Mk3(){
            return new Sword("Sword-Mk3",4,90);
        }

        public static Sword Mk4(){
            return new Sword("Sword-Mk4",6,120);
        }

        public override void Attack(CurrentMap map)
        {
            throw new System.NotImplementedException();
        }

    }
}