using System;
using System.Linq;
using System.Collections.Generic;

using RogueSharp;
using RogueSharp.MapCreation;

using RogueLike.Core.Enemies;
using RogueLike.Core.Merchants;
using RogueLike.Core.Items;
using RogueLike.Core.Equipments;
using RogueLike.Interfaces;
using RogueLike.Core;

namespace RogueLike.Systems
{
    /// <summary>
    /// This class manage the map creation system.
    /// </summary>
    public class MapCreation
    {

        /// <summary>
        /// The map width
        /// </summary>
        private readonly int mapWidth;

        /// <summary>
        /// The map height
        /// </summary>
        private readonly int mapHeight;

        /// <summary>
        /// The difficulty level is equal to the sum of the number of artifacts 
        /// collected by the player, and the map level. This allow the difficulty level to
        /// increase, when the player collects artifacts.
        /// </summary>
        private int difficultyLevel;

        /// <summary>
        /// This is the map level. The map level increase when the player goes
        /// deeper in the map
        /// </summary>
        private int mapLevel;

        /// <summary>
        /// This is the number of artifacts collected by the player
        /// </summary>
        private int nbArtifactsCollected;

        /// <summary>
        /// This is the created map that will be returned by the MapCreation
        /// </summary>
        private readonly CurrentMap map;

        /// <summary>
        /// This is the Random instance that is used several times in this class
        /// </summary>
        private Random random;


        /// <summary>
        /// The max number of items that can spawn on the map
        /// </summary>
        private const int NB_MAX_ITEM=10;

        /// <summary>
        /// The max number of equipments that can spawn on the map
        /// </summary>
        private const int NB_MAX_EQUIPMENT=10;

        /// <summary>
        /// This constructor initialize the MapCreation attributes.
        /// </summary>
        /// <param name="width">The map width</param>
        /// <param name="height">The map height</param>
        /// <param name="level">The map level</param>
        /// <param name="nbArtifacts">The number of artifacts collected by the player</param>
        public MapCreation(int width, int height, int level, int nbArtifacts)
        {
            mapWidth = width;
            mapHeight = height;
            mapLevel = level;
            nbArtifactsCollected = nbArtifacts;
            difficultyLevel = nbArtifactsCollected + level;
            map = new CurrentMap();
            random = new Random();
            System.Console.WriteLine("diff: " +difficultyLevel);
        }

        /// <summary>
        /// This constructor initialize the Location attribute of the map
        /// </summary>
        // <param name="width">The map width</param>
        /// <param name="height">The map height</param>
        /// <param name="level">The map level</param>
        /// <param name="nbArtifactsCollected">The number of artifacts collected by the player</param>
        /// <param name="mapType">The map type (spaceship, boss room, planet)</param>
        /// <param name="planet">The planet type (Alleo, Damari,T haad)</param>
        public MapCreation(int width, int height, int level, int nbArtifactsCollected, MapType mapType, PlanetName planet) : this(width, height, level, nbArtifactsCollected)
        {
            map.MapLocation.MapType = mapType;
            map.MapLocation.Planet = planet;
            map.MapLocation.InitializeSprites();

        }


        /// <summary>
        /// This constructor is used when the game start, to create the spaceship map
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="level"></param>
        /// <param name="nbArtifactsCollected"></param>
        /// <param name="mapType"></param>
        /// <returns></returns>
        public MapCreation(int width, int height, int level, int nbArtifactsCollected, MapType mapType) : this(width, height, level, nbArtifactsCollected, mapType, PlanetName.None)
        {
        }

        /// <summary>
        /// This method allows to create a cave map type thanks to the cave map creation
        /// strategy from rogue sharp
        /// </summary>
        /// <param name="player">The player</param>
        /// <returns>The created map</returns>
        private CurrentMap CreateCaveMap(Player player)
        {
            IMapCreationStrategy<Map> mapCreationStrategy = new CaveMapCreationStrategy<Map>(mapWidth, mapHeight, 45, 4, 2);
            // Create a cave style map
            Map caveMap = Map.Create(mapCreationStrategy);
            // Copy the cave map into the current map
            map.Initialize(mapWidth, mapHeight);
            map.Copy(caveMap);

            // Place the player into the map
            PlacePlayerRandomlyInMap(player);
            // Place the loots into the map
            PlaceLootsInMap();
            // Create the stairs on the map
            CreateStairs(player);

            // Inform the player
            if (mapLevel == 1)
            {
                Game.Messages.AddMessage("You teleported to " + map.MapLocation.Planet.ToString());
                Game.Messages.AddMessage("at level 1");
            }
            else
            {
                Game.Messages.AddMessage("You arrive to the level " + mapLevel);
                Game.Messages.AddMessage("of the planet " + map.MapLocation.Planet.ToString());
            }
            Game.Messages.AddMessage("Find the stairs!");

            return map;
        }

