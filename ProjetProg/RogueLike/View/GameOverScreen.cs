using RLNET;
using RogueLike.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.View
{
    public class GameOverScreen : ScreenView
    {


        public GameOverScreen(Game game) : base(game)
        {
            RootConsole.Update += OnGameOverUpdate;
            RootConsole.Render += OnGameOverRender;
            // += Allows to add a new event handler to the rootConsole.Update event
            // += Call the add method of the event.
            RootConsole.Run();
            ChangeTitle("Game over");
        }

        private void OnGameOverRender(object sender, UpdateEventArgs e)
        {
            if (RenderRequired)
            {
                RootConsole.Clear();

                RootConsole.SetBackColor(0, 0, RootConsole.Width, RootConsole.Height, RLColor.Black);

                int centerX = RootConsole.Width / 2;
                int centerY = RootConsole.Height / 2;

                int gameOverY = (int)(RootConsole.Height * 0.3);
                string gameOver = $"ý Game over ý"; //TODO: mettre symbol artefact
                RootConsole.Print(centerX - gameOver.Length / 2, gameOverY, gameOver, RLColor.White);


                string newGame = "Press N to start";
                int quarterX = centerX / 2;
                int newGameY =gameOverY+5;
                RootConsole.Print(quarterX - newGame.Length / 2, newGameY, newGame, RLColor.Red);


                string quit = "Press Esc to quit";
                RootConsole.Print(3*quarterX - quit.Length / 2, newGameY, quit, RLColor.Red);




                RootConsole.Draw();
                RenderRequired = false;
            }
        }

        private void OnGameOverUpdate(object sender, UpdateEventArgs e)
        {
            KeyPress = RootConsole.Keyboard.GetKeyPress();

            if (KeyPress != null)
            {

                switch (KeyPress.Key)
                {
                    case RLKey.N: // N for new game
                        // Remove the unneeded event handlers
                        RootConsole.Update -= OnGameOverUpdate;
                        RootConsole.Render -= OnGameOverRender;
                        StartGame();

                        break;

                    case RLKey.Escape:
                        RootConsole.Close();
                        break;

                }

            }


        }

        private void StartGame()
        {
            Game.StartGame();
        }





    }
}
