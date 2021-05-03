using RogueSharp;
using RogueSharp.MapCreation;
using System;
using RogueLike.Interfaces;
using RogueLike.Core;
using System.Linq;
using System.Collections.Generic;

namespace RogueLike.Systems
{

    public class MapGenerator
    {

        private readonly int mapWidth;

        private readonly int mapHeight;

        private int difficultyLevel;

        private int mapLevel;

        private int nbArtifactsCollected;

        private readonly CurrentMap map;

        private Random random;

        public MapGenerator(int width, int height, int level, int nbArtifacts)
        {
            mapWidth = width;
            mapHeight = height;
            mapLevel = level;
            nbArtifactsCollected = nbArtifacts;
            difficultyLevel = nbArtifactsCollected + level;
            map = new CurrentMap();
            random = new Random();
        }

        public MapGenerator(int width, int height, int level, int nbArtifactsCollected, MapType mapType, PlanetName planet) : this(width, height, level, nbArtifactsCollected)
        {
            map.Location.MapType = mapType;
            map.Location.Planet = planet;
            map.Location.InitializeSprites();
            Console.WriteLine("bbb");

        }

        public MapGenerator(int width, int height, int level, int nbArtifactsCollected, MapType mapType) : this(width, height, level, nbArtifactsCollected, mapType, PlanetName.None)
        {
        }

        public CurrentMap CreateCaveMap(Player player)
        {
            IMapCreationStrategy<Map> mapCreationStrategy = new CaveMapCreationStrategy<Map>(mapWidth, mapHeight, 45, 4, 2);
            Map caveMap = Map.Create(mapCreationStrategy); // Create a cave style map
            map.Initialize(mapWidth, mapHeight);
            map.Copy(caveMap); // Copy the cave map into the current map

            PlacePlayerInMap(player); // Place the player into the map
            PlaceLootsInMap();
            CreateStairs(player);

            return map;
        }

        public CurrentMap CreateBossRoom(Player player)
        {
            map.Initialize(mapWidth, mapHeight);
            foreach (Cell cell in map.GetAllCells())
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, false); //(x,y,istransparent,iswalkable,isexplored)
            }

