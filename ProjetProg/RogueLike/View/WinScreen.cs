using RLNET;
using RogueLike.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.View {
    public class WinScreen: ScreenView 
    {

        private TimeSpan gameTime;


        public WinScreen(Game game,TimeSpan time) : base(game) {
            gameTime = time;

            RootConsole.Update += OnWinUpdate;
            RootConsole.Render += OnWinRender;
            // += Allows to add a new event handler to the rootConsole.Update event
            // += Call the add method of the event.
            RootConsole.Run();
            ChangeTitle("Victory!");
        }

        private void OnWinRender(object sender, UpdateEventArgs e) {
            if (RenderRequired) {
                RootConsole.Clear();

                RootConsole.SetBackColor(0, 0, RootConsole.Width, RootConsole.Height, RLColor.Black);

                int centerX = RootConsole.Width / 2;
                int centerY = RootConsole.Height / 2;

                int victoryY = (int)(RootConsole.Height * 0.3);
                string congratulation = $"~ Congratulation ! ~"; //TODO: mettre symbol artefact
                RootConsole.Print(centerX - congratulation.Length / 2, victoryY, congratulation, RLColor.White);

                int timeY = victoryY+5;
                string time = $"Game time : {gameTime.Hours}h : {gameTime.Minutes}m : {gameTime.Seconds}s" ;
                RootConsole.Print(centerX - time.Length / 2, timeY, time, RLColor.Green);


                string newGame = "Press N to restart";
                int quarterX = centerX / 2;
                int newGameY =timeY+5;
                RootConsole.Print(quarterX - newGame.Length / 2, newGameY, newGame, RLColor.Red);


                string quit = "Press Esc to quit";
                RootConsole.Print(3*quarterX - quit.Length / 2, newGameY, quit, RLColor.Red);


                RootConsole.Draw();
            }
        }

        private void OnWinUpdate(object sender, UpdateEventArgs e) {
            KeyPress = RootConsole.Keyboard.GetKeyPress();

            if (KeyPress != null) {

                switch (KeyPress.Key) {
                    case RLKey.N: // N for new game
                        // Remove the unneeded event handlers
                        RootConsole.Update -= OnWinUpdate;
                        RootConsole.Render -= OnWinRender;
                        StartGame();
                        break;

                    case RLKey.Escape:
                        RootConsole.Close();
                        break;

                }

            }


        }

        private void StartGame() {
            Game.StartGame();
        }





    }
}
