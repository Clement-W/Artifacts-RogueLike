using RLNET;
namespace RogueLike.Core
{
    public class PortalToPlanet3 : TeleportationPortal
    {
        public PortalToPlanet3(int posX, int posY){
            DestinationMap = MapType.Planet;
            PlanetDestination = PlanetName.Thaadd;
            AlternateSymbol1 = Icons.planete3TpSymbol1;
            AlternateSymbol2 = Icons.planete3TpSymbol2;
            Symbol = AlternateSymbol1;
            PosX = posX;
            PosY = posY;
        }
    }
}