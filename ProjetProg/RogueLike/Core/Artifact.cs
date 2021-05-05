using RogueLike.Interfaces;
using RLNET;
using RogueSharp;
namespace RogueLike.Core
{

    /// <summary>
    /// This class represents an artifact. It's not an item because it's not usable. 
    /// But it's drawable and lootable
    /// </summary>
    public class Artifact : IDrawable, ILoot
    {

        /// <value>
        /// The name of the artifact
        /// </value>
        public string Name { get; set; }

        /// <value>
        /// The printed color of the artifact
        /// </value>
        public RLColor PrintedColor { get; set; }

        /// <value>
        /// The symbol that represents the artifact
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


        /// <value>
        /// The planet from where the artifacts comes from
        /// </value>
        public PlanetName ComesFrom { get; set; }

        /// <summary>
        /// This is the constructor of the artifact class
        /// </summary>
        /// <param name="comesFrom">The planet from where the artifact comes from</param>
        /// <param name="posX">The x position of the artifact</param>
        /// <param name="posY">The y position of the artifact</param>
        public Artifact(PlanetName comesFrom, int posX, int posY)
        {
            ComesFrom = comesFrom;
            PosX = posX;
            PosY = posY;
            Name = comesFrom.ToString() + "'s Artifact";
            SetArtifactSymbol();
            PrintedColor = Colors.BasicColor;
        }

        /// <summary>
        /// This method draws the artifact on the map
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

        /// <summary>
        /// This method sets the artifact symbol according to the planet from where it comes from
        /// </summary>
        private void SetArtifactSymbol()
        {
            switch (ComesFrom)
            {
                case PlanetName.Alleo: Symbol = Symbols.artifact1Symbol; break;
                case PlanetName.Damari: Symbol = Symbols.artifact2Symbol; break;
                case PlanetName.Thaadd: Symbol = Symbols.artifact3Symbol; break;
            }
        }


    }
}