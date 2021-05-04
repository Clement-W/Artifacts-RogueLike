namespace RogueLike.Core.Items
{
    /// <summary>
    /// This class represents a medicinal herb : a healing item that can heal the player
    /// </summary>
    public class MedicinalHerb : HealingItem
    {
        /// <summary>
        /// This constructor creates a medicinal herb, with a healing value of 30
        /// </summary>
        public MedicinalHerb() : base()
        {
            Name = "Medicinal Herb";
            Cost = 15;
            Symbol = Symbols.medHerbSymbol;
            HealingValue = 30;
        }
    }
}