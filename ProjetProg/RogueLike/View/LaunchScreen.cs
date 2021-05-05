using System;
using RLNET;
using RogueLike.Core;


namespace RogueLike.View
{

    /// <summary>
    /// This class is the displayed screen when the user launches the game.
    /// </summary>
    public class LaunchScreen : ScreenView
    {

        /// <summary>
        /// This is the constructor of the launch screen which calls the base class constructor
        /// </summary>
        /// <param name="game">The game instance that contains every game elements</param>
        public LaunchScreen(Game game) : base("Artifacts", game)
        {
            RootConsole.Update += OnLaunchUpdate;
            RootConsole.Render += OnLaunchRender;
            // += Allows to add a new event handler to the rootConsole.Update event
            // += Call the add method of the event.
            RootConsole.Run();
        }

        /// <summary>
        /// This is the event handler for the console rendering for the launch screen
        /// </summary>
        /// <param name="sender">This parameter contains a reference to the object that raised the event</param>
        /// <param name="e"> This parameter contains the event data</param>
        private void OnLaunchRender(object sender, UpdateEventArgs e)
        {
            if (RenderRequired)
            {
                RootConsole.Clear();
                RootConsole.SetBackColor(0, 0, RootConsole.Width, RootConsole.Height, Colors.Black);

                // Compute the center of the screen that will be used later
                int centerX = RootConsole.Width / 2;
                int centerY = RootConsole.Height / 2;


                // The y coordinate of the title string
                int titleY = (int)(RootConsole.Height * 0.1);
                string title = "" + Symbols.artifact4Symbol + " Artifacts " + Symbols.artifact4Symbol; //TODO: mettre symbol artefact
                // Print this string on the console
                RootConsole.Print(centerX - title.Length / 2, titleY, title, Colors.BasicColor);

                string artifactsSymbol = "" + Symbols.artifact1Symbol + "   " + Symbols.artifact2Symbol + "   " + Symbols.artifact3Symbol; //TODO: mettre les artefacts
                RootConsole.Print(centerX - artifactsSymbol.Length / 2, titleY + 1, artifactsSymbol, Colors.BasicColor);

                // The story of the game is called "lore" in RPGs
                string lore1 = "A dangerous organization is looking for the 3";
                string lore2 = "legendary artifacts. Those are hidden in some";
                string lore3 = "forbidden ruins in 3 abandoned planets.";
                string lore4 = "Try to find them as fast as possible.";

                string lore5 = "You've been given a spaceship to accomplish your";
                string lore6 = "mission. You can teleport to the planets through";
                string lore7 = "the 3 teleporters, and buy items to the ";
                string lore8 = "merchants. Good luck.";

                //Put them in an array to print them more easily
                string[] loresPart1 = { lore1, lore2, lore3, lore4 };
                string[] loresPart2 = { lore5, lore6, lore7, lore8 };

                // The y coordinate of the first lore string (part 1)
                int lorePart1Y = titleY + 3;
                for (int i = 0; i < loresPart1.Length; i++)
                {
                    RootConsole.Print(centerX - loresPart1[i].Length / 2, lorePart1Y + i, loresPart1[i], Colors.LightGray);
                }

                // The y coordinate of the first lore string (part 2)
                int lorePart2Y = lorePart1Y + loresPart1.Length + 1;
                for (int i = 0; i < loresPart2.Length; i++)
                {
                    RootConsole.Print(centerX - loresPart2[i].Length / 2, lorePart2Y + i, loresPart2[i], Colors.LightGray);
                }

                // The y coordinate of the command title
                int commandTitleY = lorePart2Y + loresPart2.Length + 2;
                string commandsTitle = Symbols.playerDownSymbol+ " Commands "+ Symbols.playerDownSymbol;
                RootConsole.Print(centerX - commandsTitle.Length / 2, commandTitleY, commandsTitle, Colors.BasicColor);

                // The y coordinate of the first command explanation
                int commandsY = commandTitleY + 1;
                string command1 = "Move with z q s d";
                string command2 = "Attack by clicking toward the enemy";
                string command3 = "Press 1,2,3,4 or 5 to use items";
                string command4 = "Press Left Ctrl to go through teleportation portals";
                string command5 = "or stairs.";

                string[] commands = { command1, command2, command3, command4, command5 };
                for (int i = 0; i < commands.Length; i++)
                {
                    RootConsole.Print(centerX - commands[i].Length / 2, commandsY + i, commands[i], Colors.LightGray);
                }

                string newGame = "Press N to start";
                int quarterX = centerX / 2;
                int newGameY = commandsY + commands.Length + 2;
                RootConsole.Print(quarterX - newGame.Length / 2, newGameY, newGame, Colors.Red);


                string quit = "Press Esc to quit";
                RootConsole.Print(3 * quarterX - quit.Length / 2, newGameY, quit, Colors.Red);


                RootConsole.Draw();
            }
        }

        /// <summary>
        /// This is the event handler for the console update for the launch screen
        /// </summary>
        /// <param name="sender">This parameter contains a reference to the object that raised the event</param>
        /// <param name="e"> This parameter contains the event data</param>

        private void OnLaunchUpdate(object sender, UpdateEventArgs e)
        {
            // get the pressed key by the user
            KeyPress = RootConsole.Keyboard.GetKeyPress();

            if (KeyPress != null)
            {

                // If the user has pressed a key, check if it's an 'N' to create a new
                // game or check if it's 'Esc' to quit the game.
                switch (KeyPress.Key)
                {
                    case RLKey.N: // N for new game
                        // Remove the unneeded event handlers
                        RootConsole.Update -= OnLaunchUpdate;
                        RootConsole.Render -= OnLaunchRender;
                        StartGame(); // Start a new game

                        break;

                    case RLKey.Escape:
                        RootConsole.Close();
                        break;

                }
            }
        }
    }
}