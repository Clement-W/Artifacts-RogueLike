using System;
using RogueLike.Core.Equipments;

namespace RogueLike.Core.Enemies
{
    /// <summary>
    /// This class represents the final boss of the Alleo planet
    /// </summary>
    public class AlleoBoss : FinalBoss
    {
        /// <summary>
        /// The constructor creates the boss with it's stats based on the difficulty level
        /// </summary>
        /// <param name="difficultyLevel"> The difficluty level of the game that is computed in MapCreation </param>
        public AlleoBoss(int difficultyLevel){
            Random random = new Random();
            Attack = 1 * difficultyLevel;
            Defense = 2*difficultyLevel;
            Awareness = 100;
            PrintedColor = Colors.basicColor; 
            BaseColor = PrintedColor;
            ColorAfterHit = Colors.ZombieHit;

            CarriedGold= (int)(difficultyLevel) * random.Next(10,40);
            Health = random.Next(50,100) * difficultyLevel;
            MaxHealth = Health;
            Name = "Alleo boss"; 
            MovingTimePeriod = 4;
            RemainingTimePeriodToMove = MovingTimePeriod;
            UpSymbol = Symbols.boss1Symbol; 
            DownSymbol = Symbols.boss1Symbol;
            LeftSymbol = Symbols.boss1Symbol;
            RightSymbol = Symbols.boss1Symbol;
            Direction = Direction.Up;
            Symbol = DownSymbol; 

            Weapon = new Trident();
        }

    }
}