using System;
namespace RogueLike.Core.Enemies
{
    /// <summary>
    /// This class represent an enemy named Dendroide. This enemy is slow but deals more damages
    /// </summary>
    public class Dendroide : Enemy
    {

        /// <summary>
        /// The constructor creates the Dendroide with it's stats based on the difficulty level
        /// </summary>
        /// <param name="difficultyLevel"> The difficluty level of the game that is computed in MapGenerator </param>
        public Dendroide(int difficultyLevel){
            Random random = new Random();
            Attack = 8 * difficultyLevel;
            Defense = difficultyLevel;
            Awareness = 20;
            PrintedColor = Colors.BasicColor; //TODO: changer les couleurs
            BaseColor = PrintedColor;
            ColorAfterHit = Colors.ZombieHit;

            Gold= (int)(difficultyLevel) * random.Next(0,5);
            Health = random.Next(50,100);
            MaxHealth = Health;
            Name = "Dendroide";
            MovingTimePeriod = 5;
            RemainingTimePeriodToMove = MovingTimePeriod;
            UpSymbol = Symbols.dendroidSymbol;
            DownSymbol = Symbols.dendroidSymbol; // TODO: changer les symbols LE TRUC BLEU
            LeftSymbol = Symbols.dendroidSymbol;
            RightSymbol = Symbols.dendroidSymbol;
            Direction = Direction.Up;
            Symbol = DownSymbol; 
        }
    }
}