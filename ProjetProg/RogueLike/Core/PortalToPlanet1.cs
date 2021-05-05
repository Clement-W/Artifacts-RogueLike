namespace RogueLike.Core
{
    /// <summary>
    /// This class represents the teleportation portal that goes to Alleo 
    /// </summary>
    public class PortalToPlanet1 : TeleportationPortal
    {

        /// <summary>
        /// Constructor of the teleportation portal that goes to Alleo 
        /// </summary>
        /// <param name="posX">The x position of the portal</param>
        /// <param name="posY">The y position of the portal</param>
        public PortalToPlanet1(int posX, int posY)
        {
            DestinationMap = MapType.Planet;
            PlanetDestination = PlanetName.Alleo;
            AlternateSymbol1 = Symbols.planet1TpSymbol1;
            AlternateSymbol2 = Symbols.planet1TpSymbol2;
            Symbol = AlternateSymbol1;
            PosX = posX;
            PosY = posY;
        }
    }
}