using System;
namespace RogueLike.Core
{
    public class Dendroide : Enemy
    {
        public Dendroide(int difficultyLevel){
            Random random = new Random();
            Attack = 8 * difficultyLevel;
            Defense = difficultyLevel;
            Awareness = 20;
            PrintedColor = Colors.Zombie; //TODO: changer les couleurs
            BaseColor = PrintedColor;
            ColorAfterHit = Colors.ZombieHit;

            Gold= (int)(difficultyLevel) * random.Next(0,5);
            Health = random.Next(50,100);
            MaxHealth = Health;
            Name = "Dendroide";
            MovingTimePeriod = 10;
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