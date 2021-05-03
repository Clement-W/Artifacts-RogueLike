using System;
namespace RogueLike.Core
{
    public class Mecabat : Enemy
    {
        public Mecabat(int difficultyLevel){
            Random random = new Random();
            Attack = 1 * difficultyLevel;
            Defense = difficultyLevel;
            Awareness = 20;
            PrintedColor = Colors.Zombie; //TODO: changer les couleurs
            BaseColor = PrintedColor;
            ColorAfterHit = Colors.ZombieHit;

            Gold= (int)(difficultyLevel) * random.Next(0,5);
            Health = random.Next(20,30) * difficultyLevel;
            MaxHealth = Health;
            Name = "Mecabot";
            MovingTimePeriod = 1;
            RemainingTimePeriodToMove = MovingTimePeriod;
            UpSymbol = Icons.zombieSymbol;
            DownSymbol = Icons.zombieSymbol; // TODO: changer les symbols LE TRUC BLEU
            LeftSymbol = Icons.zombieSymbol;
            RightSymbol = Icons.zombieSymbol;
            Direction = Direction.Up;
            Symbol = DownSymbol; 
        }
    }
}