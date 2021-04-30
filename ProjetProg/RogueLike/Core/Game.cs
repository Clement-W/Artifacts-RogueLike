using System;
using RLNET;
using RogueLike.View;
using RogueLike.Systems;
using System.Threading;


namespace RogueLike.Core
{
    public class Game
    {

        public static int Level { get; set; }


        public Player Player { get; private set; }
        public CameraSystem CameraSystem { get; private set; }
        public CommandSystem CommandSystem { get; set; }

        public static MessageLog MessageLog { get; set; }

        public CurrentMap Map { get; set; }


        public int NbArtefactsCollected{get;set;}




        public void StartGame()
        {
            Level = 1;
            NbArtefactsCollected = 0;
            GameScreen gameScreen = new GameScreen("oui", this);

            CommandSystem = new CommandSystem();
            CameraSystem = new CameraSystem();
            MessageLog = new MessageLog();
            MessageLog.AddMessage("C'est parti !");

            MapGenerator mapGenerator = new MapGenerator(Dimensions.worldWidth, Dimensions.worldHeight, Level,NbArtefactsCollected);
            Player = new Player();
            Map = mapGenerator.CreateCaveMap(Player);
            Map.UpdatePlayerFieldOfView(Player);


            







            //créé command system, la map,...
            //créé le player et le passe en paramètre au map generator
        }




    }
}
