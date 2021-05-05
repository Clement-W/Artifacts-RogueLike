using RLNET;

namespace RogueLike.Core
{
    /// <summary>
    /// This static class contains the colors used in the game
    /// </summary>
    public static class Colors
    {

        public static RLColor Floor = Palette.AlternateDarkest;

        public static RLColor FloorFov = Palette.Alternate;


        public static RLColor Wall = Palette.Secondary;

        public static RLColor WallFov = Palette.SecondaryLighter;


        public static RLColor Player = Palette.DbLight;
        public static RLColor Text = Palette.DbLight;

        public static RLColor GrayText = Palette.DbDarkLight;
        public static RLColor Gold = Palette.DbSun;

        
        public static RLColor HealthBar = RLColor.Red;

        public static RLColor HealthBarDamage = Palette.PrimaryDarkest;


        public static RLColor Zombie = Palette.DbVegetation;


        public static RLColor StairsFov = Palette.DbLight;

        public static RLColor Stairs = Floor;

        public static RLColor PlayerHit = RLColor.Red;

        public static RLColor ZombieHit = RLColor.Red;


        public static RLColor Seller = Palette.DbLight;

        public static RLColor AttackedCell = Palette.DbLight;

        public static RLColor TeleportationPortal = Palette.DbLight;


        public static RLColor FloorBackgroundPlanet1 = Palette.DbDeepWater;
        public static RLColor FloorBackgroundFovPlanet1 = Palette.DbWater; // Différent si c'est dans le champ de vision
        public static RLColor WallBackgroundPlanet1 = Palette.iceWallDark;
        public static RLColor WallBackgroundFovPlanet1 = Palette.iceWall;

        public static RLColor FloorBackgroundPlanet2 = Palette.DbOldBlood;
        public static RLColor FloorBackgroundFovPlanet2 = Palette.purplePlanet; // Différent si c'est dans le champ de vision
        public static RLColor WallBackgroundPlanet2 = Palette.purplePlanetwallDark;
        public static RLColor WallBackgroundFovPlanet2 = Palette.purplePlanetwall;

        public static RLColor FloorBackgroundPlanet3 = Palette.DbVegetation;
        public static RLColor FloorBackgroundFovPlanet3 = Palette.DbGrass; // Différent si c'est dans le champ de vision
        public static RLColor WallBackgroundPlanet3 = Palette.darkestGreen;
        public static RLColor WallBackgroundFovPlanet3 = Palette.darkGreen;

        public static RLColor FloorBackgroundShip = RLColor.Gray;
        public static RLColor FloorBackgroundFovShip = RLColor.LightGray; // Différent si c'est dans le champ de vision
        public static RLColor WallBackgroundShip = Palette.SecondaryDarkest;
        public static RLColor WallBackgroundFovShip = Palette.SecondaryDarker;

        // equipement colors : 
        public static RLColor mk1Color = Palette.DbLight;
        public static RLColor mk2Color = RLColor.Yellow;
        public static RLColor mk3Color = RLColor.LightBlue;
        public static RLColor mk4Color = RLColor.Magenta;

        public static RLColor polymerColor = Palette.DbLight;
        public static RLColor carbonColor = RLColor.Gray;
        public static RLColor platinumColor = Palette.DbSun;
        public static RLColor titaniumColor = Palette.DbOldStone;

        // basic :
        public static RLColor BasicColor = RLColor.White;
        public static RLColor Black = RLColor.Black;
        public static RLColor Red = RLColor.Red;
        public static RLColor Green = RLColor.Green;
        public static RLColor LightGray = RLColor.LightGray;

    }
}