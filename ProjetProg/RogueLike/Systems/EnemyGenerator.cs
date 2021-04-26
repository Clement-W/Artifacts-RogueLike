using RogueLike.Core;
using System;
using System.Collections.Generic;

namespace RogueLike.Systems
{
    public class EnemyGenerator
    {

        // Create an enemy among every possibles enemy
        public static Enemy CreateEnemy(int difficultyLevel, int posX, int posY)
        {
            Random random = new Random();
            List<Enemy> possibleEnemies = new List<Enemy>();
            
            if (difficultyLevel <= 2)
            {
                possibleEnemies.Add(new Zombie(difficultyLevel));
            }
            else if(difficultyLevel >2)
            {
                possibleEnemies.Add(new Zombie(difficultyLevel));
            }

            Enemy enemy = possibleEnemies[random.Next(0, possibleEnemies.Count)];
            enemy.PosX = posX;
            enemy.PosY = posY;
            return enemy;
        }
    }
}