using System;
using RogueLike.Core;
using RogueLike.View;

namespace RogueLike{

    public class Program{

        static void Main(string[] args)
        {
            Game rogueLikeGame = new Game();
            LaunchScreen launchScreen = new LaunchScreen(rogueLikeGame);
            

        }
    }
}