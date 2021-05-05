using RogueLike.Core;
namespace RogueLike.Systems
{

    /// <summary>
    /// This class manages the camera to create a scrolling map when the player moves
    /// A scrolling map is a map that moves with the player always in the center of the screen, except when near the end of the map.
    /// </summary>
    public class CameraSystem
    {

        /// <summary>
        /// The x coordinate from which the camera view port starts (the cell column on the left of the screen)
        /// </summary>
        public static int viewPortStartX = 0;

        /// <summary>
        /// The y coordinate from which the camera view port starts (the cell line on the top of the screen)
        /// </summary>
        public static int viewPortStartY = 0;


        /// <summary>
        /// This method centers the camera on the player
        /// </summary>
        /// <param name="player">The player instance on the map</param>
        public void CenterCamera(Player player)
        {

            // Adapt the camera view port according to the player position
            if (player.PosX < Dimensions.mapConsoleWidth / 2) // If player on the left extremity of the map
            {
                // Camera x starts at zero to simulate hitting the left border of the console
                viewPortStartX = 0; 
            }
            else if (player.PosX >= Dimensions.worldWidth - (Dimensions.mapConsoleWidth / 2)) // If player on the right extremity of the map
            {
                // Camera x starts at the right place to simulate hitting the right border of the console
                viewPortStartX = Dimensions.worldWidth - Dimensions.mapConsoleWidth;  
            }
            else // If player in the middle of the map
            {
                // Center the camera x on the player
                viewPortStartX = player.PosX - (Dimensions.mapConsoleWidth / 2);
            }

            if (player.PosY <= Dimensions.mapConsoleHeight / 2) // If player on the top extremity of the map
            {
                // Camera y starts at zero to simulate hitting the top border of the console
                viewPortStartY = 0;
            }
            else if (player.PosY > Dimensions.worldHeight - (Dimensions.mapConsoleHeight / 2)) // If player on the bottom extremity of the map
            {
                // Camera y starts at the right place to simulate hitting the bottom border of the console
                viewPortStartY = Dimensions.worldHeight - Dimensions.mapConsoleHeight;
            }
            else // If player in the middle of the map
            {
                // Center the camera y on the player
                viewPortStartY = player.PosY - (Dimensions.mapConsoleHeight / 2);
            }
        }
    }
}