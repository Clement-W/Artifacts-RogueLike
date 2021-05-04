using System.Collections.Generic;
using RogueLike.Interfaces;
using RLNET;
using RogueSharp;
using System.Linq;

namespace RogueLike.Core.Merchants
{

    /// <summary>
    /// This class represents a merchant. It's an animated character.
    /// </summary>
    public abstract class Merchant : Character, IAnimated
    {

        /// <value>
        /// The alternate symbol number 1 used to create the animation of the character
        /// </value>
        public char AlternateSymbol1 { get; set; }

        /// <value>
        /// The alternate symbol number 2 used to create the animation of the character
        /// </value>
        public char AlternateSymbol2 { get; set; }

        /// <value>
        /// The level of the merchant. Higher is the merchant's level, better are the items
        /// This is equal to the number of artifacts found by the player
        /// </value>
        public int MerchantLevel { get; set; }


        /// <value>
        /// The sellable items proposed by the merchant/
        /// The value of the dictionnary is the sellable, and the key is it's position on the stall
        /// We don't use a list because the index of the element changes according to the list size
        /// </value>
        public Dictionary<int, ISellable> Stall { get; set; }

        /// <summary>
        /// This is the constructor of the merchant
        /// </summary>
        /// <param name="merchantLevel">The level of the merchant</param>
        public Merchant(int merchantLevel)
        {
            Stall = new Dictionary<int, ISellable>();
            MerchantLevel = merchantLevel;
            // Generate the stall
            GenerateStall();
        }

        /// <summary>
        /// This method is called periodically to animate the merchants
        /// </summary>
        public void ChangeSymbolAlternative()
        {
            Symbol = (Symbol == AlternateSymbol1) ? AlternateSymbol2 : AlternateSymbol1;
        }

        /// <summary>
        /// This method generates the merchant stall. It's specific to the merchant type, that's wy this
        /// method is abstract.
        /// </summary>
        public abstract void GenerateStall();

        /// <summary>
        /// This method draws the merchant's stall on the map
        /// The items are drawn below it
        /// </summary>
        /// <param name="console">The map console</param>
        /// <param name="map">The map</param>
        public void DrawStall(RLConsole console, CurrentMap map)
        {

            // Foreach items in the stall
            foreach (KeyValuePair<int, ISellable> sellable in Stall)
            {
                // If the item position is 0, the item will be displayed on bottom left of the merchant (posX+(0-1))
                // At index 1, the item is displayed just below the merchant (posX+(1-1))
                // At index 2, the item is displayed on bottom right of the merchant (posX+(2-1))

                ISellable sellableLoot = sellable.Value;
                int lootPosition = sellable.Key;
                sellableLoot.PosX = PosX + (lootPosition - 1);
                sellableLoot.PosY = PosY + 1;
            }

        }

        /// <summary>
        /// This method allows a merchant to sell items
        /// </summary>
        /// <param name="sellable"></param>
        public void SellItem(ISellable sellable)
        {
            foreach (KeyValuePair<int, ISellable> sellableInStall in Stall)
            {
                // Find the sellable in the stall
                if (sellableInStall.Value.PosX == sellable.PosX && sellableInStall.Value.PosY == sellable.PosY)
                {
                    //Remove the item from the stall and set the reference of the merchant to null
                    Stall.Remove(sellableInStall.Key);
                    sellable.SoldByMerchant = null;
                    break;
                }
            }



        }








    }
}