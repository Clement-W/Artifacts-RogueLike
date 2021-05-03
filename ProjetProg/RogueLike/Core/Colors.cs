using RLNET;

namespace RogueLike.Core
{
    // Contains the colors used in the rogue like
    public static class Colors
    {
        public static RLColor FloorBackground = RLColor.Black;
        public static RLColor Floor = Palette.AlternateDarkest;
        public static RLColor FloorBackgroundFov = Palette.DbDark; // Différent si c'est dans le champ de vision
        public static RLColor FloorFov = Palette.Alternate;

        public static RLColor WallBackground = Palette.SecondaryDarkest;
        public static RLColor Wall = Palette.Secondary;
        public static RLColor WallBackgroundFov = Palette.SecondaryDarker;
        public static RLColor WallFov = Palette.SecondaryLighter;

        public static RLColor TextHeading = Palette.DbLight;

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

        public static RLColor Equipment = Palette.DbLight;

        public static RLColor Seller = Palette.DbLight;

        public static RLColor AttackedCell = Palette.DbLight;

        public static RLColor TeleportationPortal = Palette.DbLight;

        public static RLColor FloorBackgroundPlanet2 = Palette.DbOldBlood;
        public static RLColor FloorBackgroundFovPlanet2 = Palette.purplePlanet; // Différent si c'est dans le champ de vision

        public static RLColor WallBackgroundPlanet2 = Palette.SecondaryDarkest;
        public static RLColor WallBackgroundFovPlanet2 = Palette.SecondaryDarker;

        // equipement colors : 
        public static RLColor mk1Color = Palette.DbLight;
        public static RLColor mk2Color = RLColor.Yellow;
        public static RLColor mk3Color = RLColor.LightBlue;
        public static RLColor mk4Color = RLColor.Magenta;

        public static RLColor polymerColor = Palette.DbLight;
        public static RLColor carbonColor = RLColor.Gray;
        public static RLColor platinumColor = Palette.DbSun;
        public static RLColor titaniumColor = Palette.DbOldStone;


    }
}