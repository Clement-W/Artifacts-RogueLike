using RLNET;

namespace RogueLike.Core
{

    /// <summary>
    /// This class allows to centralize all the information about the map
    /// </summary>
    public class Location
    {


        /// <value>
        /// The map type of the location
        /// </value>
        public MapType MapType { get; set; }

        /// <value>
        /// The planet type of the location
        /// </value>
        public PlanetName Planet { get; set; }

        /// <value>
        /// The background color for the floor of this location
        /// </value>
        public RLColor FloorBackgroundColor { get; set; }

        /// <value>
        /// The background color for the floor in Fov of this location
        /// </value>
        public RLColor FloorBackgroundColorInFov { get; set; }

        /// <value>
        /// The background color for the walll of this location
        /// </value>
        public RLColor WallBackgroundColor { get; set; }

        /// <value>
        /// The background color for the wall in Fov of this location
        /// </value>
        public RLColor WallBackgroundColorInFov { get; set; }

        /// <value>
        /// The symbol of the floor in this location
        /// </value>
        public char FloorSymbol { get; set; }

        /// <value>
        /// The symbol of the walls in this location
        /// </value>
        public char WallSymbol { get; set; }


        /// <summary>
        /// Initialize the sprites according to the map type and the planet
        /// </summary>
        public void InitializeSprites()
        {
            SetColors();
            SetSymbol();
        }

        /// <summary>
        /// Set the color of the background according to the planet type
        /// </summary>
        private void SetColors()
        {
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
                    FloorBackgroundColorInFov = Colors.FloorBackgroundFovPlanet2;
                    WallBackgroundColor = Colors.WallBackgroundPlanet2;
                    WallBackgroundColorInFov = Colors.WallBackgroundFovPlanet2;
                    break;

                case PlanetName.Thaadd: 
                    FloorBackgroundColor = Colors.FloorBackgroundPlanet3;
                    FloorBackgroundColorInFov = Colors.FloorBackgroundFovPlanet3;
                    WallBackgroundColor = Colors.WallBackgroundPlanet3;
                    WallBackgroundColorInFov = Colors.WallBackgroundFovPlanet3;
                    break;

                default: 
                    FloorBackgroundColor = Colors.FloorBackgroundShip;
                    FloorBackgroundColorInFov = Colors.FloorBackgroundFovShip;
                    WallBackgroundColor = Colors.WallBackgroundShip;
                    WallBackgroundColorInFov = Colors.WallBackgroundFovShip;
                    break;

            }

        }

        /// <summary>
        /// Set the symbol of the floor and the walls according to the map type and the planet
        /// </summary>
        private void SetSymbol()
        {

            WallSymbol = '#';
            switch (MapType)
            {
                case MapType.BossRoom:
                    FloorSymbol = Symbols.bossFloorSymbol;
                    WallSymbol = Symbols.bossWallSymbol;
                    break;
                case MapType.Spaceship:
                    FloorSymbol = Symbols.shipFloorSymbol;
                    WallSymbol = Symbols.shipWallSymbol;
                    break;
                case MapType.Planet:
                    switch (Planet)
                    { //If it's a planet, the symbol change according to the planets
                        case PlanetName.Alleo:
                            FloorSymbol = Symbols.planet1FloorSymbol;
                            WallSymbol = Symbols.planet1WallSymbol;
                            break;
                        case PlanetName.Damari:
                            FloorSymbol = Symbols.planet2FloorSymbol;
                            WallSymbol = Symbols.planet2WallSymbol;
                            break;
                        case PlanetName.Thaadd:
                            FloorSymbol = Symbols.planet3FloorSymbol;
                            WallSymbol = Symbols.planet3WallSymbol;
                            break;
                    }

                    break;
            }
        }
    }
}