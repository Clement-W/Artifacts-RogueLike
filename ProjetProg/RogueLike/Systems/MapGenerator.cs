using RogueSharp;
using RogueSharp.MapCreation;
using System;

using RogueLike.Core;

namespace RogueLike.Systems
{

    public class MapGenerator
    {

        private readonly int mapWidth;

        private readonly int mapHeight;

        private int difficultyLevel;

        private readonly CurrentMap map;

        public MapGenerator(int width, int height)
        {
            mapWidth = width;
            mapHeight = height;
            map = new CurrentMap();

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

        private void PlacePlayerInMap(Player player)
        {
            Random random = new Random();
            int posX;
            int posY;

            do{
                posX = random.Next(0,mapWidth);
                posY = random.Next(0,mapHeight);
            } while(!map.IsWalkable(posX,posY));

            player.Move(posX,posY);
            map.AddPlayerOnTheMap(player);
        }
    }
}
