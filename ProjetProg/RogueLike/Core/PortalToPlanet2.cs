using RLNET;
namespace RogueLike.Core
{
    public class PortalToPlanet2 : TeleportationPortal
    {
        public PortalToPlanet2(int posX, int posY){
            DestinationMap = MapType.Planet;
            PlanetDestination = PlanetName.Damari;
            AlternateSymbol1 = Symbols.planet2TpSymbol1;
            AlternateSymbol2 = Symbols.planet2TpSymbol2;
            Symbol = AlternateSymbol1;
            PosX = posX;
            PosY = posY;
        }
    }
}