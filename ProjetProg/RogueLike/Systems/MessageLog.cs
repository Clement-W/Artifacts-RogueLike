using System.Collections.Generic;
using RLNET;
using RogueLike.Core;

namespace RogueLike.Systems{
    
    public class MessageLog{

        private static readonly int maxLines = 4;

        private readonly Queue<string> messages;

        public MessageLog(){
            messages = new Queue<string>();
        }

        public void AddMessage(string message){
            messages.Enqueue(message);
            if(messages.Count > maxLines){
                messages.Dequeue();
            }
        }

        public void Draw(RLConsole console){
            int cptMessage=0;
            foreach(string message in messages){
                console.Print(1,cptMessage+1,message,Colors.Text);
            }
        }
    }
}