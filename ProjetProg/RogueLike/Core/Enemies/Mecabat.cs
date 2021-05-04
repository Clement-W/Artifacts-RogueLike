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
        /// <param name="difficultyLevel"> The difficluty level of the game that is computed in MapGenerator </param>
        public Mecabat(int difficultyLevel)
        {
            Random random = new Random();
            Attack = 1 * difficultyLevel;
            Defense = difficultyLevel;
            Awareness = 15;
            PrintedColor = Colors.basicColor; //TODO: changer les couleurs
            BaseColor = PrintedColor;
            ColorAfterHit = Colors.ZombieHit;

            Gold = (int)(difficultyLevel) * random.Next(0, 5);
            Health = random.Next(20, 30) * difficultyLevel;
            MaxHealth = Health;
            Name = "Mecabot";
            MovingTimePeriod = 1;
            RemainingTimePeriodToMove = MovingTimePeriod;
            UpSymbol = Symbols.mecabatSymbol;
            DownSymbol = Symbols.mecabatSymbol; // TODO: changer les symbols LE TRUC BLEU
            LeftSymbol = Symbols.mecabatSymbol;
            RightSymbol = Symbols.mecabatSymbol;
            Direction = Direction.Up;
            Symbol = DownSymbol;
        }
    }
}