using RLNET;
namespace RogueLike.Core
{
    public class PortalToPlanet1 : TeleportationPortal
    {
        public PortalToPlanet1(int posX, int posY){
            DestinationMap = MapType.Planet;
            PlanetDestination = PlanetName.Alleo;
            AlternateSymbol1 = Icons.planete1TpSymbol1;
            AlternateSymbol2 = Icons.planete1TpSymbol2;
            Symbol = AlternateSymbol1;
            PosX = posX;
            PosY = posY;
        }
    }
}