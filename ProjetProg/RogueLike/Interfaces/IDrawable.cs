using RLNET;
using RogueSharp;

namespace RogueLike.Interfaces
{

    public interface IDrawable
    {
        RLColor Color { get; set; }

        char Symbol{get;set;}

        int PosX{get;set;}

        int PosY{get;set;}

        void Draw(RLConsole console, IMap map);

    }  
}