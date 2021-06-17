using RLNET;

namespace RogueLike.Core.Equipments
{

    /// <summary>
    /// This class represents a part of the armor : the helmet
    /// </summary>
    public class Helmet : DefenseEquipment
    {

        /// <summary>
        /// This constructor is private because the only way to create helmet armor is by using
        /// the static methods.
        /// </summary>
        /// <param name="name"> The name of the helmet</param>
        /// <param name="defBonus"> The defense bonus given by the helmet</param>
        /// <param name="cost">The cost of this equipment if it's on a merchant stall</param>
        /// <param name="color">The color of the helmet</param>
        private Helmet(string name, int defBonus, int cost, RLColor color) : base(name, defBonus, cost)
        {
            Symbol = Symbols.helmetSymbol;
            PrintedColor = color;
        }

        /// <summary>
        /// This constructor is also private, and is used to create the "None" helmet
        /// </summary>
        /// <param name="name">The name of the helmet</param>
        /// <param name="defBonus">The defense bonus given by the helmet</param>
        private Helmet(string name, int defBonus) : base(name, defBonus, 0)
        {
        }

        /// <summary>
        /// Create a None helmet
        /// The player starts with this, there's no defense bonus
        /// </summary>
        /// <returns>The None helmet</returns>
        public static Helmet None()
        {
            return new Helmet("None", 0);
        }

        /// <summary>
        /// Create the polymer helmet, it's the first level of helmet
        /// </summary>
        /// <returns>The polymer helmet</returns>
        public static Helmet CreatePolymerHelmet()
        {
            return new Helmet("Polymer Helmet", 1, 30, Colors.polymerColor);
        }

        /// <summary>
        /// Create the carbon helmet, it's the second level of helmet
        /// </summary>
        /// <returns>The carbon helmet</returns>
        public static Helmet CreateCarbonHelmet()
        {
            return new Helmet("Carbon Helmet", 2, 50, Colors.carbonColor);
        }


        /// <summary>
        /// Create the platinum helmet, it's the third level of helmet
        /// </summary>
        /// <returns>The platinum helmet</returns>
        public static Helmet CreatePlatinumHelmet()
        {
            return new Helmet("Platinum Helmet", 3, 70, Colors.platinumColor);
        }


        /// <summary>
        /// Create the titanium helmet, it's the fourth and last level of helmet
        /// This is the best helmet in the game
        /// </summary>
        /// <returns>The titanium helmet</returns>
        public static Helmet CreateTitaniumHelmet()
        {
            return new Helmet("Titanium Helmet", 4, 90, Colors.titaniumColor);
        }
    }
}