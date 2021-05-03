using RogueLike.Core;
using RLNET;
using RogueLike.Systems;
using System.Diagnostics;
using System;
using RogueSharp;
using System.Threading;
using RogueLike.Interfaces;


namespace RogueLike.View
{

    public class GameScreen : ScreenView
    {


        private static RLConsole mapConsole;
        private static RLConsole messageConsole;
        private static RLConsole statConsole;

        private static RLConsole equipmentsConsole; //armor, weapons

        private static RLConsole itemsConsole; //food, potions,...


        

        


        public GameScreen(string title, Game game) : base(title, game)
        {
            // Initialize the sub consoles of the root console
            mapConsole = new RLConsole(Dimensions.worldWidth, Dimensions.worldHeight);  //mapConsole needs to be as big as the world, but only a part of it will be rendered
            messageConsole = new RLConsole(Dimensions.messageConsoleWidth, Dimensions.messageConsoleHeight);
            statConsole = new RLConsole(Dimensions.statConsoleWidth, Dimensions.statConsoleHeight);
            equipmentsConsole = new RLConsole(Dimensions.equipmentsConsoleWidth, Dimensions.equipmentsConsoleHeight);
            itemsConsole = new RLConsole(Dimensions.itemsConsoleWidth, Dimensions.itemsConsoleHeight);
            ChangeTitle("Spaceship");

            RootConsole.Update += OnGameUpdate;
            RootConsole.Render += OnGameRender;

        }



        private void OnGameRender(object sender, UpdateEventArgs e)
        {

            if (RenderRequired)
            {
                Game.AnimationSystem.AnimateAnimatedElements(Game);

                mapConsole.Clear();
                statConsole.Clear();
                messageConsole.Clear();
                equipmentsConsole.Clear();
                itemsConsole.Clear();

                Game.Map.Draw(mapConsole, statConsole); // We need the stat console to add the monster lifebar on it if there's a monster nearby
                Game.Player.Draw(mapConsole, Game.Map);
                Game.Player.DrawStats(statConsole);
                Game.Player.DrawEquipmentInventory(equipmentsConsole);
                Game.Player.DrawItemsInventory(itemsConsole);
                Core.Game.MessageLog.Draw(messageConsole);
                Game.CameraSystem.CenterCamera(Game.Player);
                //draw inventory

                /*statConsole.SetBackColor( 0, 0, Dimensions.statConsoleWidth, Dimensions.statConsoleHeight, RLColor.Green );
                equipmentsConsole.SetBackColor( 0, 0, Dimensions.equipmentsConsoleWidth, Dimensions.equipmentsConsoleHeight, RLColor.Yellow );
                itemsConsole.SetBackColor( 0, 0, Dimensions.itemsConsoleWidth, Dimensions.itemsConsoleHeight, RLColor.Gray );
                */



                // Blit the consoles in the root console
                RLConsole.Blit(mapConsole, CameraSystem.viewPortStartX, CameraSystem.viewPortStartY, Dimensions.mapConsoleWidth, Dimensions.mapConsoleHeight, RootConsole, 0, 0);
                RLConsole.Blit(statConsole, 0, 0, Dimensions.statConsoleWidth, Dimensions.statConsoleHeight, RootConsole, Dimensions.equipmentsConsoleWidth, Dimensions.mapConsoleHeight);
                RLConsole.Blit(messageConsole, 0, 0, Dimensions.messageConsoleWidth, Dimensions.messageConsoleHeight, RootConsole, Dimensions.equipmentsConsoleWidth, Dimensions.mapConsoleHeight + Dimensions.statConsoleHeight);
                RLConsole.Blit(equipmentsConsole, 0, 0, Dimensions.equipmentsConsoleWidth, Dimensions.equipmentsConsoleHeight, RootConsole, 0, Dimensions.mapConsoleHeight);
                RLConsole.Blit(itemsConsole, 0, 0, Dimensions.itemsConsoleWidth, Dimensions.itemsConsoleHeight, RootConsole, Dimensions.equipmentsConsoleWidth + Dimensions.messageConsoleWidth, Dimensions.mapConsoleHeight);



                RootConsole.Draw();
                RenderRequired = false;

            }
        }

