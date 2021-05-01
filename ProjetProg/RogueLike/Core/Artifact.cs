using RogueLike.Interfaces;
using RLNET;
using RogueSharp;
namespace RogueLike.Core
{
    public class Artifact : IDrawable, ILoot
    {
        public string Name { get; set; }

        public RLColor PrintedColor { get; set; }

        public char Symbol { get; set; } // The displayed symbol on the console

        public int PosX { get; set; }

        public int PosY { get; set; }

        public MapType ComesFrom{get;set;} // The map from which the artifact comes from

        public void Draw(RLConsole console, IMap map)
        {
            // If it has never been explored, don't draw it
            if (map.GetCell(PosX, PosY).IsExplored)
            {

                // draw it differently if it's the fov or not
                if (map.IsInFov(PosX, PosY))
                {
                    // Draw it with the floor fov background color
                    console.Set(PosX, PosY, PrintedColor, Colors.FloorBackgroundFov, Symbol);
                }
                else
                {
                    // Draw it with the floor background and a '.' symbol
                    console.Set(PosX, PosY, PrintedColor, Colors.FloorBackground, '.');
                }
            }
        }
    }
}