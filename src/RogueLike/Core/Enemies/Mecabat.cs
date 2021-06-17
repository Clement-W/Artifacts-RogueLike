using System;

namespace RogueLike.Core.Enemies
{


    /// <summary>
    /// This class represent an enemy named Mecabat. This enemy is really fast but deals small damages
    /// </summary>
    public class Mecabat : Enemy
    {
        /// <summary>
        /// The constructor creates the Mecabat with it's stats based on the difficulty level
        /// </summary>
        /// <param name="difficultyLevel"> The difficulty level of the game that is computed in MapCreation </param>
        public Mecabat(int difficultyLevel)
        {
            Random random = new Random();
            Attack = 2 * difficultyLevel;
            Defense = difficultyLevel;
            Awareness = 15;
            PrintedColor = Colors.BasicColor;
            BaseColor = PrintedColor;
            ColorAfterHit = Colors.ZombieHit;

            CarriedGold = (int)(difficultyLevel) * random.Next(0, 5);
            Health = random.Next(20, 30) * difficultyLevel;
            MaxHealth = Health;
            Name = "Mecabot";
            MovingTimePeriod = 1;
            RemainingTimePeriodToMove = MovingTimePeriod;
            UpSymbol = Symbols.mecabatSymbol;
            DownSymbol = Symbols.mecabatSymbol; 
            LeftSymbol = Symbols.mecabatSymbol;
            RightSymbol = Symbols.mecabatSymbol;
            Direction = Direction.Up;
            Symbol = DownSymbol;
        }
    }
}