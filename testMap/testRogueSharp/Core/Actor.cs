using RLNET;
using RogueSharp;
using testRogueSharp.Interfaces;

namespace testRogueSharp.Core{

    public class Actor : IActor, IDrawable{

        // IActor
        public string Name{get;set;}
        public int Awareness{get;set;}

        //IDrawable
        public RLColor Color{get;set;}
        public char Symbol{get;set;}

        public int X{get;set;}
        public int Y{get;set;}

        public void Draw(RLConsole console, IMap map){

            //On ne dessine pas les éléments acteurs des cells qui n'ont pas été explorées
            if(!map.GetCell(X,Y).IsExplored){return;}

            //On ne dessine que les éléments acteurs quand ils sont dans le champ de vision
            if(map.IsInFov(X,Y)){
                console.Set(X,Y,Color,Colors.FloorBackgroundFov,Symbol);
            }else{
                //Quand c'est pas dans le champ de vision,on dessine juste un sol normal
                console.Set(X,Y,Color,Colors.FloorBackground,'.');
            }


        }


    }
}