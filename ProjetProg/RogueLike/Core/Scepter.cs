namespace RogueLike.Core
{
    public class Scepter : AttackEquipment
    {
        public Scepter() : base("Scepter",9,1000)
        {
            AttackRange.Add(1,1);
            AttackRange.Add(2,1);
            Symbol = Icons.scepterSymbol; // TODO: changer le symbol
        }
    }
}