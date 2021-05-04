using RLNET;
namespace RogueLike.Core
{
    public class PortalToSpaceship : TeleportationPortal
    {
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