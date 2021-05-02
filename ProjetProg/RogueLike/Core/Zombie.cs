using System;
namespace RogueLike.Core
{
    public class Zombie : Enemy
    {
        public Zombie(int diffifultyLevel){
            Random random = new Random();
            Attack = 2 * diffifultyLevel;
            Defense = diffifultyLevel;
            Awareness = 10;
            PrintedColor = Colors.Zombie;
            BaseColor = PrintedColor;
            ColorAfterHit = Colors.ZombieHit;

            Gold= (int)(diffifultyLevel) * random.Next(0,5);
            Health = random.Next(0,30) * diffifultyLevel;
            MaxHealth = Health;
            Name = "Zombie";
            MovingTimePeriod = 2;
            RemainingTimePeriodToMove = MovingTimePeriod;
            UpSymbol = Icons.zombieSymbol;
            DownSymbol = Icons.zombieSymbol;
            LeftSymbol = Icons.zombieSymbol;
            RightSymbol = Icons.zombieSymbol;

            Symbol = DownSymbol; 
        }
    }
}