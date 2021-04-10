using System;
using RLNET;
using testRogueSharp.Core;
using testRogueSharp.Systems;

namespace testRogueSharp
{
    class Game
    {
        // La console de la fenêtre
        private static readonly int screenWidth = 100;
        private static readonly int screenHeight = 70;
        private static RLRootConsole rootConsole; // console principale

        // La console de la map
        private static readonly int mapWidth = 80;
        private static readonly int mapHeight = 48;
        private static RLConsole mapConsole;

        // La console des messages
        private static readonly int messageWidth = 80;
        private static readonly int messageHeight = 11;
        private static RLConsole messageConsole;

        // La console des stats
        private static readonly int statWidth = 20;
        private static readonly int statHeight = 70;
        private static RLConsole statConsole;

        // La console de l'inventaire
        private static readonly int inventoryWidth = 80;
        private static readonly int inventoryHeight = 11;
        private static RLConsole inventoryConsole;

        public static Player Player { get; private set; }
        public static DungeonMap DungeonMap { get; private set; }


        static void Main(string[] args)
        {

            MapGenerator mapGenerator = new MapGenerator(mapWidth, mapHeight);
            DungeonMap = mapGenerator.CreateMap();
            Player = new Player();
            DungeonMap.UpdatePlayerFieldOfView();

            rootConsole = new RLRootConsole("terminal8x8.png", screenWidth, screenHeight, 8, 8, 1f, "Test rogueSharp");

            mapConsole = new RLConsole(mapWidth, mapHeight);
            messageConsole = new RLConsole(messageWidth, messageHeight);
            statConsole = new RLConsole(statWidth, statHeight);
            inventoryConsole = new RLConsole(inventoryWidth, inventoryHeight);


            rootConsole.Update += OnRootConsoleUpdate;
            // += permet d'ajouter un event hander à rootConsole.Update (qui est un event)
            // +=  appelle la méthode add de l'event
            rootConsole.Render += OnRootConsoleRender; // pareil avec le Render event

            //on affiche des trucs sur chaque console pour voir si ça marche bien
            //On met de la couleur sur toutes les consoles
            mapConsole.SetBackColor(0, 0, mapWidth, mapHeight, Colors.FloorBackground);
            messageConsole.SetBackColor(0, 0, messageWidth, messageHeight, Palette.DbDeepWater);
            statConsole.SetBackColor(0, 0, statWidth, statHeight, Palette.DbOldStone);
            inventoryConsole.SetBackColor(0, 0, inventoryWidth, inventoryHeight, Palette.DbWood);

            messageConsole.Print(1, 1, "Messages", RLColor.White);
            statConsole.Print(1, 1, "Stats", RLColor.White);
            inventoryConsole.Print(1, 1, "Inventory", RLColor.White);

            // Puis on démarre le loop de RLNET
            rootConsole.Run();
        }

        // Event handler pour l'update event de rootConsole
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {


        }

        // Event handler pour le render event de rootConsole
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {

            //On fait le render de la map
            DungeonMap.Draw(mapConsole);
            Player.Draw(mapConsole, DungeonMap);


            // On place les 4 consoles dans la rootConsole
            //Blit(RLConsole srcConsole, int srcX, int srcY, int srcWidth, int srcHeight, RLConsole destConsole, int destX, int destY)
            RLConsole.Blit(mapConsole, 0, 0, mapWidth, mapHeight, rootConsole, 0, inventoryHeight);
            RLConsole.Blit(statConsole, 0, 0, statWidth, statHeight, rootConsole, mapWidth, 0);
            RLConsole.Blit(messageConsole, 0, 0, messageWidth, messageHeight, rootConsole, 0, screenHeight - messageHeight);
            RLConsole.Blit(inventoryConsole, 0, 0, inventoryWidth, inventoryHeight, rootConsole, 0, 0);



            rootConsole.Draw();
        }
    }
}
