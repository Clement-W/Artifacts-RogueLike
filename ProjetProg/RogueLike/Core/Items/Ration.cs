namespace RogueLike.Core.Items
{

    /// <summary>
    /// This class represent a ration : a healing item that can heal the player
    /// </summary>
    public class Ration : HealingItem
    {

        /// <summary>
        /// This constructor create a ration, with a healing value of 50
        /// </summary>
        /// <returns></returns>
        public Ration() : base()
        {
            Name = "Ration";
            Cost = 40;
            Symbol = Icons.rationSymbol;
            HealingValue = 50;
        }
    }
}