using RogueLike.Interfaces;
using RogueLike.Core.Merchants;

using RLNET;
namespace RogueLike.Core.Equipments
{
    /// <summary>
    /// This class represents an equipment. An equipment is a drawable, is lootable
    /// and sellable by a merchant.
    /// </summary>
    public abstract class Equipment : IDrawable, ILoot, ISellable
    {


        /// <value>
        /// The printed color of the equipment
        /// </value>
        public RLColor PrintedColor { get; set; }


        /// <value>
        /// The symbol that represents the equipment
        /// </value>
        public char Symbol { get; set; }


        /// <value>
        /// The x position of the equipment
        /// </value>
        public int PosX { get; set; }

        /// <value>
        /// The y position of the equipment
        /// </value>
        public int PosY { get; set; }


        /// <value> 
        /// A reference to the merchant that sells this equipment
        /// </value>
        public Merchant SoldByMerchant { get; set; }

        /// <value>
        /// The name of the equipment
        /// </value>
        public string Name { get; set; }


        /// <value>
        /// The cost of the equipment when it's on a merchant stall
        /// </value>
        public int Cost { get; set; }


        /// <summary>
        /// This constructor sets the standard print color and set the reference to the merchant to null.
        /// This constructor is implicitly called
        /// </summary>
        public Equipment()
        {
            PrintedColor = Palette.DbLight;
            SoldByMerchant = null;
        }


        /// <summary>
        /// This method draws the equipment when they're on the ground
        /// </summary>
        /// <param name="console"> The map cconsole</param>
        /// <param name="map"> The map that contains the equipments</param>
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


    }
}