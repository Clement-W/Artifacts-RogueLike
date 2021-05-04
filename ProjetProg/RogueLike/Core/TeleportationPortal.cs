using RogueLike.Interfaces;
using RLNET;
using RogueSharp;
namespace RogueLike.Core
{

    /// <summary>
    /// This class represent a teleportation portable. It's drawable and animated
    /// </summary>
    public abstract class TeleportationPortal : IDrawable, IAnimated
    {

        /// <value>
        /// The alternate symbol number 1 used to create the animation of the portal
        /// </value>
        public char AlternateSymbol1 { get; set; }

        /// <value>
        /// The alternate symbol number 2 used to create the animation of the portal
        /// </value>
        public char AlternateSymbol2 { get; set; }

        /// <value>
        /// The map type of the destination
        /// </value>
        public MapType DestinationMap { get; set; }

        /// <value>
        /// The planet type of the destination
        /// </value>
        public PlanetName PlanetDestination { get; set; }

        /// <value>
        /// The printed color of the portal
        /// </value>
        public RLColor PrintedColor { get; set; }


        /// <value>
        /// The symbol that represent the portal
        /// </value>
        public char Symbol { get; set; }

        /// <value>
        /// The x position of the portal
        /// </value>
        public int PosX { get; set; }

        /// <value>
        /// The y position of the portal
        /// </value>
        public int PosY { get; set; }

        /// <summary>
        /// Default constructor of the teleportation portals. It only sets the printed color.
        /// </summary>
        public TeleportationPortal()
        {
            PrintedColor = Colors.TeleportationPortal;
        }

        /// <summary>
        /// This method draw the teleportation portal on the map
        /// </summary>
        /// <param name="console"> The map console</param>
        /// <param name="map"> The map</param>
        public void Draw(RLConsole console, CurrentMap map)
        {
            console.Set(PosX, PosY, PrintedColor, null, Symbol);
        }

        /// <summary>
        /// This method is called periodically to animate the portal
        /// </summary>
        public void ChangeSymbolAlternative()
        {
            Symbol = (Symbol == AlternateSymbol1) ? AlternateSymbol2 : AlternateSymbol1;
        }
    }
}