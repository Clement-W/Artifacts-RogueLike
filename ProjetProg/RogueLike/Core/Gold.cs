using RogueLike.Interfaces;
using RLNET;
using RogueSharp;

namespace RogueLike.Core
{
    /// <summary>
    /// This class represents the gold. This is not an item because it can't be used, or sold;
    /// It's lootable and drawable
    /// </summary>
    public class Gold : ILoot, IDrawable
    {

        /// <value>
        /// The x position of the gold
        /// </value>
        public int PosX { get; set; }

        /// <value>
        /// The y position of the gold
        /// </value>
        public int PosY { get; set; }

        /// <value>
        /// The symbol that represent the gold
        /// </value>
        public char Symbol { get; set; }

        /// <value>
        /// The printed color of the artifact
        /// </value>
        public RLColor PrintedColor { get; set; }


        /// <value>
        /// The gold amount
        /// </value>
        public int Amount { get; set; }

        /// <value>
        /// The name of the artifact
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// This is the constructor of the gold class
        /// </summary>
        /// <param name="amount"> The gold amount</param>
        /// <param name="posX"> The x position </param>
        /// <param name="posY"> The y position</param>
        public Gold(int amount, int posX, int posY)
        {
            Symbol = Symbols.goldSymbol;
            PrintedColor = Colors.Gold;
            Amount = amount;
            PosX = posX;
            PosY = posY;
            Name = "Gold";
        }



        /// <summary>
        /// Draw the gold on the map
        /// </summary>
        /// <param name="console">The map console</param>
        /// <param name="map">The map</param>
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