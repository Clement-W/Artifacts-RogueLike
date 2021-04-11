using System.Collections.Generic;
using RLNET;

namespace testRogueSharp.Systems{

    public class MessageLog{

        private static readonly int maxLines = 3; //le nombre max de ligne à sauvegarder

        private readonly Queue<string> lines;

        public MessageLog(){
            lines = new Queue<string>();
        }

        public void Add(string message){
            lines.Enqueue(message);
            //S'il y a un message en trop on enleve le plus vieux
            if(lines.Count > maxLines){
                lines.Dequeue();
            }
        }

        //Dessine chaque ligne de message dans la console
        public void Draw(RLConsole console){
            string[] linesArr = lines.ToArray();
            for(int i =0; i <linesArr.Length; i++){
                console.Print(1,i+1,linesArr[i],RLColor.White);
            }

        }

    }
}