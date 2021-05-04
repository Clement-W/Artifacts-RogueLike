using System;
using RogueLike.Core.Equipments;
namespace RogueLike.Core.Enemies
{
    /// <summary>
    /// This class represents the final boss of the Thaadd planet
    /// </summary>
    public class ThaaddBoss : FinalBoss
    {
        /// <summary>
        /// The constructor creates the boss with it's stats based on the difficulty level
        /// </summary>
        /// <param name="difficultyLevel"> The difficluty level of the game that is computed in MapGenerator </param>

        public ThaaddBoss(int difficultyLevel)
        {
            Random random = new Random();
            Attack = 1 * difficultyLevel;
            Defense = 3 * difficultyLevel;
            Awareness = 100;
            PrintedColor = Colors.basicColor;
            BaseColor = PrintedColor;
            ColorAfterHit = Colors.ZombieHit;

            Gold = (int)(difficultyLevel) * random.Next(10, 40);
            Health = random.Next(50, 100) * difficultyLevel;
            MaxHealth = Health;
            Name = "Thaadd boss"; 
            MovingTimePeriod = 4;
            RemainingTimePeriodToMove = MovingTimePeriod;
            UpSymbol = Icons.boss3Symbol; 
            DownSymbol = Icons.boss3Symbol;
            LeftSymbol = Icons.boss3Symbol;
            RightSymbol = Icons.boss3Symbol;
            Direction = Direction.Up;
            Symbol = DownSymbol;

            Weapon = new DeathScythe();
        }
    }
}