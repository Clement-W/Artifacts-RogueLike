using RogueLike.Core;
using RLNET;
using System;
using RogueLike.Systems;

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

            if (RenderRequired)
            {
                mapConsole.Clear();
                statConsole.Clear();
                messageConsole.Clear();
                inventoryConsole.Clear();


                Game.Map.Draw(mapConsole, statConsole); // We need the stat console to add the monster lifebar on it if there's a monster nearby
                Game.Player.Draw(mapConsole, Game.Map);
                Game.Player.DrawStats(statConsole);
                //messagelog draw

                CameraSystem.CenterCamera(Game.Player);


                // Blit the 4 consoles in the root console
                RLConsole.Blit(mapConsole, CameraSystem.viewPortStartX, CameraSystem.viewPortStartY, Dimensions.mapConsoleWidth, Dimensions.mapConsoleHeight, Console, 0, Dimensions.inventoryConsoleHeight);
                RLConsole.Blit(statConsole, 0, 0, Dimensions.statConsoleWidth, Dimensions.statConsoleHeight, Console, Dimensions.mapConsoleWidth, 0);
                RLConsole.Blit(messageConsole, 0, 0, Dimensions.messageConsoleWidth, Dimensions.messageConsoleHeight, Console, 0, Dimensions.screenConsoleHeight - Dimensions.messageConsoleHeight);
                RLConsole.Blit(inventoryConsole, 0, 0, Dimensions.inventoryConsoleWidth, Dimensions.inventoryConsoleHeight, Console, 0, 0);

                Console.Draw();
                RenderRequired=false;

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