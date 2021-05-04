using RLNET;
namespace RogueLike.Core
{
    /// <summary>
    /// This class represent the teleportation portal that goes to Damari 
    /// </summary>
    public class PortalToPlanet2 : TeleportationPortal
    {
        /// <summary>
        /// Constructor of the teleportation portal that goes to Damari 
        /// </summary>
        /// <param name="posX">The x position of the portal</param>
        /// <param name="posY">The y position of the portal</param>
        public PortalToPlanet2(int posX, int posY)
        {
            DestinationMap = MapType.Planet;
            PlanetDestination = PlanetName.Damari;
            AlternateSymbol1 = Symbols.planet2TpSymbol1;
            AlternateSymbol2 = Symbols.planet2TpSymbol2;
            Symbol = AlternateSymbol1;
            PosX = posX;
            PosY = posY;
        }
    }
}