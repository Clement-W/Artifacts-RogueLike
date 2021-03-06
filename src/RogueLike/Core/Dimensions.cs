namespace RogueLike.Core
{
    /// <summary>
    /// This static class contains every dimensions used to fit the consoles in the screen
    /// </summary>
    public static class Dimensions
    {
        
        public static readonly int screenConsoleWidth = 52;
        public static readonly int screenConsoleHeight = 27;

        // The width and height of every parts of the screen is computed according to the screen size.


        public static readonly int mapConsoleWidth = screenConsoleWidth;
        public static readonly int mapConsoleHeight = (int)(0.70 * screenConsoleHeight);



        public static readonly int worldWidth = mapConsoleWidth * 3;
        public static readonly int worldHeight = mapConsoleHeight * 3;


        public static readonly int messageConsoleWidth = (int)(0.6 * mapConsoleWidth);
        public static readonly int messageConsoleHeight = (int)(0.25 * screenConsoleHeight);


        public static readonly int statConsoleWidth = (int)(0.6 * screenConsoleWidth);
        public static readonly int statConsoleHeight = (int)(0.15 * screenConsoleHeight);

        public static readonly int equipmentsConsoleWidth = (int)(0.2 * screenConsoleWidth);
        public static readonly int equipmentsConsoleHeight = (int)(0.3 * screenConsoleHeight);


        public static readonly int itemsConsoleWidth = (int)(0.2 * screenConsoleWidth);
        public static readonly int itemsConsoleHeight = (int)(0.3 * screenConsoleHeight);


        

    }
}

