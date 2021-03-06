namespace RogueLike.Core.Items
{

    /// <summary>
    /// This class represents a ration : a healing item that can heal the player
    /// </summary>
    public class Ration : HealingItem
    {

        /// <summary>
        /// This constructor creates a ration, with a healing value of 50
        /// </summary>
        /// <returns></returns>
        public Ration() : base()
        {
            Name = "Ration";
            Cost = 40;
            Symbol = Symbols.rationSymbol;
            HealingValue = 50;
        }
    }
}