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
            MovingTimePeriod = 1;
            RemainingTimePeriodToMove = MovingTimePeriod;
            UpSymbol = 'À';
            DownSymbol = 'À';
            LeftSymbol = 'À';
            RightSymbol = 'À';

            Symbol = DownSymbol; 
        }
    }
}