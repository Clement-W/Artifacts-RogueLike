using RLNET;
namespace RogueLike.Core
{
    public class PortalToSpaceship : TeleportationPortal
    {
        public PortalToSpaceship(int posX, int posY){
            DestinationMap = MapType.Spaceship;
            Symbol = '*';
            PosX = posX;
            PosY = posY;
        }
    }
}