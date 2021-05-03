using RLNET;
namespace RogueLike.Core
{
    public class PortalToPlanet2 : TeleportationPortal
    {
        public PortalToPlanet2(int posX, int posY){
            DestinationMap = MapType.Planet;
            PlanetDestination = PlanetName.Damari;
            AlternateSymbol1 = Icons.planete2TpSymbol1;
            AlternateSymbol2 = Icons.planete2TpSymbol2;
            Symbol = AlternateSymbol1;
            PosX = posX;
            PosY = posY;
        }
    }
}