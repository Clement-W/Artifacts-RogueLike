using testRogueSharp.Interfaces;
using RogueSharp;
using RLNET;

namespace testRogueSharp.Core{
    public class Stairs : IDrawable{
        public RLColor Color{get;set;}
        public char Symbol{get;set;}
        public int X{get;set;}
        public int Y{get;set;}
        public bool IsUp{get;set;}

        public Stairs(int x, int y, bool isUp){
            X = x;
            Y = y;
            IsUp = isUp;
        }

        public void Draw(RLConsole console, IMap map){
            if(!map.GetCell(X,Y).IsExplored){ // on affiche rien si c'est pas exploré
                return;
            }

            Symbol = IsUp ? '<' : '>';
            if(map.IsInFov(X,Y)){
                Color = Colors.Player; 
            }else{
                Color = Colors.Floor;
            }

            console.Set(X,Y,Color,null,Symbol);


        }
    }
}