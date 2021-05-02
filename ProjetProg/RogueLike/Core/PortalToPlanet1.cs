using RLNET;
namespace RogueLike.Core
{
    public class PortalToPlanet1 : TeleportationPortal
    {
        public PortalToPlanet1(int posX, int posY){
            DestinationMap = MapType.Planet;
            PlanetDestination = PlanetName.Alleo;
            Symbol = '*';
            PosX = posX;
            PosY = posY;
        }
    }
}