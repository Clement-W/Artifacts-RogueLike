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
        private int speed; // For the energy based schedulling system

        public int Attack
        {
            get { return attack; }
            set { attack = value; }
        }

        public int Defense
        {
            get { return defense; }
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

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        // The 4 next symbols corresponds to the 4 direction sprites of the active character

        public char DownSymbol { get; set; }
        public char UpSymbol { get; set; }
        public char LeftSymbol { get; set; }
        public char RightSymbol { get; set; }

        public RLColor BaseColor { get; set; }

        public RLColor ColorAfterHit { get; set; }


        public void ChangeColorAfterHit()
        {
            PrintedColor = ColorAfterHit;
            Thread.Sleep(100); // Wait that the color is rendered on the game screen;
            PrintedColor = BaseColor;
        }




    }
}