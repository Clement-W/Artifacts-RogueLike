using RLNET;

namespace RogueLike.Core.Equipments
{

    /// <summary>
    /// This class represents a part of the armor : the boots
    /// </summary>
    public class Boots : DefenseEquipment
    {
        /// <summary>
        /// This constructor is private because the only way to create boots armor is by using
        /// the static methods.
        /// </summary>
        /// <param name="name"> The name of the boots</param>
        /// <param name="defBonus"> The defense bonus given by the boots</param>
        /// <param name="cost">The cost of this equipment if it's on a merchant stall</param>
        /// <param name="color">The color of the boots</param>
        private Boots(string name, int defBonus, int cost, RLColor color) : base(name, defBonus, cost)
        {
            Symbol = Symbols.bootsSymbol;
            PrintedColor = color;
        }

        /// <summary>
        /// This constructor is also private, and is used to create the "None" Boots
        /// </summary>
        /// <param name="name">The name of the boots</param>
        /// <param name="defBonus">The defense bonus given by the boots</param>
        private Boots(string name, int defBonus) : base(name, defBonus, 0)
        {

        }

        /// <summary>
        /// Create a None boots
        /// The player starts with this, there's no defense bonus
        /// </summary>
        /// <returns>The None boots</returns>
        public static Boots None()
        {
            return new Boots("None", 0);
        }

        /// <summary>
        /// Create the polymer boots, it's the first level of boots
        /// </summary>
        /// <returns>The polymer boots</returns>
        public static Boots CreatePolymerBoots()
        {
            return new Boots("Polymer Boots", 1, 30, Colors.polymerColor);
        }

        /// <summary>
        /// Create the polymer boots, it's the second level of boots
        /// </summary>
        /// <returns>The carbon boots</returns>
        public static Boots CreateCarbonBoots()
        {
            return new Boots("Carbon Boots", 2, 50, Colors.carbonColor);
        }


        /// <summary>
        /// Create the platinum boots, it's the third level of boots
        /// </summary>
        /// <returns>The plpatinum boots</returns>
        public static Boots CreatePlatinumBoots()
        {
            return new Boots("Platinum Boots", 3, 70, Colors.platinumColor);
        }

        /// <summary>
        /// Create the polymer boots, it's the fourth and last level of boots
        /// This is the best boots in the game
        /// </summary>
        /// <returns>The titanium boots</returns>
        public static Boots CreateTitaniumBoots()
        {
            return new Boots("Titanium Boots", 4, 90, Colors.titaniumColor);
        }
    }
}