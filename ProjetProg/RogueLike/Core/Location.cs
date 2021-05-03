using RLNET;

namespace RogueLike.Core
{
    // This class allows to centralize all the information about the map
    public class Location
    {
        public MapType MapType { get; set; }

        public PlanetName Planet { get; set; }

        public RLColor FloorBackgroundColor { get; set; }
        public RLColor FloorBackgroundColorInFov { get; set; }

        public RLColor WallBackgroundColor { get; set; }
        public RLColor WallBackgroundColorInFov { get; set; }

        public char FloorSymbol { get; set; }
        public char WallSymbol { get; set; }


        public void InitializeSprites()
        {
            SetColors();
            SetSymbol();
        }

        private void SetColors()
        {
            //TODO: set les couleurs planet 2
            switch (Planet)
            {
                case PlanetName.Alleo:
                    FloorBackgroundColor = Colors.FloorBackgroundPlanet1;
                    FloorBackgroundColorInFov = Colors.FloorBackgroundFovPlanet1;
                    WallBackgroundColor = Colors.WallBackgroundPlanet1;
                    WallBackgroundColorInFov = Colors.WallBackgroundFovPlanet1;
                    break;

                case PlanetName.Damari:
                    FloorBackgroundColor = Colors.FloorBackgroundPlanet2;
                    FloorBackgroundColorInFov = Colors.FloorBackgroundFovPlanet2;//new RLColor(90,3,3);
                    WallBackgroundColor = Colors.WallBackgroundPlanet2;
                    WallBackgroundColorInFov = Colors.WallBackgroundFovPlanet2;
                    break;
                    
                case PlanetName.Thaadd: //TODO: set lels couleurs planet 3
                    FloorBackgroundColor = Colors.FloorBackgroundPlanet3;
                    FloorBackgroundColorInFov = Colors.FloorBackgroundFovPlanet3;
                    WallBackgroundColor = Colors.WallBackgroundPlanet3;
                    WallBackgroundColorInFov = Colors.WallBackgroundFovPlanet3;
                    break;

                default: //For the spaceship 
                    FloorBackgroundColor = Colors.FloorBackgroundShip;
                    FloorBackgroundColorInFov = Colors.FloorBackgroundFovShip;
                    WallBackgroundColor = Colors.WallBackgroundShip;
                    WallBackgroundColorInFov = Colors.WallBackgroundFovShip;
                    break;
                    
            }

        }


        private void SetSymbol() {

            WallSymbol = '#';
            switch (MapType) {
                case MapType.BossRoom:
                    FloorSymbol = Icons.bossFloorSymbol;
                    WallSymbol = Icons.bossWallSymbol;
                    break;
                case MapType.Spaceship:
                    FloorSymbol = Icons.shipFloorSymbol;
                    WallSymbol = Icons.shipWallSymbol;
                    break;
                case MapType.Planet:
                    switch (Planet) {
                        case PlanetName.Alleo: FloorSymbol = Icons.planet1FloorSymbol;
                            WallSymbol = Icons.planet1WallSymbol;
                            break;
                        case PlanetName.Damari: FloorSymbol = Icons.planet2FloorSymbol;
                            WallSymbol = Icons.planet2WallSymbol;
                            break;
                        case PlanetName.Thaadd: FloorSymbol = Icons.planet3FloorSymbol;
                            WallSymbol = Icons.planet3WallSymbol; 
                            break;
                    }
           
                    break;
            }
        }


    }


}