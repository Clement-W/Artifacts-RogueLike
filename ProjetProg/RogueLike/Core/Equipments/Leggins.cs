using RLNET;

namespace RogueLike.Core.Equipments
{

    /// <summary>
    /// This class represent a part of the armor : the leggins
    /// </summary>
    public class Leggins : DefenseEquipment
    {

        /// <summary>
        /// This constructor is private because the only way to create leggins armor is by using
        /// the static methods.
        /// </summary>
        /// <param name="name"> The name of the leggins</param>
        /// <param name="defBonus"> The defense bonus given by the leggins</param>
        /// <param name="cost">The cost of this equipment if it's on a merchant stall</param>
        /// <param name="color">The color of the leggins</param>

        private Leggins(string name, int defBonus, int cost, RLColor color) : base(name, defBonus, cost)
        {
            Symbol = Symbols.legginsSymbol;
            PrintedColor = color;
        }

        /// <summary>
        /// This constructor is also private, and is used to create the "None" leggins
        /// </summary>
        /// <param name="name">The name of the leggins</param>
        /// <param name="defBonus">The defense bonus given by the leggins</param>
        private Leggins(string name, int defBonus) : base(name, defBonus, 0)
        {
        }

        /// <summary>
        /// Create a None leggins
        /// The player starts with this, there's no defense bonus
        /// </summary>
        /// <returns>The None leggins</returns>
        public static Leggins None()
        {
            return new Leggins("None", 0);
        }

        /// <summary>
        /// Create the polymer leggins, it's the first level of leggins
        /// </summary>
        /// <returns>The polymer leggins</returns>
        public static Leggins CreatePolymerLeggins()
        {
            return new Leggins("Polymer Leggins", 1, 30, Colors.polymerColor);
        }


        /// <summary>
        /// Create the carbon leggins, it's the second level of leggins
        /// </summary>
        /// <returns>The carbon leggins</returns>
        public static Leggins CreateCarbonLeggins()
        {
            return new Leggins("Carbon Leggins", 2, 50, Colors.carbonColor);
        }

        /// <summary>
        /// Create the platinum leggins, it's the third level of leggins
        /// </summary>
        /// <returns>The platinum leggins</returns>
        public static Leggins CreatePlatinumLeggins()
        {
            return new Leggins("Platinum Leggins", 3, 70, Colors.platinumColor);
        }

        /// <summary>
        /// Create the titanium leggins, it's the fourth and last level of leggins
        /// This is the best leggins in the game
        /// </summary>
        /// <returns>The titanium leggins</returns>
        public static Leggins CreateTitaniumLeggins()
        {
            return new Leggins("Titanium Leggins", 4, 90, Colors.titaniumColor);
        }
    }
}