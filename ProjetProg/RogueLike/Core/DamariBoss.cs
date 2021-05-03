using System;
using RogueLike.Behaviors;

namespace RogueLike.Core
{
    public class DamariBoss : FinalBoss
    {
        public DamariBoss(int difficultyLevel)
        {
            Random random = new Random();
            Attack = 10 * difficultyLevel;
            Defense = 3 * difficultyLevel;
            Awareness = 100;
            PrintedColor = Colors.Zombie; //TODO: changer les couleurs
            BaseColor = PrintedColor;
            ColorAfterHit = Colors.ZombieHit;

            Gold = (int)(difficultyLevel) * random.Next(10, 40);
            Health = random.Next(50, 100) * difficultyLevel;
            MaxHealth = Health;
            Name = "Damari boss"; //TODO: trouver un nom
            MovingTimePeriod = 20;
            RemainingTimePeriodToMove = MovingTimePeriod;
            UpSymbol = Icons.zombieSymbol; // TODO: changer les symbols LE TRUC BLEU
            DownSymbol = Icons.zombieSymbol;
            LeftSymbol = Icons.zombieSymbol;
            RightSymbol = Icons.zombieSymbol;
            Direction = Direction.Up;
            Symbol = DownSymbol;

            Weapon = new Scepter();
        }

        public override void PerformAction(Game game)
        {
            TeleportFarFromPlayer behavior = new TeleportFarFromPlayer();
            behavior.Act(this, game);
        }
    }
}