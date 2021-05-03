using RLNET;
using RogueLike.Core;

namespace RogueLike.View
{

    /// <summary>
    /// This class is the parent class of every consoles.
    /// It contains the root console that can be used to display multiples screens
    /// like the game over screen, the launch screen or the game screen
    /// Every screen classes inherit this class
    /// </summary>
    public class ScreenView
    {


        /// <value>This is the root console, on which multiple consoles can be embedded in it (we use the word blit)</value>
        public static RLRootConsole RootConsole { get; private set; }

        /// <value>The key that is pressed by the user</value>
        public static RLKeyPress KeyPress { get; set; }

        /// <value>A boolean that indicates if the screen has to update the render </value>
        public static bool RenderRequired { get; set; }

        /// <value>A boolean that indicates if the player did something
        /// This is used to know if the game screen has to render or not</value>
        public bool DidPlayerAct { get; set; }

        /// <value>This is the game instance that contains every elements in the game</value>
        public static Game Game { get; set; }

        /// <summary>
        /// This is the constructor of the screen view. It is called by the subclasses that inherit from this class.
        /// </summary>
        /// <param name="consoleTitle"> The name of the console</param>
        /// <param name="game">The game instance</param>
        public ScreenView(string consoleTitle, Game game)
        {
            if (RootConsole == null) // First and only time that the static root console is created
            {
                InitializeRootConsole(consoleTitle);
                Game = game;
            }
            else
            {
                ChangeTitle(consoleTitle);
            }

            RenderRequired = true;
            DidPlayerAct = false;
            
        }

  
        /// <summary>
        /// Another constructer that is used when the game has already been instancied
        /// </summary>
        /// <param name="consoleTitle">The title of the created console</param>
        /// <returns></returns>
        public ScreenView(string consoleTitle) : this(consoleTitle, null) { }


        /// <summary>
        /// This method allows to change the title of the console
        /// </summary>
        /// <param name="consoleTitle"></param>
        public void ChangeTitle(string consoleTitle)
        {
            RootConsole.Title = consoleTitle;
        }

        /// <summary>
        /// This method is used to initialize the root console.
        /// </summary>
        /// <param name="consoleTitle">The name of the console</param>
        private void InitializeRootConsole(string consoleTitle)
        {
            string fontFileName = "terminal16x16.png"; 
            int fontSize = 16;
            float scale = 1.6f;
            RootConsole = new RLRootConsole(fontFileName, Dimensions.screenConsoleWidth, Dimensions.screenConsoleHeight, fontSize, fontSize, scale, consoleTitle);
        }

        // Called to create a new game
        protected void StartGame()
        {
            Game.StartGame();
        }


    }
}