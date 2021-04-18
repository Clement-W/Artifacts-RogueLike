using System;
using RLNET;
using RogueLike.View;
using RogueLike.Systems;


namespace RogueLike.Core
{
    public class Game
    {

        private static int difficultyLevel = 1;


        public Player Player{get; private set;}

        public MapGenerator MapGenerator{get; private set;}
        public CameraSystem CameraSystem{get;private set;}

        public CurrentMap Map{get;set;}

        


        public void StartGame(){
            GameScreen gameScreen = new GameScreen("oui",this);

            MapGenerator = new MapGenerator(Dimensions.worldWidth,Dimensions.worldHeight);
            Player = new Player();
            Map = MapGenerator.CreateCaveMap(Player);
            Map.UpdatePlayerFieldOfView(Player);

            

            

            //créé command system, la map,...
            //créé le player et le passe en paramètre au map generator
        }

        

        
    }
}
