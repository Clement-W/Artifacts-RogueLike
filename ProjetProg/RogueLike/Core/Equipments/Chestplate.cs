using RLNET;

namespace RogueLike.Core.Equipments
{

    /// <summary>
    /// This class represents a part of the armor : the chestplate
    /// </summary>
    public class Chestplate : DefenseEquipment
    {
        /// <summary>
        /// This constructor is private because the only way to create chestplate armor is by using
        /// the static methods.
        /// </summary>
        /// <param name="name"> The name of the chestplate</param>
        /// <param name="defBonus"> The defense bonus given by the chestplate</param>
        /// <param name="cost">The cost of this equipment if it's on a merchant stall</param>
        /// <param name="color">The color of the chestplate</param>
        private Chestplate(string name, int defBonus, int cost, RLColor color) : base(name, defBonus, cost)
        {
            Symbol = Icons.chestplateSymbol;
            PrintedColor = color;
        }

        /// <summary>
        /// This constructor is also private, and is used to create the "None" chestplate
        /// </summary>
        /// <param name="name">The name of the chestplate</param>
        /// <param name="defBonus">The defense bonus given by the chestplate</param>
        private Chestplate(string name, int defBonus) : base(name, defBonus, 0) { }

        /// <summary>
        /// Create a None chestplate
        /// The player starts with this, there's no defense bonus
        /// </summary>
        /// <returns>The None chestplate</returns>
        public static Chestplate None()
        {
            return new Chestplate("None", 0);
        }

        /// <summary>
        /// Create the polymer chestplate, it's the first level of chestplate
        /// </summary>
        /// <returns>The polymer chestplate</returns>
        public static Chestplate CreatePolymerChestplate()
        {
            return new Chestplate("Polymer Chestplate", 1, 30, Colors.polymerColor);
        }


        /// <summary>
        /// Create the carbon chestplate, it's the second level of chestplate
        /// </summary>
        /// <returns>The carbon chestplate</returns>
        public static Chestplate CreateCarbonChestplate()
        {
            return new Chestplate("Carbon Chestplate", 2, 50, Colors.carbonColor);
        }

        /// <summary>
        /// Create the platinum chestplate, it's the third level of chestplate
        /// </summary>
        /// <returns>The platinum chestplate</returns>
        public static Chestplate CreatePlatinumChestplate()
        {
            return new Chestplate("Platinum Chestplate", 3, 70, Colors.platinumColor);
        }

        /// <summary>
        /// Create the titanium chestplate, it's the fourth and last level of chestplate
        /// This is the best chestplate in the game
        /// </summary>
        /// <returns>The titanium chestplate</returns>
        public static Chestplate CreateTitaniumChestplate()
        {
            return new Chestplate("Titanium Chestplate", 4, 90, Colors.titaniumColor);
        }
    }
}