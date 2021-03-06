using RLNET;
using RogueLike.Core;
using System;

namespace RogueLike.View
{

    /// <summary>
    /// This class manages the displayed screen when the player win the game.
    /// </summary>
    public class WinScreen : ScreenView
    {

        /// <summary>
        /// This is the time it took the player to win the game 
        /// </summary>
        private TimeSpan gameTime;


        /// <summary>
        /// This is the constructor of this screen, that calls the constructor of the base class.
        /// </summary>
        /// <param name="time"> The time it took the player to win the game</param>
        public WinScreen(TimeSpan time) : base("Victory!")
        {
            gameTime = time;

            RootConsole.Update += OnWinUpdate;
            RootConsole.Render += OnWinRender;
            // += Allows to add a new event handler to the rootConsole.Update event
            RootConsole.Run(); // Launch the infinite console loop
        }

        /// <summary>
        /// This is the event handler for the console rendering on the win screen
        /// </summary>
        /// <param name="sender">This parameter contains a reference to the object that raised the event</param>
        /// <param name="e"> This parameter contains the event data</param>
        private void OnWinRender(object sender, UpdateEventArgs e)
        {
            if (RenderRequired)
            {
                RootConsole.Clear();
                RootConsole.SetBackColor(0, 0, RootConsole.Width, RootConsole.Height, Colors.Black);


                // Compute the center of the screen that will be used later
                int centerX = RootConsole.Width / 2;
                int centerY = RootConsole.Height / 2;

                // The y coordinate of the congratulation string
                int congratulationY = (int)(RootConsole.Height * 0.3);
                string congratulation = Symbols.artifact4Symbol + " Congratulation ! " + Symbols.artifact4Symbol;
                // Print this string on the console
                RootConsole.Print(centerX - congratulation.Length / 2, congratulationY, congratulation, Colors.BasicColor);

                // The y coordinate of the time string
                int timeY = congratulationY + 5;
                string time = $"Time : {gameTime.Hours}h : {gameTime.Minutes}m : {gameTime.Seconds}s";
                RootConsole.Print(centerX - time.Length / 2, timeY, time, Colors.Green);

                
                string newGame = "Press N to restart";
                int quarterX = centerX / 2;
                int newGameY = timeY + 5;
                RootConsole.Print(quarterX - newGame.Length / 2, newGameY, newGame, Colors.Red);


                string quit = "Press Esc to quit";
                RootConsole.Print(3 * quarterX - quit.Length / 2, newGameY, quit, Colors.Red);


                RootConsole.Draw(); 
                // Set the renderrequired bool at false to avoid making the render 
                // every time in the console loop
                RenderRequired = false; 
            }
        }

        /// <summary>
        /// This is the event handler for the console update for the win screen
        /// </summary>
        /// <param name="sender">This parameter contains a reference to the object that raised the event</param>
        /// <param name="e"> This parameter contains the event data</param>
        private void OnWinUpdate(object sender, UpdateEventArgs e)
        {
            // get the pressed key by the user
            KeyPress = RootConsole.Keyboard.GetKeyPress(); 

            if (KeyPress != null)
            {
                // If the user has pressed a key, check if it's a 'N' to create a new
                // game or check if it's 'Esc' to quit the game.
                switch (KeyPress.Key)
                {
                    case RLKey.N: // N for new game
                        // Remove the unneeded event handlers
                        RootConsole.Update -= OnWinUpdate;
                        RootConsole.Render -= OnWinRender;
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
