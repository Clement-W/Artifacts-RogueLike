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

            Gold= (int)(diffifultyLevel/2) * random.Next(0,11);
            Health = random.Next(0,10) * diffifultyLevel;
            MaxHealth = Health;
            Name = "Zombie";
            MovingTimePeriod = 1;
            RemainingTimePeriodToMove = MovingTimePeriod;
            UpSymbol = '&';
            DownSymbol = '&';
            LeftSymbol = '&';
            RightSymbol = '&';

            Symbol = DownSymbol; 
        }
    }
}