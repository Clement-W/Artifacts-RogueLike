using System;
namespace RogueLike.Core
{
    public class Zombie : Enemy
    {
        public Zombie(int diffifultyLevel){
            Random random = new Random();
            Attack = 2 * diffifultyLevel;
            Defense = diffifultyLevel;
            Awareness = 10;
            Color = Colors.Zombie;
            Gold= (int)(diffifultyLevel/2) * random.Next(0,11);
            Health = random.Next(0,50) * diffifultyLevel;
            MaxHealth = Health;
            Name = "Zombie";
            Speed = 14;
            Symbol = '&';   
        }
    }
}