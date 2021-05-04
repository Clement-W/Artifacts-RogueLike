namespace RogueLike.Core.Equipments
{

    /// <summary>
    /// This is the weapon of the boss from the Damari planet : A Scepter
    /// The pattern is the same as the spear, so the attack method doesn't need to be
    /// overwritten
    /// </summary>
    public class Scepter : AttackEquipment
    {
        /// <summary>
        /// This constructor creates the scepter
        /// </summary>
        public Scepter() : base("Scepter", 9, 1000)
        {
            AttackRange.Add(1, 1);
            AttackRange.Add(2, 1);
            Symbol = Icons.scepterSymbol;
        }
    }
}