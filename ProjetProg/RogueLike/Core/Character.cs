using RogueLike.Interfaces;
using RLNET;
using RogueSharp;

namespace RogueLike.Core
{
    /// <summary>
    /// This class represent a character. 
    /// </summary>
    public abstract class Character : IDrawable
    {
        /// <value>
        /// The name of the character
        /// </value>
        public string Name { get; set; }


        /// <value>
        /// The printed color of the character
        /// </value>
        public RLColor PrintedColor { get; set; }

        /// <value>
        /// The symbol that represent the character
        /// </value>
        public char Symbol { get; set; } // The displayed symbol on the console


        /// <value>
        /// The x position of the character
        /// </value>
        public int PosX { get; set; }

        /// <value>
        /// The y position of the character
        /// </value>
        public int PosY { get; set; }



        /// <summary>
        /// This method draw the character on the map
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