        private void OnGameUpdate(object sender, UpdateEventArgs e)
        {
            //UpdateOrientation(); // pour le changement d'orientation avec la souris en continu, enlever peut-�tre (TODO)

            Game.SchedulingSystem.CheckSchedule(Game);


            DidPlayerAct = false;
            KeyPress = RootConsole.Keyboard.GetKeyPress();

            if (RootConsole.Mouse.GetLeftClick())
            {
                UpdateOrientation(); // The player looks on the click direction
                Game.CommandSystem.PlayerAttack(Game.Player, Game.Map); // Attack the cells
                RenderRequired = true;
            }

            if (KeyPress != null)
            {

                switch (KeyPress.Key)
                {
                    case RLKey.Escape: RootConsole.Close(); break;
                    case RLKey.W: DidPlayerAct = Game.CommandSystem.MovePlayer(Game.Player, Direction.Up, Game.Map); break;
                    case RLKey.S: DidPlayerAct = Game.CommandSystem.MovePlayer(Game.Player, Direction.Down, Game.Map); break;
                    case RLKey.A: DidPlayerAct = Game.CommandSystem.MovePlayer(Game.Player, Direction.Left, Game.Map); break;
                    case RLKey.D: DidPlayerAct = Game.CommandSystem.MovePlayer(Game.Player, Direction.Right, Game.Map); break;
                    case RLKey.LControl:
                        if (Game.Map.PlayerIsOnStairCase(Game.Player))
                        {

                            if (Game.CurrentLevel < 1)
                            {
                                MapGenerator mapGenerator = new MapGenerator(Dimensions.worldWidth, Dimensions.worldHeight, ++Game.CurrentLevel, Game.Player.ArtifactsCollected.Count, Game.Map.Location.MapType, Game.Map.Location.Planet);
                                // Increase game current level
                                Game.Map = mapGenerator.CreateMap(Game.Player);
                                

                                DidPlayerAct = true;
                                string mapName = Game.Map.Location.Planet.ToString();
                                ChangeTitle($"{mapName} - Level {Game.CurrentLevel}");
                            }
                            else
                            { // Create the boss room
                                MapGenerator mapGenerator = new MapGenerator(Dimensions.worldWidth, Dimensions.worldHeight, ++Game.CurrentLevel, Game.Player.ArtifactsCollected.Count, MapType.BossRoom, Game.Map.Location.Planet);
                                Game.Map = mapGenerator.CreateMap(Game.Player);
                                

                                DidPlayerAct = true;
                                string mapName = Game.Map.Location.Planet.ToString();
                                ChangeTitle($"{mapName} - Boss Room");

                            }
                        }
                        else if (Game.Map.PlayerIsOnTeleportationPortal(Game.Player))
                        {
                            TeleportationPortal portal = Game.Map.GetTeleportationPortalAt(Game.Player.PosX, Game.Player.PosY);
                            if (portal.DestinationMap == MapType.Spaceship) // The player has beaten the boss and want to go back to the spaceship
                            {
                                Game.CurrentLevel = 1;

                            }
                            MapGenerator mapGenerator = new MapGenerator(Dimensions.worldWidth, Dimensions.worldHeight, Game.CurrentLevel, Game.Player.ArtifactsCollected.Count, portal.DestinationMap, portal.PlanetDestination);
                            // Create a map generator with the portal destination map type in argument
                            Game.Map = mapGenerator.CreateMap(Game.Player);
                            DidPlayerAct = true;
                            string mapType = Game.Map.Location.MapType.ToString();
                            string title = (Game.Map.Location.MapType == MapType.Spaceship) ? mapType : $"{Game.Map.Location.Planet.ToString()} - Level {Game.CurrentLevel}";
                            ChangeTitle(title);
                        }




                        break;

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


            // If the player die, switch to the game over screen
            if (Game.Player.Health <= 0)
            {
                RootConsole.Update -= OnGameUpdate;
                RootConsole.Render -= OnGameRender;
                EndGame(Game);
            }

            if (Game.Player.ArtifactsCollected.Count == 3)
            {
                RootConsole.Update -= OnGameUpdate;
                RootConsole.Render -= OnGameRender;
                EndGame(Game);
            }
        }

        private static Cell SelectCell(int x, int y)
        {

            Cell selectedCell;
            //possibilit� de faire un switch pour changer la forme de la zone selectionn�
            //pour l'instant on focus une case TODO


            if (x >= 0 && x < Game.Map.Width && y >= 0 && y < Game.Map.Height)
            {

                selectedCell = Game.Map.CellFor(Game.Map.IndexFor(x, y)) as Cell;

            }
            else
            {
                return (Cell)Game.Map.GetCell(Game.Player.PosX, Game.Player.PosY);
            }

            return selectedCell;

        }


        private void UpdateOrientation()
        {

            int mouseX = RootConsole.Mouse.X;
            int mouseY = RootConsole.Mouse.Y;

            if (mouseX >= 0 && mouseX < RootConsole.Width && mouseY >= 0 && mouseY < RootConsole.Height)
            {

                int absoluteX = mouseX + CameraSystem.viewPortStartX;
                int absoluteY = mouseY + CameraSystem.viewPortStartY;

                Cell currentMouseCell = SelectCell(absoluteX, absoluteY);

                int x = currentMouseCell.X;
                int y = currentMouseCell.Y;


                double diffX = Math.Abs(x - Game.Player.PosX);
                double diffY = Math.Abs(y - Game.Player.PosY);

                if (x > Game.Player.PosX)
                {
                    if (diffX > diffY)
                    {
                        Game.Player.Symbol = Game.Player.RightSymbol;
                        Game.Player.Direction = Direction.Right;
                    }
                    else
                    {
                        if (y < Game.Player.PosY)
                        {
                            Game.Player.Symbol = Game.Player.UpSymbol;
                            Game.Player.Direction = Direction.Up;
                        }
                        else
                        {
                            Game.Player.Symbol = Game.Player.DownSymbol;
                            Game.Player.Direction = Direction.Down;
                        }
                    }
                }
                else
                {
                    if (diffX > diffY)
                    {
                        Game.Player.Symbol = Game.Player.LeftSymbol;
                        Game.Player.Direction = Direction.Left;
                    }
                    else
                    {
                        if (y < Game.Player.PosY)
                        {
                            Game.Player.Symbol = Game.Player.UpSymbol;
                            Game.Player.Direction = Direction.Up;
                        }
                        else
                        {
                            Game.Player.Symbol = Game.Player.DownSymbol;
                            Game.Player.Direction = Direction.Down;
                        }
                    }
                }
            }
        }

        public void EndGame(Game game)
        {
            if (game.Player.ArtifactsCollected.Count == 3)
            {
                WinScreen winScreen= new WinScreen(game);
            }
            else
            {
                GameOverScreen gameOverScreen = new GameOverScreen(game);
            }

        }


        



    }
}