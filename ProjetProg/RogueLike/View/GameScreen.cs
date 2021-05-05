using System.Diagnostics;
using System;

using RogueSharp;
using RLNET;

using RogueLike.Core;
using RogueLike.Systems;




namespace RogueLike.View
{

    /// <summary>
    /// This class manages the game screen when the player start or play the game
    /// </summary>
    public class GameScreen : ScreenView
    {

        /// <summary>
        /// This is the console that displays the map
        /// </summary>
        private static RLConsole mapConsole;

        /// <summary>
        /// This is the console that displays the messages
        /// </summary>
        private static RLConsole messageConsole;

        /// <summary>
        /// This is the console that displays the player's stats
        /// </summary>
        private static RLConsole statConsole;

        /// <summary>
        /// This is the console that displays the equipments (weapon, armor)
        /// </summary>
        private static RLConsole equipmentsConsole;

        /// <summary>
        /// This is the console that displays the items (food, health kit,...)
        /// </summary>
        private static RLConsole itemsConsole;

        /// <summary>
        /// This is the time it took the player to win the game 
        /// </summary>
        private Stopwatch gameTime;


        /// <summary>
        /// This is the constructor of the game screen
        /// </summary>
        /// <param name="title"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public GameScreen(Game game) : base("Spaceship", game)
        {
            // Initialize the sub consoles of the root console

            //mapConsole needs to be as big as the world, but only a part of it will be rendered
            mapConsole = new RLConsole(Dimensions.worldWidth, Dimensions.worldHeight);
            messageConsole = new RLConsole(Dimensions.messageConsoleWidth, Dimensions.messageConsoleHeight);
            statConsole = new RLConsole(Dimensions.statConsoleWidth, Dimensions.statConsoleHeight);
            equipmentsConsole = new RLConsole(Dimensions.equipmentsConsoleWidth, Dimensions.equipmentsConsoleHeight);
            itemsConsole = new RLConsole(Dimensions.itemsConsoleWidth, Dimensions.itemsConsoleHeight);

            RootConsole.Update += OnGameUpdate;
            RootConsole.Render += OnGameRender;
            // += Allows to add a new event handler to the rootConsole.Update event

            // Start the stopwatch to count the time it took the player to win
            gameTime = new Stopwatch();
            gameTime.Start();

        }



        /// <summary>
        /// This is the event handler for the console rendering on the game screen
        /// </summary>
        /// <param name="sender">This parameter contains a reference to the object that raised the event</param>
        /// <param name="e"> This parameter contains the event data</param>
        private void OnGameRender(object sender, UpdateEventArgs e)
        {

            if (RenderRequired)
            {
                RootConsole.Clear();
                RootConsole.SetBackColor(0, 0, RootConsole.Width, RootConsole.Height, RLColor.Black);

                // Animate every elements that can be animated in the map
                Game.Animation.AnimateAnimatedElements(Game.Map);

                // Clear every subconsoles
                mapConsole.Clear();
                statConsole.Clear();
                messageConsole.Clear();
                equipmentsConsole.Clear();
                itemsConsole.Clear();

                // Draw every subconsoles
                Game.Map.Draw(mapConsole);
                Game.Player.Draw(mapConsole, Game.Map);
                Game.Player.DrawStats(statConsole);
                Game.Player.DrawEquipmentInventory(equipmentsConsole);
                Game.Player.DrawItemsInventory(itemsConsole);
                Core.Game.Messages.Draw(messageConsole);
                Game.Camera.CenterCamera(Game.Player);


                // Blit the sub consoles in the root console
                RLConsole.Blit(mapConsole, CameraSystem.viewPortStartX, CameraSystem.viewPortStartY, Dimensions.mapConsoleWidth, Dimensions.mapConsoleHeight, RootConsole, 0, 0);
                RLConsole.Blit(statConsole, 0, 0, Dimensions.statConsoleWidth, Dimensions.statConsoleHeight, RootConsole, Dimensions.equipmentsConsoleWidth, Dimensions.mapConsoleHeight);
                RLConsole.Blit(messageConsole, 0, 0, Dimensions.messageConsoleWidth, Dimensions.messageConsoleHeight, RootConsole, Dimensions.equipmentsConsoleWidth, Dimensions.mapConsoleHeight + Dimensions.statConsoleHeight);
                RLConsole.Blit(equipmentsConsole, 0, 0, Dimensions.equipmentsConsoleWidth, Dimensions.equipmentsConsoleHeight, RootConsole, 0, Dimensions.mapConsoleHeight);
                RLConsole.Blit(itemsConsole, 0, 0, Dimensions.itemsConsoleWidth, Dimensions.itemsConsoleHeight, RootConsole, Dimensions.equipmentsConsoleWidth + Dimensions.messageConsoleWidth, Dimensions.mapConsoleHeight);


                RootConsole.Draw();
                RenderRequired = false;

            }
        }

