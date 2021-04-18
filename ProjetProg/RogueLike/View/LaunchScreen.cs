using System;
using RLNET;
using RogueLike.Core;


namespace RogueLike.View
{

    public class LaunchScreen : ScreenView
    {



        public LaunchScreen(Game game) : base(game)
        {
            Console.Update += OnLaunchUpdate;
            Console.Render += OnLaunchRender;
            // += Allows to add a new event handler to the rootConsole.Update event
            // += Call the add method of the event.
            Console.Run();
        }

        private void OnLaunchRender(object sender, UpdateEventArgs e)
        {
            if (RenderRequired)
            {
                Console.Clear();

                Console.Print(
                    (int)(Console.Width * 0.1),
                    (int)(Console.Height * 0.25) - 4,
                    "Afficher des trucs pour lancer le jeu, quitter, avoir les commandes",
                    Colors.Wall
                );

                Console.Draw();
            }
        }

        private void OnLaunchUpdate(object sender, UpdateEventArgs e)
        {
            KeyPress = Console.Keyboard.GetKeyPress();

            if (KeyPress != null)
            {

                switch (KeyPress.Key)
                {
                    case RLKey.N: // N for new game
                        // Remove the unneeded event handlers
                        Console.Update -= OnLaunchUpdate;
                        Console.Render -= OnLaunchRender;
                        StartGame();                     

                        break;

                    case RLKey.Escape:
                        Console.Close();
                        break;

                }

            }

            
        }

        private void StartGame(){
            Game.StartGame();
        }


    }
}