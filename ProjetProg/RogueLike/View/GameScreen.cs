using RogueLike.Core;
using RLNET;
using System;   

namespace RogueLike.View
{

    public class GameScreen : ScreenView
    {

        private static RLConsole mapConsole;
        private static RLConsole messageConsole;
        private static RLConsole statConsole;
        private static RLConsole inventoryConsole;

        public GameScreen(string title, Game game) : base(title, game)
        {
            // Initialize the sub consoles of the root console
            mapConsole = new RLConsole(Dimensions.worldWidth, Dimensions.worldHeight);  //mapConsole needs to be as big as the world, but only a part of it will be rendered
            messageConsole = new RLConsole(Dimensions.messageConsoleWidth, Dimensions.messageConsoleHeight);
            statConsole = new RLConsole(Dimensions.statConsoleWidth, Dimensions.statConsoleHeight);
            inventoryConsole = new RLConsole(Dimensions.inventoryConsoleWidth, Dimensions.inventoryConsoleHeight);

            Console.Update += OnGameUpdate;
            Console.Render += OnGameRender;

        }

        private void OnGameRender(object sender, UpdateEventArgs e)
        {
            //provisoire
            if (RenderRequired)
            {
                Console.Clear();

                Console.Print(
                    (int)(Console.Width * 0.1),
                    (int)(Console.Height * 0.25) - 4,
                    "En plein dans le jeu",
                    Colors.Wall
                );

                Console.Draw();
            }
        }

        private void OnGameUpdate(object sender, UpdateEventArgs e)
        {
            KeyPress = Console.Keyboard.GetKeyPress();

            if (KeyPress != null)
            {

                switch (KeyPress.Key)
                {
                    case RLKey.Escape:
                        Console.Close();
                        break;

                }

            }
        }






    }
}