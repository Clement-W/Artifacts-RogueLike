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
                RootConsole.SetBackColor(0, 0, RootConsole.Width, RootConsole.Height, RLColor.Black);

                int centerX = RootConsole.Width / 2;
                int centerY = RootConsole.Height / 2;


                int yTitle = (int)(RootConsole.Height * 0.1);
                string title = $"~ Artifacts ~"; //TODO: mettre symbol artefact
                RootConsole.Print(centerX - title.Length / 2, yTitle, title, RLColor.White);


                string artifactsSymbol = $"êéè"; //TODO: mettre les artefacts
                RootConsole.Print(centerX - artifactsSymbol.Length / 2, yTitle + 1, artifactsSymbol, RLColor.White);


                string lore1 = "A dangerous organization is looking for the 3";
                string lore2 = "legendary artifacts. Those are hidden in some";
                string lore3 = "forbidden ruins in 3 abandoned planets.";
                string lore4 = "try to find them as fast as possible.";

                string lore5 = "You've been given a spaceship to accomplish your";
                string lore6 = "mission. You can teleport to the planets through";
                string lore7 = "the 3 teleporters, and buy items to the ";
                string lore8 = "merchants. Good luck.";

                string[] loresPart1 = { lore1, lore2, lore3, lore4 };
                string[] loresPart2 = { lore5, lore6, lore7, lore8 };

                int yLorePart1 = yTitle + 3;
                for (int i = 0; i < loresPart1.Length; i++)
                {
                    RootConsole.Print(centerX - loresPart1[i].Length / 2, yLorePart1 + i, loresPart1[i], RLColor.LightGray);
                }

                int yLorePart2 = yLorePart1 + loresPart1.Length + 1;
                for (int i = 0; i < loresPart2.Length; i++)
                {
                    RootConsole.Print(centerX - loresPart2[i].Length / 2, yLorePart2 + i, loresPart2[i], RLColor.LightGray);
                }

                int yCommandTitle = yLorePart2 + loresPart2.Length + 2;
                string commandsTitle = " ~ Commands ~";
                RootConsole.Print(centerX - commandsTitle.Length / 2, yCommandTitle, commandsTitle, RLColor.White);

                int yCommands = yCommandTitle + 1;
                string command1 = "Move with z q s d";
                string command2 = "Attack by clicking toward the enemy";
                string command3 = "Press 1,2,3,4 or 5 to use items";
                string command4 = "Press Ctrl to go through teleportation portals";
                string command5 = "or stairs.";

                string[] commands = { command1, command2, command3, command4, command5 };
                for (int i = 0; i < commands.Length; i++)
                {
                    RootConsole.Print(centerX - commands[i].Length / 2, yCommands + i, commands[i], RLColor.LightGray);
                }

                string newGame = "Press N to start";
                int quarterX = centerX / 2;
                int newGameY = yCommands + commands.Length + 2;
                RootConsole.Print(quarterX - newGame.Length / 2, newGameY, newGame, RLColor.Red);


                string quit = "Press Esc to quit";
                RootConsole.Print(3*quarterX - quit.Length / 2, newGameY, quit, RLColor.Red);





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

        private void StartGame()
        {
            Game.StartGame();
        }


    }
}