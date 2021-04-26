using RogueLike.Core;
using RLNET;
using RogueLike.Systems;
using System.Diagnostics;


namespace RogueLike.View
{

    public class GameScreen : ScreenView
    {

        //TODO refaire l'écran comme sur le screen de jb  (modifier directions.cs et le blit)


        private static RLConsole mapConsole;
        private static RLConsole messageConsole;
        private static RLConsole statConsole;

        private static RLConsole equipmentsConsole; //armor, weapons

        private static RLConsole itemsConsole; //food, potions,...


        private static Stopwatch stopwatch;


        public GameScreen(string title, Game game) : base(title, game)
        {
            // Initialize the sub consoles of the root console
            mapConsole = new RLConsole(Dimensions.worldWidth, Dimensions.worldHeight);  //mapConsole needs to be as big as the world, but only a part of it will be rendered
            messageConsole = new RLConsole(Dimensions.messageConsoleWidth, Dimensions.messageConsoleHeight);
            statConsole = new RLConsole(Dimensions.statConsoleWidth, Dimensions.statConsoleHeight);
            equipmentsConsole = new RLConsole(Dimensions.equipmentsConsoleWidth, Dimensions.equipmentsConsoleHeight);
            itemsConsole = new RLConsole(Dimensions.itemsConsoleWidth, Dimensions.itemsConsoleHeight);
            Console.Title = $"Nom du Rogue Like - Level {Game.DifficultyLevel}";

            Console.Update += OnGameUpdate;
            Console.Render += OnGameRender;

            stopwatch = new Stopwatch();
            stopwatch.Start();
        }



        private void OnGameRender(object sender, UpdateEventArgs e)
        {

            if (RenderRequired)
            {
                mapConsole.Clear();
                statConsole.Clear();
                messageConsole.Clear();
                equipmentsConsole.Clear();
                itemsConsole.Clear();


                Game.Map.Draw(mapConsole, statConsole); // We need the stat console to add the monster lifebar on it if there's a monster nearby
                Game.Player.Draw(mapConsole, Game.Map);
                Game.Player.DrawStats(statConsole);
                Game.Player.DrawEquipmentInventory(equipmentsConsole);
                Game.MessageLog.Draw(messageConsole);
                Game.CameraSystem.CenterCamera(Game.Player);
                //draw inventory

                /*statConsole.SetBackColor( 0, 0, Dimensions.statConsoleWidth, Dimensions.statConsoleHeight, RLColor.Green );
                equipmentsConsole.SetBackColor( 0, 0, Dimensions.equipmentsConsoleWidth, Dimensions.equipmentsConsoleHeight, RLColor.Yellow );
                itemsConsole.SetBackColor( 0, 0, Dimensions.itemsConsoleWidth, Dimensions.itemsConsoleHeight, RLColor.Gray );
                */


                // Blit the consoles in the root console
                RLConsole.Blit(mapConsole, CameraSystem.viewPortStartX, CameraSystem.viewPortStartY, Dimensions.mapConsoleWidth, Dimensions.mapConsoleHeight, Console, 0, 0);
                RLConsole.Blit(statConsole, 0, 0, Dimensions.statConsoleWidth, Dimensions.statConsoleHeight, Console, Dimensions.equipmentsConsoleWidth, Dimensions.mapConsoleHeight);
                RLConsole.Blit(messageConsole, 0, 0, Dimensions.messageConsoleWidth, Dimensions.messageConsoleHeight, Console, Dimensions.equipmentsConsoleWidth, Dimensions.mapConsoleHeight + Dimensions.statConsoleHeight);
                RLConsole.Blit(equipmentsConsole, 0, 0, Dimensions.equipmentsConsoleWidth, Dimensions.equipmentsConsoleHeight, Console, 0, Dimensions.mapConsoleHeight);
                RLConsole.Blit(itemsConsole, 0, 0, Dimensions.itemsConsoleWidth, Dimensions.itemsConsoleHeight, Console, Dimensions.equipmentsConsoleWidth + Dimensions.messageConsoleWidth, Dimensions.mapConsoleHeight);



                Console.Draw();
                RenderRequired = false;

            }
        }

        private void OnGameUpdate(object sender, UpdateEventArgs e)
        {

            
            if (stopwatch.ElapsedMilliseconds > 1000)
            {
            
                stopwatch.Reset();
                Game.CommandSystem.MoveEnemies(Game);
                RenderRequired=true;
                stopwatch.Start();
            }

            DidPlayerAct = false;
            KeyPress = Console.Keyboard.GetKeyPress();

            if (KeyPress != null)
            {

                switch (KeyPress.Key)
                {
                    case RLKey.Escape: Console.Close(); break;
                    case RLKey.Up: DidPlayerAct = Game.CommandSystem.MovePlayer(Game.Player, Direction.Up, Game.Map); break;
                    case RLKey.Down: DidPlayerAct = Game.CommandSystem.MovePlayer(Game.Player, Direction.Down, Game.Map); break;
                    case RLKey.Left: DidPlayerAct = Game.CommandSystem.MovePlayer(Game.Player, Direction.Left, Game.Map); break;
                    case RLKey.Right: DidPlayerAct = Game.CommandSystem.MovePlayer(Game.Player, Direction.Right, Game.Map); break;
                    case RLKey.LControl:
                        if (Game.Map.CanMoveToNextLevel(Game.Player))
                        {
                            MapGenerator mapGenerator = new MapGenerator(Dimensions.worldWidth, Dimensions.worldHeight, ++Game.DifficultyLevel);
                            Game.Map = mapGenerator.CreateCaveMap(Game.Player);
                            Game.MessageLog = new MessageLog();
                            Game.CommandSystem = new CommandSystem();
                            DidPlayerAct = true;
                            Console.Title = $"Nom du Rogue Like - Level {Game.DifficultyLevel}";
                        }
                        break;
                }

            }

            if (DidPlayerAct == true)
            {
                RenderRequired = true;
                // Provisoire avant qu'on fasse un scheduling system
                //Game.CommandSystem.MoveEnemies(Game);
            }
        }






    }
}