using RogueLike.Interfaces;
using RLNET;
using RogueSharp;
namespace RogueLike.Core
{
    public abstract class TeleportationPortal : IDrawable, IAnimated
    {

        public char AlternateSymbol1 { get; set; }
        public char AlternateSymbol2 { get; set; }

        public MapType DestinationMap { get; set; }

        public PlanetName PlanetDestination{get;set;}

        public RLColor PrintedColor { get; set; }

        public char Symbol { get; set; } // The displayed symbol on the console

        public int PosX { get; set; }

        public int PosY { get; set; }

        public TeleportationPortal(){
            PrintedColor = Colors.TeleportationPortal;
        }

        public void Draw(RLConsole console, CurrentMap map){
            if(map.GetCell(PosX,PosY).IsExplored){ // Draw it only if the cell has been explored by the player

                PrintedColor = map.IsInFov(PosX,PosY) ? Colors.StairsFov : Colors.Stairs; // TODO : changer les couleurs

                console.Set(PosX,PosY,PrintedColor,null,Symbol);
            }
        }

        // called perdiodically to animate the portals
        public void ChangeSymbolAlternative() {
            Symbol = (Symbol == AlternateSymbol1) ? AlternateSymbol2 : AlternateSymbol1;
        }
    }
}