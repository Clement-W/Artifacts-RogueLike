using RLNET;
using RogueLike.Core;
namespace RogueLike.Interfaces
{

    public interface IDrawable
    {
        RLColor PrintedColor { get; set; }

        char Symbol { get; set; }

        int PosX { get; set; }

        int PosY { get; set; }

        void Draw(RLConsole console, CurrentMap map);

    }
}