using RogueLike.Core;

namespace RogueLike.Interfaces
{
    /// <summary>
    /// This interface is implemented by every elements that can be looted on the map.
    /// If a class implements ILoot, the object of this class will be able to be displayed
    /// on the floor in the map.
    /// </summary>
    public interface ILoot
    {
        /// <value>The x position of the lootable element on the map</value>
        int PosX { get; set; }


        /// <value>The y position of the lootable element on the map</value>
        int PosY { get; set; }


        /// <value> The name of the lootable element</value>
        string Name { get; set; }

    }
}