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
            PrintedColor = Colors.Player;
            BaseColor = PrintedColor;
            ColorAfterHit = Colors.PlayerHit; // TODO changer ça

            UpSymbol = '@';
            DownSymbol = '@';
            LeftSymbol = '@';
            RightSymbol = '@';

            Symbol = DownSymbol;
        }

        public void DrawStats(RLConsole statConsole){

            int healthBarWidth = Dimensions.statConsoleWidth-2; 
            int remaningHealth = (int)(((double)Health/(double)MaxHealth)*healthBarWidth);

            // Create the health bar thanks to the background color
            statConsole.SetBackColor(1, 2,remaningHealth,1,Colors.HealthBar);
            statConsole.SetBackColor(1+remaningHealth, 2,healthBarWidth - remaningHealth,1,Colors.HealthBarDamage);

            statConsole.Print(((int)(healthBarWidth/2)),2,"PV",Colors.Text);

            statConsole.Print(3,3,$"Attack: {Attack}",Colors.GrayText);
            statConsole.Print(3+(int)(Dimensions.statConsoleWidth/3),3,$"Defense: {Defense}",Colors.GrayText);
            statConsole.Print(3+(int)(2*Dimensions.statConsoleWidth/3),3,$"Gold: {Gold}",Colors.Gold);
        }

        public void Move(int x, int y){
            PosX = x;
            PosY = y;
        }

    }
}