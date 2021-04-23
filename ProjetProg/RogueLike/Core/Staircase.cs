using RogueLike.Interfaces;
using RLNET;
using RogueSharp;

namespace RogueLike.Core
{
    public class Staircase : IDrawable
    {

        public RLColor PrintedColor { get; set; }

        public char Symbol{get;set;} // Based on the value of the IsUpstairs property

        public int PosX{get;set;}

        public int PosY{get;set;}

        public Staircase(int posX, int posY){
            PosX = posX;
            PosY = posY;
            Symbol = '>';
        }

        public void Draw(RLConsole console, IMap map){
            if(map.GetCell(PosX,PosY).IsExplored){ // Draw it only if the cell has been explored by the player

                PrintedColor = map.IsInFov(PosX,PosY) ? Colors.StairsFov : Colors.Stairs;

                console.Set(PosX,PosY,PrintedColor,null,Symbol);
            }
        }

    }
}