using System;
using RLNET;
using RogueLike.View;
using RogueLike.Systems;
using System.Threading;


namespace RogueLike.Core
{
    public class Game
    {




        public Player Player { get; private set; }
        public CameraSystem CameraSystem { get; private set; }
        public CommandSystem CommandSystem { get; set; }

        public static MessageLog MessageLog { get; set; }

        public CurrentMap Map { get; set; }

        public static int CurrentLevel { get; set; } // the level in the map 


        public void StartGame()
        {
            CurrentLevel = 1;
            GameScreen gameScreen = new GameScreen("oui", this);

            CommandSystem = new CommandSystem();
            CameraSystem = new CameraSystem();
            MessageLog = new MessageLog();
            MessageLog.AddMessage("Let's go !");
            Player = new Player();
            InitializeMap();

            // créé la map vaisseau
            // dans le vaisseau il y a les portails, quand on va dans un portail, ça TP dans la map donc ça set l'attribut currentMapType et level


            //créé command system, la map,...
            //créé le player et le passe en paramètre au map generator
        }

        private void InitializeMap()
        {
            MapGenerator mapGenerator = new MapGenerator(Dimensions.worldWidth, Dimensions.worldHeight, CurrentLevel, Player.ArtifactsCollected.Count,MapType.Spaceship);
           
            Map = mapGenerator.CreateMap(Player);
            //Spaceship.UpdatePlayerFieldOfView(Player);
        }






    }
}
