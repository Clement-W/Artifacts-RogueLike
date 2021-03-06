using System;

namespace RogueLike.Core.Enemies
{
    /// <summary>
    /// This class represent an enemy named Zombie. It's slow and deals low damages
    /// </summary>
    public class Zombie : Enemy
    {
        /// <summary>
        /// The constructor create the Zombie with it's stats based on the difficulty level
        /// </summary>
        /// <param name="difficultyLevel"> The difficulty level of the game that is computed in MapCreation </param>
        public Zombie(int difficultyLevel)
        {
            Random random = new Random();
            Attack = 3 * difficultyLevel;
            Defense = difficultyLevel;
            Awareness = 10;
            PrintedColor = Colors.Zombie;
            BaseColor = PrintedColor;
            ColorAfterHit = Colors.ZombieHit;

            CarriedGold = (int)(difficultyLevel) * random.Next(0, 5);
            Health = random.Next(0, 30) * difficultyLevel;
            MaxHealth = Health;
            Name = "Zombie";
            MovingTimePeriod = 2;
            RemainingTimePeriodToMove = MovingTimePeriod;
            UpSymbol = Symbols.zombieSymbol;
            DownSymbol = Symbols.zombieSymbol;
            LeftSymbol = Symbols.zombieSymbol;
            RightSymbol = Symbols.zombieSymbol;
            Direction = Direction.Up;
            Symbol = DownSymbol;
        }
    }
}