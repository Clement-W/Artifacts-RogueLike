using System;
using RogueLike.Core;
using RogueLike.View;

namespace RogueLike
{

    /// <summary>
    /// This is the entry point of the project
    /// </summary>
    public class Program
    {

        static void Main(string[] args)
        {
            Game rogueLikeGame = new Game(); // Create a game instance
            LaunchScreen launchScreen = new LaunchScreen(rogueLikeGame); // Create the launch screen of the game


        }
    }
}