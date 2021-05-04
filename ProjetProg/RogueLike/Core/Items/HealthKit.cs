namespace RogueLike.Core.Items
{
    /// <summary>
    /// This class represents a health kit : a healing item that can heal the player
    /// </summary>
    public class HealthKit : HealingItem
    {

        /// <summary>
        /// This constructor creates a health kit, with a healing value of 50
        /// </summary>
        public HealthKit() : base()
        {
            Name = "Health kit";
            Cost = 50;
            Symbol = Icons.healthKitSymbol;
            HealingValue = 100;
        }

    }
}