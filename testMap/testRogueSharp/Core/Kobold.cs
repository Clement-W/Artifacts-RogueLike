using RogueSharp.DiceNotation;
namespace testRogueSharp.Core
{

    public class Kobold : Monster
    {

        //Permet de générer un Kobold
        public static Kobold Create(int dungeonLevel)
        {
            // créé un objet sans constructeur, mais là l'aide d'initialisateur
            //on utilise le dé de rogue sharp: 
            // ex : dice.roll(3d6) produit un resultat entre 3 et 18 (3 dés à 6 faces)
            int health = Dice.Roll( "2D5" ); //obligé de définir avant car pareil pour health et maxhealth
            return new Kobold
            {
                Attack = Dice.Roll("1D3") + dungeonLevel / 3,
                AttackChance = Dice.Roll("25D3"),
                Awareness = 10,
                Color = Colors.KoboldColor,
                Defense = Dice.Roll("1D3") + dungeonLevel / 3,
                DefenseChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("5D5"),
                Health = health,
                MaxHealth = health,
                Name = "Kobold",
                Speed = 14,
                Symbol = '&'
             };
        }
    }
}