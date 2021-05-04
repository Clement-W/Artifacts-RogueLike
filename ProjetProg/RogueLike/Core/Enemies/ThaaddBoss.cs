using System;
using RogueLike.Core.Equipments;
namespace RogueLike.Core.Enemies
{
    public class ThaaddBoss : FinalBoss
    {
        public ThaaddBoss(int difficultyLevel)
        {
            Random random = new Random();
            Attack = 1 * difficultyLevel;
            Defense = 3 * difficultyLevel;
            Awareness = 100;
            PrintedColor = Colors.basicColor; //TODO: changer les couleurs
            BaseColor = PrintedColor;
            ColorAfterHit = Colors.ZombieHit;

            Gold = (int)(difficultyLevel) * random.Next(10, 40);
            Health = random.Next(50, 100) * difficultyLevel;
            MaxHealth = Health;
            Name = "Thaadd boss"; //TODO: trouver un nom
            MovingTimePeriod = 4;
            RemainingTimePeriodToMove = MovingTimePeriod;
            UpSymbol = Icons.boss3Symbol; // TODO: changer les symbols LE TRUC BLEU
            DownSymbol = Icons.boss3Symbol;
            LeftSymbol = Icons.boss3Symbol;
            RightSymbol = Icons.boss3Symbol;
            Direction = Direction.Up;
            Symbol = DownSymbol;

            Weapon = new DeathScythe();
        }
    }
}