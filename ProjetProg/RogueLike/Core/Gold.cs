using RogueLike.Interfaces;
using RLNET;
using RogueSharp;

namespace RogueLike.Core
{
    public class Gold : ILoot, IDrawable
    {
        public int PosX{get;set;}
        public int PosY{get;set;}

        public char Symbol{get;set;}

        public RLColor PrintedColor{get;set;}

        public int Amount{get;set;}

        public string Name{get;set;}

        public Gold(int amount,int posX, int posY){
            Symbol='$';
            PrintedColor = Colors.Gold;
            Amount = amount;
            PosX = posX;
            PosY = posY;
            Name="Gold";
        }


        public void Draw(RLConsole console, CurrentMap map)
        {
            // If it has never been explored, don't draw it
            if (map.GetCell(PosX, PosY).IsExplored)
            {

                // draw it differently if it's the fov or not
                if (map.IsInFov(PosX, PosY))
                {
                    // Draw it with the floor fov background color
                    console.Set(PosX, PosY, PrintedColor, map.Location.FloorBackgroundColorInFov, Symbol);
                }
                else
                {
                    // Draw it with the floor background and a '.' symbol
                    console.Set(PosX, PosY, PrintedColor, map.Location.FloorBackgroundColor, '.');
                }
            }
        }
    }
}