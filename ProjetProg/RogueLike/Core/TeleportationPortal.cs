using RogueLike.Interfaces;
using RLNET;
using RogueSharp;
namespace RogueLike.Core
{
    public abstract class TeleportationPortal : IDrawable
    {
        public MapType DestinationMap { get; set; }

        public PlanetName PlanetDestination{get;set;}

        public RLColor PrintedColor { get; set; }

        public char Symbol { get; set; } // The displayed symbol on the console

        public int PosX { get; set; }

        public int PosY { get; set; }

        public TeleportationPortal(){
            PrintedColor = Colors.TeleportationPortal;
        }

        public void Draw(RLConsole console, IMap map){
            if(map.GetCell(PosX,PosY).IsExplored){ // Draw it only if the cell has been explored by the player

                PrintedColor = map.IsInFov(PosX,PosY) ? Colors.StairsFov : Colors.Stairs; // TODO : changer les couleurs

                console.Set(PosX,PosY,PrintedColor,null,Symbol);
            }
        }
    }
}