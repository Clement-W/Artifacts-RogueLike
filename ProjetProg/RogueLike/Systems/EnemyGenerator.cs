using RogueLike.Core;
using System;

namespace RogueLike.Systems
{
    public class EnemyGenerator
    {
        
        // Create an enemy among every possibles enemy
        public static Enemy CreateEnemy(int difficultyLevel,int posX, int posY){
            Random random = new Random();
            Enemy[] possibleEnemies = {new Zombie(difficultyLevel)};
            Enemy enemy = possibleEnemies[random.Next(0,possibleEnemies.Length)];
            enemy.PosX = posX;
            enemy.PosY = posY;
            return enemy;
        }
    }
}