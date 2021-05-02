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


        public void SetColors()
        {
            //TODO: set les couleurs planet 2
            switch (Planet)
            {
                case PlanetName.Planet1:
                    FloorBackgroundColor = Colors.FloorBackground;
                    FloorBackgroundColorInFov = Colors.FloorBackgroundFov;
                    WallBackgroundColor = Colors.WallBackground;
                    WallBackgroundColorInFov = Colors.WallBackgroundFov;
                    break;

                case PlanetName.Planet2:
                    FloorBackgroundColor = Colors.FloorBackgroundPlanet2;
                    FloorBackgroundColorInFov = Colors.FloorBackgroundFovPlanet2;
                    WallBackgroundColor = Colors.WallBackgroundPlanet2;
                    WallBackgroundColorInFov = Colors.WallBackgroundFovPlanet2;
                    break;

                case PlanetName.Planet3: //TODO: set lels couleurs planet 3
                    FloorBackgroundColor = Colors.FloorBackground;
                    FloorBackgroundColorInFov = Colors.FloorBackgroundFov;
                    WallBackgroundColor = Colors.WallBackground;
                    WallBackgroundColorInFov = Colors.WallBackgroundFov;
                    break;

                default: //For the spaceship 
                    FloorBackgroundColor = Colors.FloorBackground;
                    FloorBackgroundColorInFov = Colors.FloorBackgroundFov;
                    WallBackgroundColor = Colors.WallBackground;
                    WallBackgroundColorInFov = Colors.WallBackgroundFov;
                    break;
            }
        }


    }


}