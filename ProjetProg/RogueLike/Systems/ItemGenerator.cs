using System;
using System.Collections.Generic;

using RogueLike.Core.Items;
using RogueLike.Interfaces;

namespace RogueLike.Systems
{
    /// <summary>
    /// This class is used to generate random item in the map
    /// </summary>
    public class ItemGenerator : IDrawableGenerator
    {


        /// <summary>
        /// Create a random item among the possible items
        /// </summary>
        /// <param name="difficultyLevel"> The difficluty level of the game that is computed in mapCreation</param>
        /// <param name="posX"> The x position of the item</param>
        /// <param name="posY"> The y position of the item</param>
        /// <returns>Return the created item</returns>
        public IDrawable Create(int difficultyLevel,int posX, int posY)
        { // For now, the difficulty level is not used because we don't have enough items

            Random random = new Random();

            // List of every possible items
            List<Item> possibleItems = new List<Item>(new Item[] { new OldSandwich(), new Ration(), new HealthKit(), new Bandage(), new MedicinalHerb() });

            // Take one randomly
            Item item = possibleItems[random.Next(0, possibleItems.Count)];
            item.PosX = posX;
            item.PosY = posY;
            return item;

        }
    }
}