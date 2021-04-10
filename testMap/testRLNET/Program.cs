using System;
using RLNET;

namespace testRLNET
{
    class Program
    {
        
        static void Main(string[] args)
        {
            RLRootConsole rootConsole = new RLRootConsole("terminal8x8.png", 60,40,8,8);
            // 60 = width ; 40 = height ; 8,8 = w et h d'un tile en pixel sur l'image de font
            Engine engine= new Engine(rootConsole);
            rootConsole.Run();
            
        }
    }

    public class Engine{
        private RLRootConsole rootConsole;

        private int px;
        private int py;
        
        public Engine(RLRootConsole console){
            rootConsole = console;
            rootConsole.Render +=Render;
            rootConsole.Update +=Update;
        }

        public void Render(object sender, UpdateEventArgs e){
            rootConsole.Clear();
            rootConsole.Set(px,py,RLColor.White, null, '@');
            rootConsole.Draw();
        }

        public void Update(object sender, UpdateEventArgs e){
            RLKeyPress keyPress = rootConsole.Keyboard.GetKeyPress();
            if(keyPress !=null){
                switch(keyPress.Key){
                    case RLKey.Up: py--; break;
                    case RLKey.Down: py++; break;
                    case RLKey.Left: px--; break;
                    case RLKey.Right: px++; break;
                    case RLKey.Escape: rootConsole.Close(); break;
                }
            }
        }
    }

    
}
