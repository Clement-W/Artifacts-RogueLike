using System;
namespace RogueLike.Core.Enemies
{


    public class Mecabat : Enemy
    {
        public Mecabat(int difficultyLevel){
            Random random = new Random();
            Attack = 1 * difficultyLevel;
            Defense = difficultyLevel;
            Awareness = 15;
            PrintedColor = Colors.basicColor; //TODO: changer les couleurs
            BaseColor = PrintedColor;
            ColorAfterHit = Colors.ZombieHit;

            Gold= (int)(difficultyLevel) * random.Next(0,5);
            Health = random.Next(20,30) * difficultyLevel;
            MaxHealth = Health;
            Name = "Mecabot";
            MovingTimePeriod = 1;
            RemainingTimePeriodToMove = MovingTimePeriod;
            UpSymbol = Icons.mecabatSymbol;
            DownSymbol = Icons.mecabatSymbol; // TODO: changer les symbols LE TRUC BLEU
            LeftSymbol = Icons.mecabatSymbol;
            RightSymbol = Icons.mecabatSymbol;
            Direction = Direction.Up;
            Symbol = DownSymbol; 
        }
    }
}