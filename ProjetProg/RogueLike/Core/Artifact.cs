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

        public PlanetName ComesFrom { get; set; } // The planet from which the artifact comes from

        public Artifact(PlanetName comesFrom, int posX, int posY)
        {
            ComesFrom = comesFrom;
            PosX = posX;
            PosY = posY;
            Name = comesFrom.ToString() + "'s Artifact";
            Symbol = '%';
            PrintedColor = RLColor.White; //TODO: changer ça
        }

        public void Draw(RLConsole console, CurrentMap map)
        {
            // If it has never been explored, don't draw it
            if (map.GetCell(PosX, PosY).IsExplored)
            {

                // draw it differently if it's the fov or not
                if (map.IsInFov(PosX, PosY))
                {
                    // Draw it with the floor fov background color
                    console.Set(PosX, PosY, PrintedColor, map.Location.FloorBackgroundColorInFov, Symbol);
                }
                else
                {
                    // Draw it with the floor background and a '.' symbol
                    console.Set(PosX, PosY, PrintedColor, map.Location.FloorBackgroundColor, map.Location.FloorSymbol);
                }
            }
        }

      
    }
}