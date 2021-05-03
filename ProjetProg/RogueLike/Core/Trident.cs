namespace RogueLike.Core
{
    public class Trident : AttackEquipment
    {
        public Trident(): base("Trident",8,1000)
        {
            AttackRange.Add(1,1);
            AttackRange.Add(2,1);
            AttackRange.Add(3,3);
            // Attack with a T form
            Symbol = Icons.tridentSymbol; //TODO: changer le symbol
        }
    }
}