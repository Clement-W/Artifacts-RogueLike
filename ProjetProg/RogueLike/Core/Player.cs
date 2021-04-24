using RLNET;

namespace RogueLike.Core{

    public class Player : ActiveCharacter{

        public Player(){
            //create a player with the initial stats

            Attack = 10;
            Defense = 5;
            Awareness = 15;
            Gold=0;
            Health = 100;
            MaxHealth = 100;
            Speed = 10;
            Name = "Adventurer";
            Color = Colors.Player;
            Symbol = '@';
        }

        public void DrawStats(RLConsole statConsole){
            statConsole.Print(1,1,$"Name: {Name}", Colors.Text);
            statConsole.Print(1,3,$"Health: {Health}/{MaxHealth}",Colors.Text);
            statConsole.Print(1,5,$"Attack: {Attack}",Colors.Text);
            statConsole.Print(1,7,$"Defense: {Defense}",Colors.Text);
            statConsole.Print(1,9,$"Gold: {Gold}",Colors.Gold);
        }

        public void Move(int x, int y){
            PosX = x;
            PosY = y;
        }

    }
}