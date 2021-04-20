using RogueSharp;
using RogueSharp.MapCreation;
using System;

using RogueLike.Core;
using System.Collections.Generic;

namespace RogueLike.Systems
{

    public class MapGenerator
    {

        private readonly int mapWidth;

        private readonly int mapHeight;

        private int difficultyLevel;

        private readonly CurrentMap map;

        private Random random;

        public MapGenerator(int width, int height, int difficultyLevel)
        {
            mapWidth = width;
            mapHeight = height;
            this.difficultyLevel = difficultyLevel;
            map = new CurrentMap();
            random = new Random();

        }

        public CurrentMap CreateCaveMap(Player player)
        {
            IMapCreationStrategy<Map> mapCreationStrategy = new CaveMapCreationStrategy<Map>(mapWidth, mapHeight, 45, 4, 2);
            Map caveMap = Map.Create(mapCreationStrategy); // Create a cave style map
            map.Initialize(mapWidth, mapHeight);
            map.Copy(caveMap); // Copy the cave map into the current map
            PlacePlayerInMap(player); // Place the player into the map
            return map;
        }


        // Create a map that looks like a spaceship 
        public CurrentMap CreateSpaceship(Player player){
            map.Initialize(mapWidth,mapHeight);
            foreach (Cell cell in map.GetAllCells())
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, false); //(x,y,istransparent,iswalkable,isexplored)
            }

        
            //create the middle part of the spaceship
            foreach (Cell cell in map.GetCellsInSquare((mapWidth/2),(mapHeight/2),8))
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true); //(x,y,istransparent,iswalkable,isexplored)
            }
            foreach (Cell cell in map.GetBorderCellsInSquare((mapWidth/2),(mapHeight/2),8))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true); //(x,y,istransparent,iswalkable,isexplored)
            }


            // The left part of the spaceship
            foreach (Cell cell in map.GetBorderCellsInSquare(mapWidth/2-4,mapHeight/2,7))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true); //(x,y,istransparent,iswalkable,isexplored)
            }
            foreach (Cell cell in map.GetCellsInSquare(mapWidth/2-4,mapHeight/2,7))
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true); //(x,y,istransparent,iswalkable,isexplored)
            }

            
            // The right part of the spaceship
            foreach (Cell cell in map.GetBorderCellsInDiamond(mapWidth/2+8,mapHeight/2,7))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true); //(x,y,istransparent,iswalkable,isexplored)
            }
            foreach (Cell cell in map.GetCellsInDiamond(mapWidth/2+8,mapHeight/2,7))
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true); //(x,y,istransparent,iswalkable,isexplored)
            }

            //TODO : ajouter les portails de téléportation
            //TODO : ajouter les pnj

            

            player.Move(mapWidth/2, mapHeight/2);
            map.AddPlayerOnTheMap(player);
            return map;

        }


        

        private void PlacePlayerInMap(Player player)
        {
            int posX;
            int posY;

            do
            {
                posX = random.Next(0, mapWidth);
                posY = random.Next(0, mapHeight);
            } while (!map.IsWalkable(posX, posY));

            player.Move(posX, posY);
            map.AddPlayerOnTheMap(player);
            PlaceEnemyInMap();
        }

        private void PlaceEnemyInMap()
        {

            int nbMaxEnemy = ((mapWidth * mapHeight) / 200) * (difficultyLevel);
            for (int i = 0; i < nbMaxEnemy; i++)
            {
                if (random.Next(0, 2) == 1)
                { // 50% to create an enemy
                    int x;
                    int y;
                    do
                    {
                        x = random.Next(0, mapWidth);
                        y = random.Next(0, mapHeight);
                    } while (!map.IsWalkable(x, y));

                    Enemy enemy = new Zombie(difficultyLevel);
                    enemy.PosX = x;
                    enemy.PosY = y;
                    map.AddEnemy(enemy);

                }
            }
        }
    }
}
