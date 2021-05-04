using RLNET;

namespace RogueLike.Core.Equipments
{
    /// <summary>
    /// This class represents a weapon : the spear
    /// </summary>
    public class Spear : AttackEquipment
    {

        /// <summary>
        /// This constructor is private because the only way to create a spear is by using
        /// the static methods.
        /// </summary>
        /// <param name="name"> The name of the spear</param>
        /// <param name="attBonus"> The attack bonus given by the spear</param>
        /// <param name="cost">The cost of this equipment if it's on a merchant stall</param>
        /// <param name="color">The color of the spear</param>
        private Spear(string name, int attBonus, int cost, RLColor color) : base(name, attBonus, cost)
        {
            AttackRange.Add(1, 1);
            AttackRange.Add(2, 1);
            Symbol = Symbols.spearSymbol;
            PrintedColor = color;
        }

        /// <summary>
        /// Create the spear Mk1, it's the first level of spear
        /// Mk1 corresponds to Mark1, it's an english notation used to designate a version of a product.
        /// </summary>
        /// <returns>The spear Mk1</returns>
        public static Spear CreateSpearMk1()
        {
            return new Spear("Spear-Mk1", 1, 30, Colors.mk1Color);
        }

        /// <summary>
        /// Create the spear Mk2, it's the second level of spear
        /// </summary>
        /// <returns>The spear Mk2</returns>
        public static Spear CreateSpearMk2()
        {
            return new Spear("Spear-Mk2", 2, 50, Colors.mk2Color);
        }

        /// <summary>
        /// Create the spear Mk3, it's the third level of spear
        /// </summary>
        /// <returns>The spear Mk3</returns>
        public static Spear CreateSpearMk3()
        {
            return new Spear("Spear-Mk3", 3, 70, Colors.mk3Color);
        }

        /// <summary>
        /// Create the spear Mk4, it's the last level of spear
        /// This is the best spear in the game
        /// </summary>
        /// <returns>The spear Mk4</returns>
        public static Spear CreateSpearMk4()
        {
            return new Spear("Spear-Mk4", 5, 100, Colors.mk4Color);
        }


    }
}