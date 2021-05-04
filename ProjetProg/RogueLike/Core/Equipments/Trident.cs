namespace RogueLike.Core.Equipments
{
    /// <summary>
    /// This is the weapon of the boss from the Alleo planet : A Trident
    /// The pattern is a horizontal T, and is compatible with the base attack method.
    /// </summary>
    public class Trident : AttackEquipment
    {
        /// <summary>
        /// Create the trident
        /// </summary>
        public Trident(): base("Trident",8,1000)
        {
            AttackRange.Add(1,1);
            AttackRange.Add(2,1);
            AttackRange.Add(3,3);
            // Attack with a T form
            Symbol = Icons.tridentSymbol; 
        }
    }
}