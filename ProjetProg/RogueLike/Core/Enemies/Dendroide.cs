using System;
namespace RogueLike.Core.Enemies
{
    public class Dendroide : Enemy
    {
        public Dendroide(int difficultyLevel){
            Random random = new Random();
            Attack = 8 * difficultyLevel;
            Defense = difficultyLevel;
            Awareness = 20;
            PrintedColor = Colors.basicColor; //TODO: changer les couleurs
            BaseColor = PrintedColor;
            ColorAfterHit = Colors.ZombieHit;

            Gold= (int)(difficultyLevel) * random.Next(0,5);
            Health = random.Next(50,100);
            MaxHealth = Health;
            Name = "Dendroide";
            MovingTimePeriod = 5;
            RemainingTimePeriodToMove = MovingTimePeriod;
            UpSymbol = Icons.dendroidSymbol;
            DownSymbol = Icons.dendroidSymbol; // TODO: changer les symbols LE TRUC BLEU
            LeftSymbol = Icons.dendroidSymbol;
            RightSymbol = Icons.dendroidSymbol;
            Direction = Direction.Up;
            Symbol = DownSymbol; 
        }
    }
}