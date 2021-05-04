using RogueLike.Interfaces;
using RLNET;
using RogueSharp;
using RogueLike.Core.Merchants;

namespace RogueLike.Core.Items
{
    /// <summary>
    /// This class represents an item that is drawable, lootable and sellable by a merchant
    /// </summary>
    public abstract class Item : IDrawable, ILoot, ISellable
    {
        /// <value>
        /// The printed color of the item
        /// </value>
        public RLColor PrintedColor { get; set; }

        /// <value>
        /// The symbol that represents the item
        /// 
        /// </value>
        public char Symbol { get; set; }

        /// <value>
        /// The x position of the item
        /// </value>
        public int PosX { get; set; }

        /// <value>
        /// The y position of the item
        /// </value>
        public int PosY { get; set; }

        /// <value>
        /// The name of the item
        /// </value>
        public string Name { get; set; }

        /// <value> 
        /// A reference to the merchant that sells this item
        /// </value>
        public Merchant SoldByMerchant { get; set; }


        /// <value>
        /// The cost of the item when it's on a merchant stall
        /// </value>
        public int Cost { get; set; }


        /// <summary>
        /// This constructor sets the standard print color and sets the reference to the merchant to null.
        /// This constructor is implicitly called
        /// </summary>
        public Item()
        {
            PrintedColor = RLColor.White;
            SoldByMerchant = null;
        }

        /// <summary>
        /// This method draws the items when they're on the ground
        /// </summary>
        /// <param name="console"> The map console</param>
        /// <param name="map"> The map that contains the item</param>
        public void Draw(RLConsole console, CurrentMap map)
        {
            // If it has never been explored, don't draw it
            if (map.GetCell(PosX, PosY).IsExplored)
            {

                // draw it differently if it's the fov or not
                if (map.IsInFov(PosX, PosY))
                {
                    // Draw it with the floor fov background color of the map
                    console.Set(PosX, PosY, PrintedColor, map.Location.FloorBackgroundColorInFov, Symbol);
                }
                else
                {
                    // Draw it with the floor background and a the floor symbol of the map
                    console.Set(PosX, PosY, PrintedColor, map.Location.FloorBackgroundColor, map.Location.FloorSymbol);
                }
            }
        }

        /// <summary>
        /// This method allows the player to use the item.
        /// Every item needs to overwrite this method
        /// </summary>
        /// <param name="player">The player that uses the item</param>
        public abstract void Use(Player player);






    }
}