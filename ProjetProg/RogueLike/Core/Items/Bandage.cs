namespace RogueLike.Core.Items
{
    /// <summary>
    /// This class represents a bandage : a healing item that can heal the player
    /// </summary>
    public class Bandage : HealingItem
    {
        /// <summary>
        /// This constructor creates a bandage, with a healing value of 40
        /// </summary>
        public Bandage() : base(){
            Name = "Bandage";
            Cost = 20;
            Symbol = Symbols.bandageSymbol;
            HealingValue = 40;
        }

    }
}