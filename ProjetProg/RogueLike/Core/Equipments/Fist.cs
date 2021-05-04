namespace RogueLike.Core.Equipments
{
    /// <summary>
    /// This class represents the initial attack equipment of the player : the fist
    /// </summary>
    public class Fist : AttackEquipment
    {

        /// <summary>
        /// Allow to create the fist attack equipment, with 0 attack bonus
        /// </summary>
        public Fist()
        {
            Name = "Fist";
            AttackBonus = 0;
            AttackRange.Add(1, 1);
            Symbol = Icons.fistSymbol;
        }

    }
}