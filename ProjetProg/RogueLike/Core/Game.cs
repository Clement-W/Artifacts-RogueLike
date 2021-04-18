using System;
using RLNET;
using RogueLike.View;


namespace RogueLike.Core
{
    public class Game
    {

        private static int difficultyLevel = 1;


        public static Random random{get;private set;}


        public void StartGame(){
            GameScreen gameScreen = new GameScreen("oui",this);

        }

        

        
    }
}
