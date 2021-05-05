using System;
using RogueLike.Core.Behaviors;
using RogueLike.Systems;
using RogueLike.Core.Equipments;
namespace RogueLike.Core.Enemies
{

    /// <summary>
    /// This class represents the final boss of the Damari planet
    /// </summary>
    public class DamariBoss : FinalBoss
    {

        /// <summary>
        /// The constructor create the boss with it's stats based on the difficulty level
        /// </summary>
        /// <param name="difficultyLevel"> The difficluty level of the game that is computed in MapGenerator </param>
        public DamariBoss(int difficultyLevel)
        {
            Random random = new Random();
            Attack = 10 * difficultyLevel;
            Defense = 3 * difficultyLevel;
            Awareness = 100;
            PrintedColor = Colors.BasicColor; 
            BaseColor = PrintedColor;
            ColorAfterHit = Colors.ZombieHit;

            Gold = (int)(difficultyLevel) * random.Next(10, 40);
            Health = random.Next(50, 100) * difficultyLevel;
            MaxHealth = Health;
            Name = "Damari boss"; 
            MovingTimePeriod = 20;
            RemainingTimePeriodToMove = MovingTimePeriod;
            UpSymbol = Symbols.boss2Symbol; 
            DownSymbol = Symbols.boss2Symbol;
            LeftSymbol = Symbols.boss2Symbol;
            RightSymbol = Symbols.boss2Symbol;
            Direction = Direction.Up;
            Symbol = DownSymbol;

            Weapon = new Scepter();
        }

        /// <summary>
        /// Perform an action according to a specific behavior
        /// </summary>
        /// <param name="player">The player is needed to attack it if needed</param>
        /// <param name="map">The map where the enemy and the player are situated</param>
        /// <param name="command">The command system is used to take an action on the game</param>
        public override void PerformAction(Player player, CurrentMap map, CommandSystem commandSystem)
        {
            TeleportFarFromPlayer behavior = new TeleportFarFromPlayer();
            behavior.Act(this, player, map, commandSystem);
        }
    }
}