        /// <summary>
        /// This method allows to create a boss room map type
        /// </summary>
        /// <param name="player">The player</param>
        /// <returns>The crated map</returns>
        private CurrentMap CreateBossRoom(Player player)
        {
            // Initialize the map cells
            map.Initialize(mapWidth, mapHeight);
            foreach (Cell cell in map.GetAllCells())
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, false); //(x,y,istransparent,iswalkable,isexplored)
            }

            // Create the middle part of the boss room
            foreach (Cell cell in map.GetCellsInDiamond((mapWidth / 2), (mapHeight / 2), 10))
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true);
            }
            foreach (Cell cell in map.GetBorderCellsInDiamond((mapWidth / 2), (mapHeight / 2), 10))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            // Place the player in the boss room
            player.SetPosition(mapWidth / 2, mapHeight / 2 + 5);
            map.AddPlayerOnTheMap(player);

            // Create the boss and place it into the map
            CreateBoss(map);

            return map;

        }

        /// <summary>
        /// Create the boss and place it into the map
        /// </summary>
        /// <param name="map">The map</param>
        private void CreateBoss(CurrentMap map)
        {
            Enemy finalBoss = null;
            Game.Messages.AddMessage("You enter the boss room");
            // Create the specific boss according to the planet
            switch (map.MapLocation.Planet)
            {
                case PlanetName.Alleo:
                    finalBoss = new AlleoBoss(difficultyLevel);
                    Game.Messages.AddMessage("Be careful, with it's trident,");
                    Game.Messages.AddMessage("this boss has a long range!");
                    break;

                case PlanetName.Damari:
                    finalBoss = new DamariBoss(difficultyLevel);
                    Game.Messages.AddMessage("Be careful, this boss can");
                    Game.Messages.AddMessage("teleport itself and makes");
                    Game.Messages.AddMessage("heavy damages!");
                    break;

                case PlanetName.Thaadd:
                    finalBoss = new ThaaddBoss(difficultyLevel);
                    Game.Messages.AddMessage("Be careful, with it's death");
                    Game.Messages.AddMessage("scythe, this boss can attack");
                    Game.Messages.AddMessage("All around itself!");
                    break;
            }

            //Place the boss into the map
            finalBoss.PosX = mapWidth / 2;
            finalBoss.PosY = mapHeight / 2 - 5;
            map.AddEnemy(finalBoss);

        }


        /// <summary>
        /// Create the map according to the map type
        /// This method is the only method that can be called by other classes
        /// </summary>
        /// <param name="player">The player</param>
        /// <returns></returns>
        public CurrentMap CreateMap(Player player)
        {
            Game.Messages = new MessageLog(); //reset the messagelog
            switch (map.MapLocation.MapType)
            {
                // Create the desired map according to the map type
                case MapType.Spaceship:
                    return CreateSpaceship(player);
                case MapType.BossRoom:
                    return CreateBossRoom(player);
                default:
                    return CreateCaveMap(player);
            }
        }




        /// <summary>
        /// Create a map that looks like a spaceship (more like a satellite)
        /// This can be considered as the player's home, the player can find teleportation
        /// portals and merchants to buy items or equipments
        /// </summary>
        /// <param name="player"></param>
        /// <returns>The created map</returns>
        private CurrentMap CreateSpaceship(Player player)
        {
            // Initialize the ells
            map.Initialize(mapWidth, mapHeight);
            foreach (Cell cell in map.GetAllCells())
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, false); //(x,y,istransparent,iswalkable,isexplored)
            }

            // Get the center coordinates of the map, that will be used to compute other coordinates
            int centerX = mapWidth / 2;
            int centerY = mapHeight / 2;
            Game.Messages.AddMessage("Welcome to your spaceship!");



            // Add the main spaceship part
            int spaceShipSize = 5;
            foreach (Cell cell in map.GetBorderCellsInDiamond(centerX, centerY, spaceShipSize))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true); //(x,y,istransparent,iswalkable,isexplored)
            }
            foreach (Cell cell in map.GetCellsInDiamond(centerX, centerY, spaceShipSize))
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true); //(x,y,istransparent,iswalkable,isexplored)
            }

            // Add the left spaceship part
            foreach (Cell cell in map.GetBorderCellsInDiamond(centerX - 7, centerY, spaceShipSize))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true); //(x,y,istransparent,iswalkable,isexplored)
            }
            foreach (Cell cell in map.GetCellsInDiamond(centerX - 7, centerY, spaceShipSize))
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true); //(x,y,istransparent,iswalkable,isexplored)
            }

            // Add the right spaceship part
            foreach (Cell cell in map.GetBorderCellsInDiamond(centerX + 7, centerY, spaceShipSize))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true); //(x,y,istransparent,iswalkable,isexplored)
            }
            foreach (Cell cell in map.GetCellsInDiamond(centerX + 7, centerY, spaceShipSize))
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true); //(x,y,istransparent,iswalkable,isexplored)
            }

            // Add the down spaceship part
            foreach (Cell cell in map.GetBorderCellsInDiamond(centerX, centerY + 5, spaceShipSize))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true); //(x,y,istransparent,iswalkable,isexplored)
            }
            foreach (Cell cell in map.GetCellsInDiamond(centerX, centerY + 5, spaceShipSize))
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true); //(x,y,istransparent,iswalkable,isexplored)
            }



            //Add the corridor that goes to the sellers spaceship
            int startCorridorX = mapWidth / 2;
            int minY = centerY - spaceShipSize;
            int endCorridorY = minY - 1;
            foreach (Cell cell in map.GetCellsAlongLine(startCorridorX, minY, startCorridorX, endCorridorY))
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true);
            }



            // Add the sellers spaceship
            int sellersSpaceShipSize = 7;
            foreach (Cell cell in map.GetBorderCellsInDiamond(startCorridorX, endCorridorY - spaceShipSize, sellersSpaceShipSize))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true); //(x,y,istransparent,iswalkable,isexplored)
            }
            foreach (Cell cell in map.GetCellsInDiamond(startCorridorX, endCorridorY - spaceShipSize, sellersSpaceShipSize))
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true); //(x,y,istransparent,iswalkable,isexplored)
            }

            // Place the sellers in the spaceship
            PlaceSellersInSpaceship(startCorridorX, endCorridorY - spaceShipSize);

            // Place the teleportation portals in the space ship
            PlaceTeleportationPortalsInSpaceship(player);

            //Place the player into the spaceship
            player.SetPosition(mapWidth / 2, mapHeight / 2);
            map.AddPlayerOnTheMap(player);
            return map;
        }

        /// <summary>
        /// Place the sellers into the spaceship
        /// </summary>
        /// <param name="sellerSpaceshipCenterX">The center x of the sellers's spaceship</param>
        /// <param name="sellerSpaceshipCenterY">The center y of the sellers's spaceship</param>
        private void PlaceSellersInSpaceship(int sellerSpaceshipCenterX, int sellerSpaceshipCenterY)
        {
            // Compute the position of the item seller
            int itemSellerPosX = sellerSpaceshipCenterX - 3;
            int itemSellerPosY = sellerSpaceshipCenterY - 1;

            // Compute the position of the equipment seller
            int equipmentSellerPosX = sellerSpaceshipCenterX + 3;
            int equipmentSellerPosY = sellerSpaceshipCenterY - 1;

            // Create the merchants
            ItemSeller itemSeller = new ItemSeller(itemSellerPosX, itemSellerPosY, nbArtifactsCollected);
            EquipmentSeller equipmentSeller = new EquipmentSeller(equipmentSellerPosX, equipmentSellerPosY, nbArtifactsCollected);

            // Place the stall as a loot in the map for the items seller
            foreach (ISellable sellable in itemSeller.Stall.Values)
            {
                sellable.SoldByMerchant = itemSeller;
                map.AddLoot(sellable as ILoot);
            }

            // Place the stall as a loot in the map for the equipment seller
            foreach (ISellable sellable in equipmentSeller.Stall.Values)
            {
                sellable.SoldByMerchant = equipmentSeller;
                map.AddLoot(sellable as ILoot);
            }

            // Place the sellers into the map
            map.AddMerchant(itemSeller);
            map.AddMerchant(equipmentSeller);


        }

        // Place teleportation portals into the map
        private void PlaceTeleportationPortalsInSpaceship(Player player)
        {

            // Center coordinates used to compute other coordinates
            int centerX = mapWidth / 2;
            int centerY = mapHeight / 2;

            
            
            // If an artifact has already been collected by te player, don't put the 
            // teleportation portal that teleport to this planet, that's why we need to save
            // the visited planets of the player
            List<PlanetName> visitedPlanets = new List<PlanetName>();
            foreach (Artifact artifact in player.ArtifactsCollected)
            {
                // A planet is considered visited when the artifacts is collected
                // by the player
                visitedPlanets.Add(artifact.ComesFrom);
            }

            // Add the teleportation portal of the non visited planets
            if (!visitedPlanets.Contains(PlanetName.Alleo))
            {
                map.AddTeleportationPortal(new PortalToPlanet1(centerX - 8, centerY));
            }
            if (!visitedPlanets.Contains(PlanetName.Damari))
            {
                map.AddTeleportationPortal(new PortalToPlanet2(centerX, centerY + 6));
            }
            if (!visitedPlanets.Contains(PlanetName.Thaadd))
            {
                map.AddTeleportationPortal(new PortalToPlanet3(centerX + 8, centerY));
            }
        }





        /// <summary>
        /// Place randomly the player into the map
        /// </summary>
        /// <param name="player">The player</param>
        private void PlacePlayerRandomlyInMap(Player player)
        {
            ICell cell = map.FindRandomWalkableCell();

            player.SetPosition(cell.X, cell.Y);
            map.AddPlayerOnTheMap(player);
            PlaceRandomEnemiesInMap();
        }

        /// <summary>
        /// Place randomly the enemies into the map
        /// </summary>
        private void PlaceRandomEnemiesInMap()
        {
            EnemyGenerator enemyGenerator = new EnemyGenerator();
            // Compute the max number of enemies
            int nbMaxEnemy = ((mapWidth * mapHeight) / 200) * (difficultyLevel);

            for (int i = 0; i < nbMaxEnemy; i++)
            {
                if (random.Next(0, 2) == 1)
                { // 50% to create an enemy

                    ICell cell = map.FindRandomWalkableCell();

                    Enemy enemy = enemyGenerator.Create(difficultyLevel, cell.X, cell.Y) as Enemy;
                    map.AddEnemy(enemy);
                }
            }
        }

        /// <summary>
        /// Place random equipments into the map
        /// </summary>
        private void PlaceRandomEquipmentsInMap()
        {
            EquipmentGenerator equipmentGenerator = new EquipmentGenerator();
            for (int i = 0; i < NB_MAX_EQUIPMENT; i++)
            {
                if (random.Next(0, 2) == 1)
                { // 50% to create an equipment

                    ICell cell = map.FindRandomWalkableCell();

                    Equipment equipment = equipmentGenerator.Create(difficultyLevel, cell.X, cell.Y) as Equipment;
                    map.AddLoot(equipment);
                }
            }
        }

        /// <summary>
        /// Place random items in the map
        /// </summary>
        private void PlaceRandomItemsInMap()
        {
            ItemGenerator itemGenerator = new ItemGenerator();
            for (int i = 0; i < NB_MAX_ITEM; i++)
            {
                if (random.Next(0, 2) == 1)
                { // 50% to create an item
                    ICell cell = map.FindRandomWalkableCell();
                    Item item = itemGenerator.Create(difficultyLevel, cell.X, cell.Y) as Item;
                    map.AddLoot(item);
                }
            }
        }


        /// <summary>
        /// Place the loots in the map
        /// </summary>
        private void PlaceLootsInMap()
        {
            PlaceRandomEquipmentsInMap();
            PlaceRandomItemsInMap();
        }

        /// <summary>
        /// Place the stairs as far as possible of the player, to go deeper in the map 
        /// </summary>
        /// <param name="player">The player</param>
        private void CreateStairs(Player player)
        {
            Cell farthestCellFromPlayer = FindFarthestPointFromPlayer(player); 
            map.Stairs = new Staircase(farthestCellFromPlayer.X, farthestCellFromPlayer.Y);
        }

        /// <summary>
        /// Find the farthest walkable cell from the player in a cave map
        /// </summary>
        /// <param name="player"> The player</param>
        /// <returns>The farthest walkable cell from the player</returns>
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





        /// <summary>
        /// Iterate over the cells in a circle starting from a point
        /// This is used to find a walkable cell in the corner of a cave map
        /// </summary>
        /// <param name="cornerX">The x position of the corner</param>
        /// <param name="cornerY">The y position of the corner</param>
        /// <returns></returns>
        private Cell FindWalkableCellAroundPoint(int cornerX, int cornerY)
        {
            // The radius of the circle (incremented in the while loop if it's not high enough)
            int radius = 2;
            bool found = false;
            Cell farthestCell = null;
            // While a walkable cell has not been found, iterate over the cells in circle
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
                // Increment the radius if the cell has not been found
                radius++;
            }
            return farthestCell;
        }
    }
}
