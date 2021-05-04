using System;
namespace RogueLike.Core.Enemies
{
    public class Zombie : Enemy
    {
        public Zombie(int difficultyLevel){
            Random random = new Random();
            Attack = 6 * difficultyLevel;
            Defense = difficultyLevel;
            Awareness = 10;
            PrintedColor = Colors.Zombie;
            BaseColor = PrintedColor;
            ColorAfterHit = Colors.ZombieHit;

            Gold= (int)(difficultyLevel) * random.Next(0,5);
            Health = random.Next(0,30) * difficultyLevel;
            MaxHealth = Health;
            Name = "Zombie";
            MovingTimePeriod = 2;
            RemainingTimePeriodToMove = MovingTimePeriod;
            UpSymbol = Icons.zombieSymbol;
            DownSymbol = Icons.zombieSymbol;
            LeftSymbol = Icons.zombieSymbol;
            RightSymbol = Icons.zombieSymbol;
            Direction = Direction.Up;
            Symbol = DownSymbol; 
        }
    }
}