        /// <summary>
        /// This is the event handler for the console update for the game screen
        /// </summary>
        /// <param name="sender">This parameter contains a reference to the object that raised the event</param>
        /// <param name="e"> This parameter contains the event data</param>
        private void OnGameUpdate(object sender, UpdateEventArgs e)
        {

            //Check the scheduling system to move the enemies
            Game.Scheduling.CheckSchedule(Game);


            DidPlayerAct = false;
            KeyPress = RootConsole.Keyboard.GetKeyPress();

            // If the player clicks, update their orientation and attack
            if (RootConsole.Mouse.GetLeftClick())
            {
                UpdateOrientation(); // The player looks toward the click direction
                Game.Command.Attack(Game.Map,Game.Player,Game.Player); // Attack the targeted cells
                RenderRequired = true;
            }

            if (KeyPress != null)
            {
                // If the user has pressed a key, switch frorm the possibles keys
                switch (KeyPress.Key)
                {

                    case RLKey.Escape: RootConsole.Close(); break;
                    // RLNET works with qwerty keybords
                    case RLKey.W: DidPlayerAct = Game.Command.MovePlayer(Game.Player, Direction.Up, Game.Map); break;
                    case RLKey.S: DidPlayerAct = Game.Command.MovePlayer(Game.Player, Direction.Down, Game.Map); break;
                    case RLKey.A: DidPlayerAct = Game.Command.MovePlayer(Game.Player, Direction.Left, Game.Map); break;
                    case RLKey.D: DidPlayerAct = Game.Command.MovePlayer(Game.Player, Direction.Right, Game.Map); break;
                    case RLKey.LControl: // Left control to go through staircases or teleportation portals
                        if (Game.Map.IsPlayerOnStaircase(Game.Player))
                        {
                            CreateNextStage();
                        }
                        // If the player is not on staircase but on a teleportation portal
                        else if (Game.Map.IsPlayerOnTeleportationPortal(Game.Player))
                        {
                            TeleportPlayer();
                        }
                        break;

                    // To use the items, the player needs to press 1,2,3,4 or 5 for the 5 item slots
                    case RLKey.Number1: DidPlayerAct = true; Game.Player.UseItem(0); break;
                    case RLKey.Number2: DidPlayerAct = true; Game.Player.UseItem(1); break;
                    case RLKey.Number3: DidPlayerAct = true; Game.Player.UseItem(2); break;
                    case RLKey.Number4: DidPlayerAct = true; Game.Player.UseItem(3); break;
                    case RLKey.Number5: DidPlayerAct = true; Game.Player.UseItem(4); break;
                }

            }

            if (DidPlayerAct == true)
            {
                RenderRequired = true;
            }



            // Check if the player has died
            CheckPlayerDeath();
            //Check if the player has won
            CheckPlayerWin();



        }

        /// <summary>
        /// Called when the player is on staircase and presses Ctrl
        /// This method creates the next stage of the map, it can be a cave map or a boss room
        /// </summary>
        private void CreateNextStage()
        {
            // While the player doesn't access the second floor, go to the next stage
            if (Game.CurrentLevel < 3)
            {
                // Create a mapCreation to generate the corresponding map
                // Increase game current level with ++Game.CurrentLevel
                MapCreation mapCreation = new MapCreation(Dimensions.worldWidth, Dimensions.worldHeight, ++Game.CurrentLevel, Game.Player.ArtifactsCollected.Count, Game.Map.MapLocation.MapType, Game.Map.MapLocation.Planet);
                // Assign the new map to the current game map
                Game.Map = mapCreation.CreateMap(Game.Player);


                DidPlayerAct = true;
                // Change the title of the console according to the planet name and the level
                string planetName = Game.Map.MapLocation.Planet.ToString();
                ChangeTitle($"{planetName} - Level {Game.CurrentLevel}");

            }
            else
            { // When the palyer wants to access the third floor, create a boss room 
                MapCreation mapCreation = new MapCreation(Dimensions.worldWidth, Dimensions.worldHeight, ++Game.CurrentLevel, Game.Player.ArtifactsCollected.Count, MapType.BossRoom, Game.Map.MapLocation.Planet);
                Game.Map = mapCreation.CreateMap(Game.Player);


                DidPlayerAct = true;
                string mapName = Game.Map.MapLocation.Planet.ToString();
                ChangeTitle($"{mapName} - Boss Room");

            }
        }

        /// <summary>
        /// Called when te player is on a teleportation portal and presses left Ctrl
        /// This method creates a new map according to the destination of the portal
        /// </summary>
        private void TeleportPlayer()
        {
            // Get the current teleportation portal 
            TeleportationPortal portal = Game.Map.GetTeleportationPortalAt(Game.Player.PosX, Game.Player.PosY);
            // If the destination of the portal is the spaceship, 
            // the player has beaten the boss and want to go back to the spaceship
            if (portal.DestinationMap == MapType.Spaceship)
            {
                Game.CurrentLevel = 1; //Set the game level to 1

            }
            // Create a map Creation with the portal's destination map type in argument 
            MapCreation mapCreation = new MapCreation(Dimensions.worldWidth, Dimensions.worldHeight, Game.CurrentLevel, Game.Player.ArtifactsCollected.Count, portal.DestinationMap, portal.PlanetDestination);

            // Change the game current map
            Game.Map = mapCreation.CreateMap(Game.Player);
            DidPlayerAct = true;
            string mapType = Game.Map.MapLocation.MapType.ToString();
            string title = (Game.Map.MapLocation.MapType == MapType.Spaceship) ? mapType : $"{Game.Map.MapLocation.Planet.ToString()} - Level {Game.CurrentLevel}";
            ChangeTitle(title);
        }

