using System;
using RLNET;
using RogueLike.Core;


namespace RogueLike.View
{

    public class LaunchScreen : ScreenView
    {

        public LaunchScreen(Game game) : base(game)
        {
            RootConsole.Update += OnLaunchUpdate;
            RootConsole.Render += OnLaunchRender;
            // += Allows to add a new event handler to the rootConsole.Update event
            // += Call the add method of the event.
            RootConsole.Run();
        }

        private void OnLaunchRender(object sender, UpdateEventArgs e)
        {
            if (RenderRequired)
            {
                RootConsole.Clear();

                RootConsole.Print(
                    (int)(RootConsole.Width * 0.1),
                    (int)(RootConsole.Height * 0.25) - 4,
                    "Afficher des trucs pour lancer le jeu, quitter, avoir les commandes : N",
                    Colors.Text
                );

                RootConsole.Draw();
            }
        }

        private void OnLaunchUpdate(object sender, UpdateEventArgs e)
        {
            KeyPress = RootConsole.Keyboard.GetKeyPress();

            if (KeyPress != null)
            {

                switch (KeyPress.Key)
                {
                    case RLKey.N: // N for new game
                        // Remove the unneeded event handlers
                        RootConsole.Update -= OnLaunchUpdate;
                        RootConsole.Render -= OnLaunchRender;
                        StartGame();                     

                        break;

                    case RLKey.Escape:
                        RootConsole.Close();
                        break;

                }

            }

            
        }

        private void StartGame(){
            Game.StartGame();
        }


    }
}