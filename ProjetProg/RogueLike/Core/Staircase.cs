using RogueLike.Interfaces;
using RLNET;
using RogueSharp;

namespace RogueLike.Core
{
    /// <summary>
    /// This class represent the staircase. It's a drawable element that allow the player
    /// to go deeper in the map
    /// </summary>
    public class Staircase : IDrawable
    {

        /// <value>
        /// The printed color of the staircase
        /// </value>
        public RLColor PrintedColor { get; set; }


        /// <value>
        /// The symbol that represent the symbol
        /// </value>
        public char Symbol { get; set; }

        /// <value>
        /// The x position of the artifact
        /// </value>
        public int PosX { get; set; }


        /// <value>
        /// The y position of the artifact
        /// </value>
        public int PosY { get; set; }

        /// <summary>
        /// This is the constructor of the staircase class
        /// </summary>
        /// <param name="posX">The x position of the staircase</param>
        /// <param name="posY">The y position of the staircase</param>
        public Staircase(int posX, int posY)
        {
            PosX = posX;
            PosY = posY;
            Symbol = '>';
        }

        /// <summary>
        /// This method draw the staircase on the map
        /// </summary>
        /// <param name="console">The map console</param>
        /// <param name="map">The map</param>
        public void Draw(RLConsole console, CurrentMap map)
        {
            if (map.GetCell(PosX, PosY).IsExplored)
            { // Draw it only if the cell has been explored by the player

                PrintedColor = map.IsInFov(PosX, PosY) ? Colors.StairsFov : Colors.Stairs;

                console.Set(PosX, PosY, PrintedColor, null, Symbol);
            }
        }

    }
}