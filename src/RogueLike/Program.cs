using RogueLike.Core;
using RogueLike.View;

namespace RogueLike
{

    /// <summary>
    /// This class contains the entry point of the project
    /// </summary>
    public class Program
    {

        /// <summary>
        /// The entry point of the project
        /// </summary>
        /// <param name="args">Command line arguments (not used here)</param>
        static void Main(string[] args)
        {
            Game rogueLikeGame = new Game(); // Create a game instance
            LaunchScreen launchScreen = new LaunchScreen(rogueLikeGame); // Create the launch screen of the game
        }
    }
}