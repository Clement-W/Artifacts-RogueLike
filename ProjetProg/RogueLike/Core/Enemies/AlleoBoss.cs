using System;
using RogueLike.Core.Equipments;
namespace RogueLike.Core.Enemies
{
    public class AlleoBoss : FinalBoss
    {
        public AlleoBoss(int difficultyLevel){
            Random random = new Random();
            Attack = 1 * difficultyLevel;
            Defense = 3*difficultyLevel;
            Awareness = 100;
            PrintedColor = Colors.basicColor; //TODO: changer les couleurs
            BaseColor = PrintedColor;
            ColorAfterHit = Colors.ZombieHit;

            Gold= (int)(difficultyLevel) * random.Next(10,40);
            Health = random.Next(50,100) * difficultyLevel;
            MaxHealth = Health;
            Name = "Alleo boss"; //TODO: trouver un nom
            MovingTimePeriod = 4;
            RemainingTimePeriodToMove = MovingTimePeriod;
            UpSymbol = Icons.boss1Symbol; // TODO: changer les symbols LE TRUC BLEU
            DownSymbol = Icons.boss1Symbol;
            LeftSymbol = Icons.boss1Symbol;
            RightSymbol = Icons.boss1Symbol;
            Direction = Direction.Up;
            Symbol = DownSymbol; 

            Weapon = new Trident();
        }

    }
}