using RLNET;

namespace RogueLike.Core
{
    public static class Dimensions
    {
        
        public static readonly int screenConsoleWidth = 80;
        public static readonly int screenConsoleHeight = 35;

        //The w and h of every parts of the screen is computed according to the screen size.


        public static readonly int mapConsoleWidth = (int)(0.8 * screenConsoleWidth);
        public static readonly int mapConsoleHeight = (int)(0.68 * screenConsoleHeight);



        public static readonly int worldWidth = mapConsoleWidth * 3;
        public static readonly int worldHeight = mapConsoleHeight * 3;


        public static readonly int messageConsoleWidth = mapConsoleWidth;
        public static readonly int messageConsoleHeight = (int)(0.14 * screenConsoleHeight);


        public static readonly int statConsoleWidth = (int)(0.5 * screenConsoleWidth);
        public static readonly int statConsoleHeight = screenConsoleHeight;


        public static readonly int inventoryConsoleWidth = mapConsoleWidth;
        public static readonly int inventoryConsoleHeight = messageConsoleHeight;

    }
}

