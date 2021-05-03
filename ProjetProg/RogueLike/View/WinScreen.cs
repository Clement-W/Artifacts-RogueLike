using RLNET;
using RogueLike.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.View {
    public class WinScreen: ScreenView 
    {


        public WinScreen(Game game) : base(game) {
            RootConsole.Update += OnWinUpdate;
            RootConsole.Render += OnWinRender;
            // += Allows to add a new event handler to the rootConsole.Update event
            // += Call the add method of the event.
            RootConsole.Run();
            ChangeTitle("Victory!");
        }

        private void OnWinRender(object sender, UpdateEventArgs e) {
            if (RenderRequired) {
                RootConsole.Clear();

                RootConsole.Print(
                    (int)(RootConsole.Width * 0.1),
                    (int)(RootConsole.Height * 0.25) - 4,
                    "Victoire !: press n to restart",
                    Colors.Text
                );

                RootConsole.Draw();
            }
        }

        private void OnWinUpdate(object sender, UpdateEventArgs e) {
            KeyPress = RootConsole.Keyboard.GetKeyPress();

            if (KeyPress != null) {

                switch (KeyPress.Key) {
                    case RLKey.N: // N for new game
                        // Remove the unneeded event handlers
                        RootConsole.Update -= OnWinUpdate;
                        RootConsole.Render -= OnWinRender;
                        StartGame();
                        break;

                    case RLKey.Escape:
                        RootConsole.Close();
                        break;

                }

            }


        }

        private void StartGame() {
            Game.StartGame();
        }





    }
}
