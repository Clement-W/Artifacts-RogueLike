using System.Threading;
using RLNET;
namespace RogueLike.Core
{

    public abstract class ActiveCharacter : Character
    {

        private int attack;
        private int defense;
        private int awareness;
        private int gold;
        private int health;
        private int maxHealth;


        public int Attack
        {
            get
            {
                return attack + Weapon.AttackBonus;
            }
            set { attack = value; }
        }

        public int Defense
        {
            get
            {
                if (this is Player)
                {
                    Player p = this as Player;
                    return defense + p.Head.DefenseBonus + p.Chest.DefenseBonus + p.Legs.DefenseBonus + p.Feet.DefenseBonus;
                }
                return defense;
            }
            set { defense = value; }
        }

        public int Awareness
        {
            get { return awareness; }
            set { awareness = value; }
        }

        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = value; }
        }



        // The 4 next symbols corresponds to the 4 direction sprites of the active character
        // This is only used by the player at the moment, but it will be used by the enemies later
        public char DownSymbol { get; set; }
        public char UpSymbol { get; set; }
        public char LeftSymbol { get; set; }
        public char RightSymbol { get; set; }

        public Direction Direction{get;set;}

        public RLColor BaseColor { get; set; }

        public RLColor ColorAfterHit { get; set; }

        public AttackEquipment Weapon { get; set; }


        public void ChangeColorAfterHit()
        {
            PrintedColor = ColorAfterHit;
            Thread.Sleep(100); // Wait that the color is rendered on the game screen;
            PrintedColor = BaseColor;
        }




    }
}