namespace RogueLike.Core.Equipments
{

    /// <summary>
    /// This class represents the equipments designed to protect from damage
    /// </summary>
    public abstract class DefenseEquipment : Equipment
    {
        /// <value>
        /// The defense bonus given to the player with this equipment 
        /// </value>
        public int DefenseBonus { get; set; }


        /// <summary>
        /// This is a constructor of this class.
        /// </summary>
        /// <param name="name">The name of the defense equipment</param>
        /// <param name="defBonus">The bonus of defense given by this equipment</param>
        /// <param name="cost">The cost of this equipment if it's on a merchant stall</param>
        public DefenseEquipment(string name, int defBonus, int cost)
        {
            Name = name;
            DefenseBonus = defBonus;
            Cost = cost;
        }

    }
}