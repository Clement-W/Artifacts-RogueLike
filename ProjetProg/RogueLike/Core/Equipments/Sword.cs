using RLNET;

namespace RogueLike.Core.Equipments
{

    /// <summary>
    /// This class represents a weapon : the sword
    /// </summary>
    public class Sword : AttackEquipment
    {

        /// <summary>
        /// This constructor is private because the only way to create a sword is by using
        /// the static methods.
        /// </summary>
        /// <param name="name"> The name of the sword</param>
        /// <param name="attBonus"> The attack bonus given by the sword</param>
        /// <param name="cost">The cost of this equipment if it's on a merchant stall</param>
        /// <param name="color">The color of the sword</param>
        private Sword(string name, int attBonus, int cost, RLColor color) : base(name, attBonus, cost)
        {
            AttackRange.Add(1, 3);
            Symbol = Icons.swordSymbol;
            PrintedColor = color;
        }



        /// <summary>
        /// Create the sword Mk1, it's the first level of sword
        /// Mk1 corresponds to Mark1, it's an english notation used to designate a version of a product.
        /// </summary>
        /// <returns>The sword Mk1</returns>
        public static Sword CreateSwordMk1()
        {
            return new Sword("Sword-Mk1", 1, 40, Colors.mk1Color);
        }


        /// <summary>
        /// Create the sword Mk2, it's the second level of sword
        /// </summary>
        /// <returns>The sword Mk2</returns>
        public static Sword CreateSwordMk2()
        {
            return new Sword("Sword-Mk2", 3, 60, Colors.mk2Color);
        }


        /// <summary>
        /// Create the sword Mk3, it's the third level of sword
        /// </summary>
        /// <returns>The sword Mk3</returns>
        public static Sword CreateSwordMk3()
        {
            return new Sword("Sword-Mk3", 4, 90, Colors.mk3Color);
        }

        /// <summary>
        /// Create the sword Mk4, it's the last level of sword
        /// This is the best sword in the game
        /// </summary>
        /// <returns>The sword Mk4</returns>
        public static Sword CreateSwordMk4()
        {
            return new Sword("Sword-Mk4", 6, 120, Colors.mk4Color);
        }


    }
}