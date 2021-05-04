using RLNET;
using RogueLike.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.View
{
    /// <summary>
    /// This class is the game over screen when the player dies
    /// </summary>
    public class GameOverScreen : ScreenView
    {


        /// <summary>
        /// This is the constructor of this screen, that calls the constructor of the base class.
        /// </summary>
        public GameOverScreen() : base("Game over")
        {
            RootConsole.Update += OnGameOverUpdate;
            RootConsole.Render += OnGameOverRender;
            // += Allows to add a new event handler to the rootConsole.Update event
            // += Call the add method of the event.
            RootConsole.Run();
        }

        /// <summary>
        /// This is the event handler for the console rendering on the game over screen
        /// </summary>
        /// <param name="sender">This parameter contains a reference to the object that raised the event</param>
        /// <param name="e"> This parameter contains the event data</param>
        private void OnGameOverRender(object sender, UpdateEventArgs e)
        {
            if (RenderRequired)
            {
                RootConsole.Clear();
                RootConsole.SetBackColor(0, 0, RootConsole.Width, RootConsole.Height, RLColor.Black);

                // Compute the center of the screen that will be used later
                int centerX = RootConsole.Width / 2;
                int centerY = RootConsole.Height / 2;

                // The y coordinate of the game over string
                int gameOverY = (int)(RootConsole.Height * 0.3);
                string gameOver = $"ý Game over ý"; //TODO: mettre symbol artefact
                // Print this string on the console
                RootConsole.Print(centerX - gameOver.Length / 2, gameOverY, gameOver, RLColor.White);


                string newGame = "Press N to start";
                int quarterX = centerX / 2;
                int newGameY = gameOverY + 5;
                RootConsole.Print(quarterX - newGame.Length / 2, newGameY, newGame, RLColor.Red);

                string quit = "Press Esc to quit";
                RootConsole.Print(3 * quarterX - quit.Length / 2, newGameY, quit, RLColor.Red);

                RootConsole.Draw();
                RenderRequired = false;
            }
        }


        /// <summary>
        /// This is the event handler for the console update for the game over screen
        /// </summary>
        /// <param name="sender">This parameter contains a reference to the object that raised the event</param>
        /// <param name="e"> This parameter contains the event data</param>
        private void OnGameOverUpdate(object sender, UpdateEventArgs e)
        {
            // Get the pressed key by the user
            KeyPress = RootConsole.Keyboard.GetKeyPress();

            if (KeyPress != null)
            {
                // If the user has pressed a key, check if it's a 'N' to create a new
                // game or check if it's 'Esc' to quit the game.
                switch (KeyPress.Key)
                {
                    case RLKey.N: // N for new game
                        // Remove the unneeded event handlers
                        RootConsole.Update -= OnGameOverUpdate;
                        RootConsole.Render -= OnGameOverRender;
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
