using RLNET;
namespace RogueLike.Core
{
    /// <summary>
    /// This class represent the teleportation portal that goes to the spaceship
    /// </summary>
    public class PortalToSpaceship : TeleportationPortal
    {
        /// <summary>
        /// Constructor of the teleportation portal that goes to the spaceship
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        public PortalToSpaceship(int posX, int posY){
            DestinationMap = MapType.Spaceship;
            AlternateSymbol1 = Symbols.shipTpSymbol1;
            AlternateSymbol2 = Symbols.shipTpSymbol2;
            Symbol = AlternateSymbol1;
            PosX = posX;
            PosY = posY;
        }
    }
}