using testRogueSharp.Core;

namespace testRogueSharp.Systems{

    public class CommandSystem{
        
        //retourne true si le player a pu bouger, sinon false
        public bool MovePlayer(Direction direction){
            int x = Game.Player.X;
            int y = Game.Player.Y;

            switch(direction){
                case Direction.Up: y=Game.Player.Y -1; break;
                case Direction.Down: y=Game.Player.Y + 1; break;
                case Direction.Left: x=Game.Player.X -1; break;
                case Direction.Right: x=Game.Player.X +1; break;
                default: return false;
            }

            if(Game.DungeonMap.SetActorPosition(Game.Player,x,y)){
                return true; //bouge le player
            }
            return false;

        }
    }
}