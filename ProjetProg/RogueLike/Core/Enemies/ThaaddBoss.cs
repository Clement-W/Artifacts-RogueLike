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
        /// <param name="difficultyLevel"> The difficulty level of the game that is computed in MapCreation </param>

        public ThaaddBoss(int difficultyLevel)
        {
            Random random = new Random();
            Attack = 1 * difficultyLevel;
            Defense = 3 * difficultyLevel;
            Awareness = 100;
            PrintedColor = Colors.BasicColor;
            BaseColor = PrintedColor;
            ColorAfterHit = Colors.ZombieHit;

            CarriedGold = (int)(difficultyLevel) * random.Next(10, 40);
            Health = random.Next(50, 100) * difficultyLevel;
            MaxHealth = Health;
            Name = "Thaadd boss"; 
            MovingTimePeriod = 4;
            RemainingTimePeriodToMove = MovingTimePeriod;
            UpSymbol = Symbols.boss3Symbol; 
            DownSymbol = Symbols.boss3Symbol;
            LeftSymbol = Symbols.boss3Symbol;
            RightSymbol = Symbols.boss3Symbol;
            Direction = Direction.Up;
            Symbol = DownSymbol;

            Weapon = new DeathScythe();
        }
    }
}