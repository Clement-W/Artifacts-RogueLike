using testRogueSharp.Core;

namespace testRogueSharp.Systems
{

    public class CameraSystem
    {
        public static int viewPortStartX = 0;
        public static int viewPortStartY = 0;


        public static void ReCenterCamera()
        {

            //centre la caméra sur le joueur
            viewPortStartX = Game.Player.X - (Game.mapWidth / 2);
            viewPortStartY = Game.Player.Y - (Game.mapHeight / 2);
            
            //on check le min et le max du camera viewport
            if (Game.Player.X <= Game.mapWidth / 2)
            {
                viewPortStartX = 0;
            }
            else if (Game.Player.X > Game.worldWidth - (Game.mapWidth / 2))
            {
                viewPortStartX = Game.worldWidth - Game.mapWidth;
            }

            if (Game.Player.Y <= Game.mapHeight / 2)
            {
                viewPortStartY = 0;
            }
            else if (Game.Player.Y > Game.worldHeight - (Game.mapHeight / 2))
            {
                viewPortStartY = Game.worldHeight - Game.mapHeight;
            }
        }
    }
}