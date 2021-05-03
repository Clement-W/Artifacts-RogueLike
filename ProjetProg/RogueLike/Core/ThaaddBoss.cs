using System;
namespace RogueLike.Core
{
    public class ThaaddBoss : FinalBoss
    {
        public ThaaddBoss(int difficultyLevel)
        {
            Random random = new Random();
            Attack = 1 * difficultyLevel;
            Defense = 3 * difficultyLevel;
            Awareness = 100;
            PrintedColor = Colors.Zombie; //TODO: changer les couleurs
            BaseColor = PrintedColor;
            ColorAfterHit = Colors.ZombieHit;

            Gold = (int)(difficultyLevel) * random.Next(10, 40);
            Health = random.Next(50, 100) * difficultyLevel;
            MaxHealth = Health;
            Name = "Thaadd boss"; //TODO: trouver un nom
            MovingTimePeriod = 4;
            RemainingTimePeriodToMove = MovingTimePeriod;
            UpSymbol = Icons.zombieSymbol; // TODO: changer les symbols LE TRUC BLEU
            DownSymbol = Icons.zombieSymbol;
            LeftSymbol = Icons.zombieSymbol;
            RightSymbol = Icons.zombieSymbol;
            Direction = Direction.Up;
            Symbol = DownSymbol;

            Weapon = new DeathScythe();
        }
    }
}