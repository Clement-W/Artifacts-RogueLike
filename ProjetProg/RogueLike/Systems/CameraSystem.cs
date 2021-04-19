using RogueLike.Core;
namespace RogueLike.Systems
{

    public class CameraSystem
    // http://www.roguebasin.com/index.php?title=Scrolling_map 
    {
        public static int viewPortStartX = 0;
        public static int viewPortStartY = 0;


        public void CenterCamera(Player player)
        {

            // Adapt the camera view port according to the player position
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
                // Center the camera on the plsayer
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