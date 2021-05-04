using RogueLike.Core;
using System;
using System.Collections.Generic;
using RogueLike.Interfaces;
using RogueLike.Core.Enemies;

namespace RogueLike.Systems
{
    /// <summary>
    /// This class is used to generate random enemies
    /// </summary>
    public class EnemyGenerator : IDrawableGenerator
    {

        /// <summary>
        /// Create an enemy among every possibles enemy
        /// </summary>
        /// <param name="difficultyLevel"> The difficluty level of the game that is computed in mapCreation</param>
        /// <param name="posX"> The x position of the enemy</param>
        /// <param name="posY"> The y position of the enemy</param>
        /// <returns>Return the created enemy as a Drawable</returns>
        public IDrawable Create(int difficultyLevel, int posX, int posY)
        {
            Random random = new Random();
            List<Enemy> possibleEnemies = new List<Enemy>();

            // Create a pool of possible enemies
            possibleEnemies.AddRange(new Enemy[] { new Zombie(difficultyLevel), new Mecabat(difficultyLevel), new Dendroide(difficultyLevel) });

            // Take a random enemy in the possible enemies list
            Enemy enemy = possibleEnemies[random.Next(0, possibleEnemies.Count)];
            // Set it's coordinates
            enemy.PosX = posX;
            enemy.PosY = posY;
            return enemy;
        }
    }
}