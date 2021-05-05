namespace RogueLike.Core.Items
{
    /// <summary>
    /// This class represents an item that can heal the player
    /// A the moment there's only healing items.
    /// </summary>
    public abstract class HealingItem : Item
    {
        /// <summary>
        /// The healing value is the value added to the player's health if they use the item
        /// </summary>
        protected int HealingValue;

        /// <summary>
        /// This method allows the player to use the healing item.
        /// Here, it adds the healing value to the player's health
        /// </summary>
        /// <param name="player">The player which uses the item</param>
        public override void Use(Player player){
            player.Health+=HealingValue;
            if(player.Health>player.MaxHealth){
                player.Health = player.MaxHealth;   
            }
        }
    }
}