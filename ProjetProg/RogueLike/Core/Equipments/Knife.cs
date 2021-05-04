using RLNET;

namespace RogueLike.Core.Equipments
{

    /// <summary>
    /// This class represents a weapon : the knife
    /// </summary>
    public class Knife : AttackEquipment
    {

        /// <summary>
        /// This constructor is private because the only way to create a knife is by using
        /// the static methods.
        /// </summary>
        /// <param name="name"> The name of the knife</param>
        /// <param name="attBonus"> The attack bonus given by the knife</param>
        /// <param name="cost">The cost of this equipment if it's on a merchant stall</param>
        /// <param name="color">The color of the knife</param>
        private Knife(string name, int attBonus, int cost, RLColor color) : base(name, attBonus, cost)
        {
            AttackRange.Add(1, 1);
            Symbol = Icons.knifeSymbol;
            PrintedColor = color;
        }


        /// <summary>
        /// Create the Knife Mk1, it's the first level of knife
        /// Mk1 corresponds to Mark1, it's an english notation used to designate a version of a product.
        /// </summary>
        /// <returns>The Knife Mk1</returns>
        public static Knife CreateKnifeMk1()
        {
            return new Knife("Knife-Mk1", 0, 10, Colors.mk1Color);
        }


        /// <summary>
        /// Create the Knife Mk2, it's the second level of knife
        /// </summary>
        /// <returns>The Knife Mk2</returns>
        public static Knife CreateKnifeMk2()
        {
            return new Knife("Knife-Mk2", 2, 20, Colors.mk2Color);
        }


        /// <summary>
        /// Create the Knife Mk3, it's the third level of knife
        /// </summary>
        /// <returns>The Knife Mk3</returns>
        public static Knife CreateKnifeMk3()
        {
            return new Knife("Knife-Mk3", 4, 50, Colors.mk3Color);
        }


        /// <summary>
        /// Create the Knife Mk4, it's the last level of knife
        /// This is the best Knife in the game
        /// </summary>
        /// <returns>The Knife Mk4</returns>
        public static Knife CreateKnifeMk4()
        {
            return new Knife("Knife-Mk4", 7, 70, Colors.mk4Color);
        }




    }
}