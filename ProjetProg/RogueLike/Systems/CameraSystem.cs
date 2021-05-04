using RogueLike.Core;
namespace RogueLike.Systems
{

    /// <summary>
    /// This class deal with the camera to create a scrolling map when the player moves
    /// A scrolling map is a map that moves so that the player is always in the center of the screen
    /// </summary>
    public class CameraSystem
    {

        /// <summary>
        /// The x coordinate from which the camera view port starts
        /// </summary>
        public static int viewPortStartX = 0;

        /// <summary>
        /// The y coordinate from which the camera view port starts
        /// </summary>
        public static int viewPortStartY = 0;


        /// <summary>
        /// This method allows to center the camera on the player
        /// </summary>
        /// <param name="player">The player instance that is on the map</param>
        public void CenterCamera(Player player)
        {

            // Adapt the camera view port according to the player position
            //TODO: faire de meilleurs commentaires
            if (player.PosX < Dimensions.mapConsoleWidth / 2) 
            {
                viewPortStartX = 0;
            }
            else if (player.PosX >= Dimensions.worldWidth - (Dimensions.mapConsoleWidth / 2))
            {
                viewPortStartX = Dimensions.worldWidth - Dimensions.mapConsoleWidth;
            }
            else
            {
                // Center the camera on the player
                viewPortStartX = player.PosX - (Dimensions.mapConsoleWidth / 2);
            }

            if (player.PosY <= Dimensions.mapConsoleHeight / 2)
            {
                viewPortStartY = 0;
            }
            else if (player.PosY > Dimensions.worldHeight - (Dimensions.mapConsoleHeight / 2))
            {
                viewPortStartY = Dimensions.worldHeight - Dimensions.mapConsoleHeight;
            }
            else
            {
                viewPortStartY = player.PosY - (Dimensions.mapConsoleHeight / 2);
            }
        }
    }
}