namespace RogueLike.Core
{
    public class Knife : AttackEquipment
    {
        public Knife(string name, int attBonus, int cost) : this(name, attBonus)
        {
            Name = name;
            AttackBonus = attBonus;
            DepthRange = 1;
            WideRange = 1;
        }
        public Knife(string name, int attBonus) : base(name, attBonus) { }

        public static Knife Mk1()
        {
            return new Knife("Knife-Mk1", 0,10);
        }

        public static Knife Mk2()
        {
            return new Knife("Knife-Mk2", 2,20);
        }

        public static Knife Mk3()
        {
            return new Knife("Knife-Mk3", 4,50);
        }

        public static Knife Mk4()
        {
            return new Knife("Knife-Mk4", 7,70);
        }

        public override void Attack(CurrentMap map)
        {
            throw new System.NotImplementedException();
        }



    }
}