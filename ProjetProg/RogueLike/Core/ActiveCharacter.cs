using System.Threading;
using RLNET;
using RogueLike.Core.Equipments;
namespace RogueLike.Core
{
    /// <summary>
    /// This class represent an activeCharacter. The active characters are those who can take actions on the game.
    /// For now, only the player and the enemies are active characters.
    /// </summary>
    public abstract class ActiveCharacter : Character
    {
        /// <summary>
        /// This is the initial attack stat of the character
        /// </summary>
        private int attack;

        /// <summary>
        /// This is the initial defense stat of the character
        /// </summary>
        private int defense;

        /// <summary>
        /// This is the awareness of the character. Awareness is used to compute the Fov.
        /// </summary>
        public int Awareness { get; set; }

        /// <summary>
        /// This is the gold amount that has the character
        /// </summary>
        public int Gold { get; set; }

        /// <summary>
        /// This is the current health of character
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// This is the maximum health that the character can have
        /// </summary>
        public int MaxHealth { get; set; }

        /// <value>
        /// This property is used to add the weapon attack to the player attack stat
        /// </value>
        public int Attack
        {
            get
            {
                return attack + Weapon.AttackBonus;
            }
            set { attack = value; }
        }


        /// <value>
        /// This property is used to add the defense stat of the defense equipment to the initial defense 
        /// stat if the character is the player.
        /// </value>
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




        // The 4 next symbols corresponds to the 4 direction sprites of the active character
        // This is only used by the player at the moment, but it will be used by the enemies later


        /// <value>
        /// The character's symbol when it's looking downward
        /// </value>
        public char DownSymbol { get; set; }

        /// <value>
        /// The character's symbol when it's looking upward
        /// </value>
        public char UpSymbol { get; set; }

        /// <value>
        /// The character's symbol when it's looking on the left
        /// </value>
        public char LeftSymbol { get; set; }

        /// <value>
        /// The character's symbol when it's looking on the right
        /// </value>
        public char RightSymbol { get; set; }

        /// <value>
        /// This corresponds to the direction of the active character
        /// </value>
        public Direction Direction { get; set; }

        /// <value>
        /// This is the base color of the character. This color can change when
        /// the active character is hit.
        /// </value>
        public RLColor BaseColor { get; set; }

    
        /// <value>
        /// This is the color of the active character when hit.
        /// </value>
        public RLColor ColorAfterHit { get; set; }

  
        /// <value>
        /// This is the Weapon worn by the active character 
        /// </value>
        public AttackEquipment Weapon { get; set; }


        /// <summary>
        /// This method change the color of the active character to show when they're hit
        /// </summary>
        public void ChangeColorAfterHit()
        {
            PrintedColor = ColorAfterHit;
            Thread.Sleep(100); // Wait that the color is rendered on the game screen;
            PrintedColor = BaseColor;
        }

    }
}