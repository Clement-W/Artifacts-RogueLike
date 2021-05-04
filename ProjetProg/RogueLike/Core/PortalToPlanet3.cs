using RLNET;
namespace RogueLike.Core
{

    /// <summary>
    /// This class represents the teleportation portal that goes to Thaad 
    /// </summary>
    public class PortalToPlanet3 : TeleportationPortal
    {
        /// <summary>
        /// Constructor of the teleportation portal that goes to Thaad 
        /// </summary>
        /// <param name="posX">The x position of the portal</param>
        /// <param name="posY">The y position of the portal</param>
        public PortalToPlanet3(int posX, int posY)
        {
            DestinationMap = MapType.Planet;
            PlanetDestination = PlanetName.Thaadd;
            AlternateSymbol1 = Symbols.planet3TpSymbol1;
            AlternateSymbol2 = Symbols.planet3TpSymbol2;
            Symbol = AlternateSymbol1;
            PosX = posX;
            PosY = posY;
        }
    }
}