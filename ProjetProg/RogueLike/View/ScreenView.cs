using RLNET;
using RogueLike.Core;

namespace RogueLike.View
{

    public class ScreenView
    {

        public static RLRootConsole RootConsole { get; private set; }

        public static RLKeyPress KeyPress { get; set; }

        public static bool RenderRequired { get; set; }

        public bool DidPlayerAct { get; set; }

        public static Game Game { get; set; }

        public ScreenView(string consoleTitle, Game game)
        {
            if (RootConsole == null) // First time that the static root console is created
            {
                InitializeRootConsole(consoleTitle);
            }else{
                ChangeTitle(consoleTitle);
            }

            RenderRequired = true;
            DidPlayerAct = false;
            Game = game;
        }

        public ScreenView(Game game) : this("Nom du rogue like", game)
        {
        }

        public ScreenView(string consoleTitle) : this(consoleTitle, null) { }
        public ScreenView() : this("", null) { }

        public void ChangeTitle(string consoleTitle)
        {
            RootConsole.Title = consoleTitle;
        }

        private void InitializeRootConsole(string consoleTitle)
        {
            string fontFileName = "terminal16x16.png";
            int fontSize = 16;
            float scale = 1.6f;
            RootConsole = new RLRootConsole(fontFileName, Dimensions.screenConsoleWidth, Dimensions.screenConsoleHeight, fontSize, fontSize, scale, consoleTitle);
        }


    }
}