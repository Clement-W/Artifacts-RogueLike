using RLNET;
namespace testRogueSharp.Core
{

    public class Player : Actor
    {

        public Player()
        {
            Attack = 2; // peut faire jusqu'à 2 de dégats
            AttackChance = 50; //Une chance sur 2 d'attaquer
            Defense = 2; // Que 2 de défense
            DefenseChance = 40; //40% de chance défendre
            Gold = 0;
            Health = 100;
            MaxHealth = 100;
            Speed = 10; // plus speed est bas plus il peut agir raapidement 
            Awareness = 15;
            Name = "Rogue";
            Color = Colors.Player;
            Symbol = '@';
        }

        public void DrawStats(RLConsole statConsole)
        {
            statConsole.Print(1, 1, $"Name:   {Name}", Colors.Text);
            statConsole.Print(1, 3, $"Health:  {Health}/{MaxHealth}", Colors.Text);
            statConsole.Print(1, 5, $"Attack:  {Attack} ({AttackChance}%)", Colors.Text);
            statConsole.Print(1, 7, $"Defense: {Defense} ({DefenseChance}%)", Colors.Text);
            statConsole.Print(1, 9, $"Gold:    {Gold}", Colors.Gold);
        }
    }
}