            //create the middle part of the boss room
            foreach (Cell cell in map.GetCellsInDiamond((mapWidth / 2), (mapHeight / 2), 10))
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true); //(x,y,istransparent,iswalkable,isexplored)
            }
            foreach (Cell cell in map.GetBorderCellsInDiamond((mapWidth / 2), (mapHeight / 2), 10))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true); //(x,y,istransparent,iswalkable,isexplored)
            }


            player.SetPosition(mapWidth / 2, mapHeight / 2 + 5);
            map.AddPlayerOnTheMap(player);

            CreateBoss(map);
            //FIXME:provisoire :
            //map.AddTeleportationPortal(new PortalToSpaceship(mapWidth / 2 + 2, mapHeight / 2 + 2));
            //map.AddLoot(new Artifact(map.Location.Planet, mapWidth / 2 - 2, mapHeight / 2 - 2));
            return map;

        }

        private void CreateBoss(CurrentMap map)
        {
            Enemy finalBoss = null;
            switch (map.Location.Planet)
            {
                case PlanetName.Alleo:
                    finalBoss = new AlleoBoss(difficultyLevel);
                    break;

                case PlanetName.Damari:
                    finalBoss = new DamariBoss(difficultyLevel);
                    break;

                case PlanetName.Thaadd:
                    finalBoss = new ThaaddBoss(difficultyLevel);
                    break;
            }

            finalBoss.PosX = mapWidth / 2;
            finalBoss.PosY = mapHeight / 2 - 5;
            map.AddEnemy(finalBoss);
        }


        public CurrentMap CreateMap(Player player)
        {
            switch (map.Location.MapType)
            {
                case MapType.Spaceship:
                    return CreateSpaceship(player);
                case MapType.BossRoom:
                    return CreateBossRoom(player);
                default:
                    return CreateCaveMap(player);
            }
        }




        // Create a map that looks like a spaceship 
        public CurrentMap CreateSpaceship(Player player)
        {
            map.Initialize(mapWidth, mapHeight);
            foreach (Cell cell in map.GetAllCells())
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, false); //(x,y,istransparent,iswalkable,isexplored)
            }


            //create the middle part of the spaceship
            foreach (Cell cell in map.GetCellsInSquare((mapWidth / 2), (mapHeight / 2), 8))
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true); //(x,y,istransparent,iswalkable,isexplored)
            }
            foreach (Cell cell in map.GetBorderCellsInSquare((mapWidth / 2), (mapHeight / 2), 8))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true); //(x,y,istransparent,iswalkable,isexplored)
            }


            // The left part of the spaceship
            foreach (Cell cell in map.GetBorderCellsInSquare(mapWidth / 2 - 4, mapHeight / 2, 7))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true); //(x,y,istransparent,iswalkable,isexplored)
            }
            foreach (Cell cell in map.GetCellsInSquare(mapWidth / 2 - 4, mapHeight / 2, 7))
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true); //(x,y,istransparent,iswalkable,isexplored)
            }


            // The right part of the spaceship
            foreach (Cell cell in map.GetBorderCellsInDiamond(mapWidth / 2 + 8, mapHeight / 2, 7))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true); //(x,y,istransparent,iswalkable,isexplored)
            }
            foreach (Cell cell in map.GetCellsInDiamond(mapWidth / 2 + 8, mapHeight / 2, 7))
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true); //(x,y,istransparent,iswalkable,isexplored)
            }

            int minX = mapWidth / 2 - 4 - 7; // left border of the spaceship 
            //mapWidth/2 -4 is the x origin of the left square and -7 because the square's border are 14
            int maxY = (mapHeight / 2 + 8); // top border of the spaceship
            int minY = (mapHeight / 2 - 8);

            //Add the reactors
            foreach (Cell cell in map.GetBorderCellsInSquare(minX - 2, mapHeight / 2 + 4, 1))
            {
                map.SetCellProperties(cell.X, cell.Y, true, false, true);
            }

            foreach (Cell cell in map.GetBorderCellsInSquare(minX - 2, mapHeight / 2 - 4, 1))
            {
                map.SetCellProperties(cell.X, cell.Y, true, false, true);
            }

            //Add the corridor to the sellers spaceship
            int startCorridorX = mapWidth / 2;
            int endCorridorY = minY - 2;
            foreach (Cell cell in map.GetCellsAlongLine(startCorridorX, minY, startCorridorX, endCorridorY))
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true);
            }
            foreach (Cell cell in map.GetCellsAlongLine(startCorridorX - 1, minY, startCorridorX - 1, endCorridorY))
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true);
            }
            foreach (Cell cell in map.GetCellsAlongLine(startCorridorX + 1, minY, startCorridorX + 1, endCorridorY))
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true);
            }


            // Add the sellers spaceship
            int spaceShipSize = 7;
            foreach (Cell cell in map.GetBorderCellsInDiamond(startCorridorX, endCorridorY - spaceShipSize, spaceShipSize))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true); //(x,y,istransparent,iswalkable,isexplored)
            }
            foreach (Cell cell in map.GetCellsInDiamond(startCorridorX, endCorridorY - spaceShipSize, spaceShipSize))
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true); //(x,y,istransparent,iswalkable,isexplored)
            }



            //TODO : ajouter les portails de téléportation
            //TODO : ajouter les pnj
            PlaceSellersInSpaceship(startCorridorX, endCorridorY - spaceShipSize);

            PlaceTeleportationPortalsInSpaceship(player);


            player.SetPosition(mapWidth / 2, mapHeight / 2);
            map.AddPlayerOnTheMap(player);
            return map;
        }

        private void PlaceSellersInSpaceship(int sellerSpaceshipCenterX, int sellerSpaceshipCenterY)
        {
            int itemSellerPosX = sellerSpaceshipCenterX - 3;
            int itemSellerPosY = sellerSpaceshipCenterY - 1;

            int equipmentSellerPosX = sellerSpaceshipCenterX + 3;
            int equipmentSellerPosY = sellerSpaceshipCenterY - 1;

            ItemSeller itemSeller = new ItemSeller(itemSellerPosX, itemSellerPosY, nbArtifactsCollected);
            EquipmentSeller equipmentSeller = new EquipmentSeller(equipmentSellerPosX, equipmentSellerPosY, nbArtifactsCollected);

            foreach (ISellable sellable in itemSeller.Stall.Values)
            {
                sellable.SoldByMerchant = itemSeller;
                map.AddLoot(sellable as ILoot);
            }

            foreach (ISellable sellable in equipmentSeller.Stall.Values)
            {
                sellable.SoldByMerchant = equipmentSeller;
                map.AddLoot(sellable as ILoot);
            }

            map.AddMerchant(itemSeller);
            map.AddMerchant(equipmentSeller);

            
        }

        private void PlaceTeleportationPortalsInSpaceship(Player player)
        {

            int middlePortalX = mapWidth / 2;
            int middlePortalY = mapHeight / 2 + 5;

            List<PlanetName> visitedPlanets = new List<PlanetName>();
            // If an artifact has already been collected by te player, don't put the teleportation portal that teleport to this planet
            foreach (Artifact artifact in player.ArtifactsCollected)
            {
                visitedPlanets.Add(artifact.ComesFrom);
            }

            if (!visitedPlanets.Contains(PlanetName.Alleo))
            {
                map.AddTeleportationPortal(new PortalToPlanet1(middlePortalX - 5, middlePortalY));
            }
            if (!visitedPlanets.Contains(PlanetName.Damari))
            {
                map.AddTeleportationPortal(new PortalToPlanet2(middlePortalX, middlePortalY));
            }
            if (!visitedPlanets.Contains(PlanetName.Thaadd))
            {
                map.AddTeleportationPortal(new PortalToPlanet3(middlePortalX + 5, middlePortalY));
            }
        }





        private void PlacePlayerInMap(Player player)
        {
            ICell cell = map.FindRandomWalkableCell();

            player.SetPosition(cell.X, cell.Y);
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

                    ICell cell = map.FindRandomWalkableCell();

                    Enemy enemy = EnemyGenerator.CreateEnemy(difficultyLevel, cell.X, cell.Y);
                    map.AddEnemy(enemy);
                }
            }
        }

        private void PlaceEquipmentsInMap()
        {
            int nbMaxEquipment = 10;
            for (int i = 0; i < nbMaxEquipment; i++)
            {
                if (random.Next(0, 2) == 1)
                { // 50% to create an equipment

                    ICell cell = map.FindRandomWalkableCell();

                    Equipment equipment = EquimentGenerator.CreateEquipment(difficultyLevel, cell.X, cell.Y);
                    map.AddLoot(equipment);
                }
            }
        }

        private void PlaceItemsInMap()
        {
            int nbMaxItem = 10;
            for (int i = 0; i < nbMaxItem; i++)
            {
                if (random.Next(0, 2) == 1)
                { // 50% to create an item
                    ICell cell = map.FindRandomWalkableCell();
                    Item item = ItemGenerator.CreateItem(cell.X, cell.Y);
                    map.AddLoot(item);
                }
            }
        }


        private void PlaceLootsInMap()
        {
            PlaceEquipmentsInMap();
            PlaceItemsInMap();
        }

        // Place the stairs as far as possible of the player, to go deeper in the map 
        private void CreateStairs(Player player)
        {
            //Cell farthestCellFromPlayer = FindFarthestPointFromPlayer(player);
            //TODO: provisoirement on met à côté:
            Cell farthestCellFromPlayer = map.FindClosestWalkableCell(player);
            map.Staircase = new Staircase(farthestCellFromPlayer.X, farthestCellFromPlayer.Y);
        }

        private Cell FindFarthestPointFromPlayer(Player player)
        {
            Cell farthestCell = null;

            // Euclidian distance between the player and the 4 corners
            double distanceTopLeft = Math.Sqrt((player.PosX) ^ 2 + (player.PosY) ^ 2);
            double distanceTopRight = Math.Sqrt((player.PosX - mapWidth) ^ 2 + (player.PosY) ^ 2);
            double distanceBottomLeft = Math.Sqrt((player.PosX) ^ 2 + (player.PosY - mapHeight) ^ 2);
            double distanceBottomRight = Math.Sqrt((player.PosX - mapWidth) ^ 2 + (player.PosY - mapHeight) ^ 2);

            // Get the farthest corner 
            double maxDistance = new double[4] { distanceTopLeft, distanceTopRight, distanceBottomLeft, distanceBottomRight }.Max();

            // Get a walkable cell around the farthest corner
            if (maxDistance == distanceTopLeft)
            {
                farthestCell = FindWalkableCellAroundPoint(0, 0); // Coordinates of the top left corner
            }
            else if (maxDistance == distanceTopRight)
            {
                farthestCell = FindWalkableCellAroundPoint(mapWidth, 0);
            }
            else if (maxDistance == distanceBottomLeft)
            {
                farthestCell = FindWalkableCellAroundPoint(0, mapHeight);
            }
            else if (maxDistance == distanceBottomRight)
            {
                farthestCell = FindWalkableCellAroundPoint(mapWidth, mapHeight);
            }

            return farthestCell;
        }




        // Iterate over the cells in a circle starting from the corner
        private Cell FindWalkableCellAroundPoint(int cornerX, int cornerY)
        {
            int radius = 2;
            bool found = false;
            Cell farthestCell = null;
            while (!found)
            {
                foreach (Cell cell in map.GetCellsInCircle(cornerX, cornerY, radius))
                {
                    if (cell.IsWalkable)
                    {
                        farthestCell = cell;
                        found = true;
                        break;
                    }
                }
                radius++;
            }
            return farthestCell;
        }
    }
}
