namespace RogueLike.Core
{
    public class Spear : AttackEquipment
    {

        public Spear(string name, int attBonus, int cost) : this(name, attBonus)
        {
            Name = name;
            AttackBonus = attBonus;
            DepthRange = 2;
            WideRange = 1;
        }
        public Spear(string name, int attBonus) : base(name, attBonus) { }

        public static Spear Mk1()
        {
            return new Spear("Spear-Mk1", 1,30);
        }

        public static Spear Mk2()
        {
            return new Spear("Spear-Mk2", 2,50);
        }

        public static Spear Mk3()
        {
            return new Spear("Spear-Mk3", 3,70);
        }

        public static Spear Mk4()
        {
            return new Spear("Spear-Mk4", 5,100);
        }

        public override void Attack(CurrentMap map)
        {
            throw new System.NotImplementedException();
        }

    }
}