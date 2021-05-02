using RLNET;
namespace RogueLike.Core
{
    public class PortalToPlanet2 : TeleportationPortal
    {
        public PortalToPlanet2(int posX, int posY){
            DestinationMap = MapType.Planet;
            PlanetDestination = PlanetName.Damari;
            Symbol = '*';
            PosX = posX;
            PosY = posY;
        }
    }
}