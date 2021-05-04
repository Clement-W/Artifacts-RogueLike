using RLNET;
using RogueLike.Core;
namespace RogueLike.Interfaces
{

    /// <summary>
    /// This interface is implemented by every element that are drawable on the map
    /// </summary>
    public interface IDrawable
    {

        /// <value> The color of the drawable that is printed on the screen </value>
        RLColor PrintedColor { get; set; }


        /// <value> The symbol of the drawable that is printed on the screen</value>
        char Symbol { get; set; }


        /// <value> The x position of the drawable on the map</value>
        int PosX { get; set; }

        /// <value> The y position of the drawable on the map</value>
        int PosY { get; set; }

        /// <summary>
        /// This method draws the drawable on the map
        /// </summary>
        /// <param name="console">The console that contains the map</param>
        /// <param name="map">The map that contains the drawable</param>
        void Draw(RLConsole console, CurrentMap map);

    }
}