        /// <summary>
        /// Check if the player has died
        /// </summary>
        private void CheckPlayerDeath()
        {
            if (Game.Player.Health <= 0)
            {
                RootConsole.Update -= OnGameUpdate;
                RootConsole.Render -= OnGameRender;
                EndGame(Game.Player);
            }
        }

        /// <summary>
        /// Check if the player has won
        /// </summary>
        private void CheckPlayerWin()
        {
            if (Game.Player.ArtifactsCollected.Count == 3)
            {
                RootConsole.Update -= OnGameUpdate;
                RootConsole.Render -= OnGameRender;
                EndGame(Game.Player);
            }
        }




        /// <summary>
        /// Update the player orientation according to it's click position
        /// </summary>
        private void UpdateOrientation()
        {
            // Get the coordinate of teh click
            int mouseX = RootConsole.Mouse.X;
            int mouseY = RootConsole.Mouse.Y;

            // Check that the click is made in the root console
            if (mouseX >= 0 && mouseX < RootConsole.Width && mouseY >= 0 && mouseY < RootConsole.Height)
            {
                // Get the coordinates of the cell on the map according to the camera viewport
                int absoluteX = mouseX + CameraSystem.viewPortStartX;
                int absoluteY = mouseY + CameraSystem.viewPortStartY;

                // Get the difference between the cell and the player position
                double diffX = Math.Abs(absoluteX - Game.Player.PosX);
                double diffY = Math.Abs(absoluteY - Game.Player.PosY);

                // Compare the cell and the player position to deduce the player orientation
                // If the clickedd cell x position was greater than the player position, the click was on the right part of the screen
                if (absoluteX > Game.Player.PosX)
                {
                    // Deduce orientation with clickOnRight = true
                    DeduceOrientation(diffX, diffY, absoluteY, true);
                }
                // else, the click was on the left part of the screen
                else
                {
                    DeduceOrientation(diffX, diffY, absoluteY, false);
                }
            }
        }

        /// <summary>
        /// Deduce the player orientation
        /// </summary>
        /// <param name="diffX">The x difference between the clicked cell and the player position</param>
        /// <param name="diffY">The y difference between the clicked cell and the player position</param>
        /// <param name="absoluteY">The y coordinates of the clicked cell on the map according to the camera viewport</param>
        /// <param name="clickOnRight">A boolean that indicates if the click was on the left part of the screen, or on the right part</param>
        private void DeduceOrientation(double diffX, double diffY, int absoluteY, bool clickOnRight)
        {
            // If the x difference is grater than the y difference, the click was closer to the horizontal axis
            if (diffX > diffY)
            {
                DeduceHorizontalOrientation(clickOnRight);
            }
            else
            {
                DeduceVerticalOrientation(absoluteY);
            }
        }

        /// <summary>
        /// Deduce the horizontal orientation according to the boolean clickOnRiht
        /// </summary>
        /// <param name="clickOnRight">This boolean is true if the click was on the right part of the screen</param>
        private void DeduceHorizontalOrientation(bool clickOnRight)
        {
            if (!clickOnRight)
            {
                Game.Player.Symbol = Game.Player.LeftSymbol;
                Game.Player.Direction = Direction.Left;
            }
            else
            {
                Game.Player.Symbol = Game.Player.RightSymbol;
                Game.Player.Direction = Direction.Right;
            }
        }

        /// <summary>
        /// Deduce the vertical orientation according to absoluteY
        /// </summary>
        /// <param name="absoluteY">The y coordinates of the clicked cell on the map according to the camera viewport</param>
        private void DeduceVerticalOrientation(int absoluteY)
        {
            // if the click was above the player, orient the player upperward
            if (absoluteY < Game.Player.PosY)
            {
                Game.Player.Symbol = Game.Player.UpSymbol;
                Game.Player.Direction = Direction.Up;
            }
            // else, the click was below the player, orient the player downward
            else
            {
                Game.Player.Symbol = Game.Player.DownSymbol;
                Game.Player.Direction = Direction.Down;
            }
        }

        /// <summary>
        /// This method is called to create the win screen or the game over screen according to the player victory or not
        /// </summary>
        /// <param name="player">The player</param>
        private void EndGame(Player player)
        {
            if (player.ArtifactsCollected.Count == 3)
            {
                gameTime.Stop();
                WinScreen winScreen = new WinScreen(gameTime.Elapsed);
            }
            else
            {
                GameOverScreen gameOverScreen = new GameOverScreen();
            }
        }
    }
}