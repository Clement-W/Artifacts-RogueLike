namespace RogueLike.Core.Items
{
    /// <summary>
    /// This class represent a old sandwich : a healing item that can heal the player
    /// </summary>
    public class OldSandwich : HealingItem
    {

        /// <summary>
        /// This constructor create an old sandwich, with a healing value of 20
        /// </summary>
        public OldSandwich() : base()
        {
            Name = "Old Sandwich";
            Cost = 10;
            Symbol = Icons.sandwichSymbol;
            HealingValue = 20;
        }
    }
}