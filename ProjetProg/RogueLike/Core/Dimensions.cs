using RLNET;

namespace RogueLike.Core
{
    public static class Dimensions
    {
        
        public static readonly int screenConsoleWidth = 44;
        public static readonly int screenConsoleHeight = 24;

        //The w and h of every parts of the screen is computed according to the screen size.


        public static readonly int mapConsoleWidth = screenConsoleWidth;
        public static readonly int mapConsoleHeight = (int)(0.70 * screenConsoleHeight);



        public static readonly int worldWidth = mapConsoleWidth * 3;
        public static readonly int worldHeight = mapConsoleHeight * 3;


        public static readonly int messageConsoleWidth = (int)(0.6 * mapConsoleWidth);
        public static readonly int messageConsoleHeight = (int)(0.2 * screenConsoleHeight);


        public static readonly int statConsoleWidth = (int)(0.6 * screenConsoleWidth);
        public static readonly int statConsoleHeight = (int)(0.2 * screenConsoleHeight);

        public static readonly int equipmentsConsoleWidth = (int)(0.2 * screenConsoleWidth);
        public static readonly int equipmentsConsoleHeight = (int)(0.3 * screenConsoleHeight);


        public static readonly int itemsConsoleWidth = (int)(0.2 * screenConsoleWidth);
        public static readonly int itemsConsoleHeight = (int)(0.3 * screenConsoleHeight);


        

    }
}

