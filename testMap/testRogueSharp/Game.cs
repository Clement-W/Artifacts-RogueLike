using System;
using RLNET;
using RogueSharp;

using testRogueSharp.Core;
using testRogueSharp.Systems;

namespace testRogueSharp
{
    class Game
    {
        // La console de la fenêtre
        private static readonly int screenWidth = 80;
        private static readonly int screenHeight = 35;
        private static RLRootConsole rootConsole; // console principale

        // La console de la map
        private static readonly int mapWidth = 64;
        private static readonly int mapHeight = 24;
        private static RLConsole mapConsole;

        // La console des messages
        private static readonly int messageWidth = 64;
        private static readonly int messageHeight = 5;
        private static RLConsole messageConsole;

        // La console des stats
        private static readonly int statWidth = 16;
        private static readonly int statHeight = 35;
        private static RLConsole statConsole;

        // La console de l'inventaire
        private static readonly int inventoryWidth = 64;
        private static readonly int inventoryHeight = 5;
        private static RLConsole inventoryConsole;


        private static bool renderRequired = true;
        // pour ne redessiner la map que si c'est necessaire

        public static Random Random { get; private set; }

        // Les éléments du jeu
        public static Player Player { get; set; }
        public static DungeonMap DungeonMap { get; private set; }
        public static CommandSystem CommandSystem { get; private set; }
        public static MessageLog MessageLog { get; private set; }

        public static SchedulingSystem SchedulingSystem { get; private set; }

        // Pour la selection à la souris
        private static bool _highlightWalls;


        static void Main(string[] args)
        {
            Random = new Random();

            SchedulingSystem = new SchedulingSystem();

            MapGenerator mapGenerator = new MapGenerator(mapWidth, mapHeight);

            DungeonMap = mapGenerator.CreateCaveMap(); // créé la map et créé et place le joueur


            DungeonMap.UpdatePlayerFieldOfView();

            CommandSystem = new CommandSystem();

            MessageLog = new MessageLog();
            MessageLog.Add("Rogue énorme");
            MessageLog.Add("Message de log");

            rootConsole = new RLRootConsole("terminal16x16.png", screenWidth, screenHeight, 16, 16, 1f, "Test rogueSharp");

            mapConsole = new RLConsole(mapWidth, mapHeight);
            messageConsole = new RLConsole(messageWidth, messageHeight);
            statConsole = new RLConsole(statWidth, statHeight);
            inventoryConsole = new RLConsole(inventoryWidth, inventoryHeight);


            rootConsole.Update += OnRootConsoleUpdate;
            // += permet d'ajouter un event hander à rootConsole.Update (qui est un event)
            // +=  appelle la méthode add de l'event
            rootConsole.Render += OnRootConsoleRender; // pareil avec le Render event

            inventoryConsole.SetBackColor(0, 0, inventoryWidth, inventoryHeight, Palette.DbWood);



            inventoryConsole.Print(1, 1, "Inventory", RLColor.White);

            // Puis on démarre le loop de RLNET
            rootConsole.Run();
        }

        // Event handler pour l'update event de rootConsole
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            bool didPlayerAct = false;
            RLKeyPress keyPress = rootConsole.Keyboard.GetKeyPress();
            
            if (CommandSystem.IsPlayerTurn)
            {
                if (keyPress != null)
                {
                    switch (keyPress.Key)
                    {
                        case RLKey.Up: didPlayerAct = CommandSystem.MovePlayer(Direction.Up); break;
                        case RLKey.Down: didPlayerAct = CommandSystem.MovePlayer(Direction.Down); break;
                        case RLKey.Left: didPlayerAct = CommandSystem.MovePlayer(Direction.Left); break;
                        case RLKey.Right: didPlayerAct = CommandSystem.MovePlayer(Direction.Right); break;
                        case RLKey.Escape: rootConsole.Close(); break;
                    }
                    if (rootConsole.Mouse.GetLeftClick()) {
                        highlightWalls = !highlightWalls;
                    }
                }
                if (didPlayerAct)
                {
                    renderRequired = true;
                    CommandSystem.EndPlayerTurn();
                    //Console.WriteLine(SchedulingSystem);

                }

            } else{
                CommandSystem.ActivateMonsters();
                renderRequired=true;
            }


        }

        // Event handler pour le render event de rootConsole
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {

            if (true) // à remplacer par renderRequired
            {
                mapConsole.Clear();
                statConsole.Clear();
                messageConsole.Clear();

                //On fait le render de la map
                DungeonMap.Draw(mapConsole, statConsole);
                Player.Draw(mapConsole, DungeonMap);
                Player.DrawStats(statConsole);
                MessageLog.Draw(messageConsole);

                // On place les 4 consoles dans la rootConsole
                //Blit(RLConsole srcConsole, int srcX, int srcY, int srcWidth, int srcHeight, RLConsole destConsole, int destX, int destY)
                RLConsole.Blit(mapConsole, 0, 0, mapWidth, mapHeight, rootConsole, 0, inventoryHeight);
                RLConsole.Blit(statConsole, 0, 0, statWidth, statHeight, rootConsole, mapWidth, 0);
                RLConsole.Blit(messageConsole, 0, 0, messageWidth, messageHeight, rootConsole, 0, screenHeight - messageHeight);
                RLConsole.Blit(inventoryConsole, 0, 0, inventoryWidth, inventoryHeight, rootConsole, 0, 0);

                rootConsole.Draw();

                renderRequired = false;
            }
        }
    }
}
