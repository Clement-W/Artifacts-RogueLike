using RLNET;
namespace RogueLike.Core
{
    public class PortalToPlanet3 : TeleportationPortal
    {
        public PortalToPlanet3(int posX, int posY){
            DestinationMap = MapType.Planet;
            PlanetDestination = PlanetName.Thaadd;
            Symbol = '*';
            PosX = posX;
            PosY = posY;
        }
    }
}