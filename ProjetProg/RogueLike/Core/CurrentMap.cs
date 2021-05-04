using RLNET;
using RogueSharp;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using RogueLike.Interfaces;
using System.Threading;
using System.Diagnostics;
using System;
using RogueLike.Core.Enemies;
using RogueLike.Core.Merchants;
namespace RogueLike.Core
{

    /// <summary>
    /// This class represent the current map where the player is
    /// It inherit the Map class from RogueSharp. This allows us to use the rogue
    /// sharp method on the map (getCellsInSquare(),ComputeFov(),...)
    /// </summary>
    public class CurrentMap : Map
    {

        /// <value>
        /// This list contains every animated sprites
        /// </value>
        public List<IAnimated> AnimatedSprites { get; set; }



        /// <value>
        /// This list contains every enemies
        /// </value>
        public List<Enemy> Enemies { get; private set; }

        /// <value>
        /// This list contains the loots that are on the map
        /// </value>
        public List<ILoot> Loots { get; private set; }


        /// <value>
        /// This list contains the merchants
        /// </value>
        public List<Merchant> Merchants { get; private set; }


        /// <value>
        /// This is the staircase that can be on cave maps
        /// The staircase allow to go deeper in the map
        /// </value>
        public Staircase Staircase { get; set; }

        /// <value>
        /// This list contains every teleportation portals
        /// </value>
        public List<TeleportationPortal> TeleportationPortals { get; private set; }

        /// <value>
        /// This list contains  the attacked cells
        /// This is used to save which cells are attacked by an active character
        /// ( to change the appearance of those cells)
        /// </value>
        public List<ICell> AttackedCells { get; set; }

        /// <value>
        /// The location contains every information on the map : the maptype, the planet, ...
        /// </value>
        public Location Location { get; set; }



        /// <summary>
        /// This constructor create the map
        /// </summary>
        public CurrentMap()
        {

            AnimatedSprites = new List<IAnimated>();
            Enemies = new List<Enemy>();
            Loots = new List<ILoot>();
            AttackedCells = new List<ICell>();
            Merchants = new List<Merchant>();
            TeleportationPortals = new List<TeleportationPortal>();
            Location = new Location();

        }

        /// <summary>
        /// This method return
        /// </summary>
        /// <returns></returns>
        public List<Enemy> GetEnemies()
        {
            return Enemies;
        }


        /// <summary>
        /// Check if the given cell is in the list of the attacked cells
        /// </summary>
        /// <param name="cell">The cell to test</param>
        /// <returns>True if this cell is in the list of attacked cells</returns>
        private bool IsCellAttacked(ICell cell)
        {
            // Lock the list to avoid that another thread access or modify it at 
            // the same time to avoid multi-threading errors
            lock (AttackedCells)
            {
                foreach (Cell attCell in AttackedCells)
                {
                    // Check if the cells are at the same position
                    if (cell.X == attCell.X && cell.Y == attCell.Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// This method is called when the map is updated (in the render method)
        /// It draw the symbols on each cell
        /// </summary>
        /// <param name="mapConsole">The map console</param>
        public void Draw(RLConsole mapConsole)
        {
            foreach (Cell cell in GetAllCells())
            {

                //Check if the cell is attacked or not to draw it differently
                if (!IsCellAttacked(cell))
                {
                    DrawCell(mapConsole, cell);
                }
                else
                {
                    // Lock the list while we're drawing a cell of this list
                    lock (AttackedCells)
                    {
                        DrawAttackedCellWithColor(mapConsole, cell, Colors.AttackedCell);
                    }
                }

            }

            // Draw the loots
            foreach (ILoot loot in Loots)
            {
                IDrawable drawableLoot = loot as IDrawable;
                drawableLoot.Draw(mapConsole, this);
            }

            // Draw the staircase
            if (Staircase != null)
            {
                Staircase.Draw(mapConsole, this);
            }

            // Draw the merchants
            foreach (Merchant merchant in Merchants)
            {
                merchant.Draw(mapConsole, this);
                merchant.DrawStall(mapConsole, this);
            }

            // Draw the teleportation portals
            foreach (TeleportationPortal portal in TeleportationPortals)
            {
                portal.Draw(mapConsole, this);
            }

            // Draw the enemies
            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(mapConsole, this);
            }

        }


        /// <summary>
        /// This method draw a symbol on a cell, according to the location
        /// </summary>
        /// <param name="console">The map console</param>
        /// <param name="cell">The cell we draw on</param>
        private void DrawCell(RLConsole console, Cell cell)
        {
            // If the cell has never been explored, don't draw anything
            if (cell.IsExplored)
            {
                // Draw the cell differently if it's in the field of view of the player
                if (IsInFov(cell.X, cell.Y))
                {

                    // Draw the cell differently if it's walkable or not
                    if (cell.IsWalkable)
                    {
                        console.Set(cell.X, cell.Y, Colors.FloorFov, Location.FloorBackgroundColorInFov, Location.FloorSymbol);
                    }
                    else
                    {
                        console.Set(cell.X, cell.Y, Colors.WallFov, Location.WallBackgroundColorInFov, Location.WallSymbol);
                    }

                }
                else
                {
                    if (cell.IsWalkable)
                    {
                        console.Set(cell.X, cell.Y, Colors.Floor, Location.FloorBackgroundColor, Location.FloorSymbol);
                    }
                    else
                    {
                        console.Set(cell.X, cell.Y, Colors.Wall, Location.WallBackgroundColor, Location.WallSymbol);
                    }
                }
            }
        }


        /// <summary>
        ///  Draw the cell with the specified color (used when a cell is attacked by the player)
        /// </summary>
        /// <param name="mapConsole"> The map console</param>
        /// <param name="cell">The attacked cell</param>
        /// <param name="color">The color used when the cell is attacked</param>
        private void DrawAttackedCellWithColor(RLConsole mapConsole, Cell cell, RLColor color)
        {
            if (cell.IsWalkable)
            {
                mapConsole.Set(cell.X, cell.Y, Colors.AttackedCell, Location.FloorBackgroundColorInFov, Location.FloorSymbol);
            }
            else
            {
                mapConsole.Set(cell.X, cell.Y, Colors.WallFov, Location.WallBackgroundColorInFov, Location.WallSymbol);
            }
        }


        /// <summary>
        /// Set the field of view of the player according to it's awareness
        /// </summary>
        /// <param name="player">The player</param>
        public void UpdatePlayerFieldOfView(Player player)
        {
            int radius;
            // If the location is a boss room or the spaceship, the awareness is equal to the map width (infinite)
            // Only the walls can block the fov
            if (Location.MapType == MapType.BossRoom || Location.MapType == MapType.Spaceship)
            {
                radius = Width;
            }
            else
            {
                // Else, the radius is equal to the player awareness
                radius = player.Awareness;
            }

            // For each cells in the fov, draw them as explored cells
            ReadOnlyCollection<ICell> cellsFov = ComputeFov(player.PosX, player.PosY, radius, true); // Get the cells in the player fov
            foreach (Cell cell in cellsFov)
            {
                SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
            }
        }

        /// <summary>
        /// Change the walkability of the cell
        /// </summary>
        /// <param name="x"> The x position of the cell</param>
        /// <param name="y"> The y position of the cell</param>
        /// <param name="isWalkable"> A boolean that tells if the cell is walkable or not</param>
        public void SetCellWalkability(int x, int y, bool isWalkable)
        {
            ICell cell = GetCell(x, y);
            SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
        }

        /// <summary>
        /// Check if a loot is collectible and/or buyable
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="posX">The x position of the loot</param>
        /// <param name="posY">The y position of the loot</param>
        /// <returns></returns>
        public bool CheckLootCollectability(Player player, int posX, int posY)
        {
            // This boolean is used for the items that are on a merchant stall. If The player
            // doesn't have enough money, the player can't walk on the cell. 
            bool lootCanBeWalkedOn = true;

            //Get the loot
            ILoot loot = GetLootAt(posX, posY);

            //Before moving and collect the item, check if it's in a merchant stall
            if (loot != null && loot is ISellable)
            {
                ISellable sellable = loot as ISellable;

                // If the item is in a merchant stall
                if (sellable.SoldByMerchant != null)
                {
                    // if the player have enough money
                    if (player.Gold >= sellable.Cost)
                    {
                        // collect the item if possible
                        if (CollectLoot(player, posX, posY))
                        {
                            player.Gold -= sellable.Cost;
                            sellable.SoldByMerchant.SellItem(sellable);
                        }
                        else
                        {
                            lootCanBeWalkedOn = false;
                        }
                    }
                    else
                    {
                        // Else, the player don't have enough money, inform them.
                        Game.MessageLog.AddMessage("You don't have enough gold");
                        Game.MessageLog.AddMessage($"You need {sellable.Cost - player.Gold} more");
                        // The player can't walk on that item
                        lootCanBeWalkedOn = false;
                    }
                }
                else
                {
                    CollectLoot(player, posX, posY);
                }
            }
            else
            {
                CollectLoot(player, posX, posY);
            }
            return lootCanBeWalkedOn;
        }

        /// <summary>
        /// Set the character position on the map
        /// </summary>
        /// <param name="character">The character</param>
        /// <param name="posX">The x position</param>
        /// <param name="posY">The y position</param>
        /// <returns>Return true if the character has moved on the specified position</returns>
        public bool SetCharacterPosition(Character character, int posX, int posY)
        {
            // If the desired position is walkable
            if (GetCell(posX, posY).IsWalkable)
            {
                bool canMove = true;
                if (character is Player)
                {
                    // If there's a loot, check if it's collectible by the player
                    canMove = CheckLootCollectability(character as Player, posX, posY);

                }
                // If the player can move, move them
                if (canMove)
                {
                    // The actual position of the character is now walkable
                    SetCellWalkability(character.PosX, character.PosY, true);

                    // Set the new position of the character
                    character.PosX = posX;
                    character.PosY = posY;

                    // It's last position is now not walkable
                    SetCellWalkability(character.PosX, character.PosY, false);

                    // Update the fov if the character is the player
                    if (character is Player)
                    {
                        UpdatePlayerFieldOfView(character as Player);
                    }
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Add the player to the map
        /// </summary>
        /// <param name="player">The player</param>
        public void AddPlayerOnTheMap(Player player)
        {
            SetCellWalkability(player.PosX, player.PosY, false);
            UpdatePlayerFieldOfView(player);
        }

        /// <summary>
        /// Add an enemy to the map
        /// </summary>
        /// <param name="enemy">An enemy</param>
        public void AddEnemy(Enemy enemy)
        {
            Enemies.Add(enemy);
            SetCellWalkability(enemy.PosX, enemy.PosY, false);
        }

        /// <summary>
        /// Remove an enemy from the map
        /// </summary>
        /// <param name="enemy">An enemy</param>
        public void RemoveEnemy(Enemy enemy)
        {
            Enemies.Remove(enemy);
            SetCellWalkability(enemy.PosX, enemy.PosY, true);
        }


        /// <summary>
        /// Add a merchant to the map
        /// </summary>
        /// <param name="merchant">A merchant</param>
        public void AddMerchant(Merchant merchant)
        {
            Merchants.Add(merchant);
            SetCellWalkability(merchant.PosX, merchant.PosY, false);
            AnimatedSprites.Add(merchant);
        }


        /// <summary>
        /// Get the enemy at the specified position (if there's one)
        /// </summary>
        /// <param name="posX">The x position of the potential enemy</param>
        /// <param name="posY">The y position of the potential enemy</param>
        /// <returns>The enemy if there's one, else, it return null</returns>
        public Enemy GetEnemyAt(int posX, int posY)
        {
            return Enemies.FirstOrDefault(enemy => (enemy.PosX == posX && enemy.PosY == posY));
        }


        /// <summary>
        /// Get the teleportation portal at the specified position if there's one
        /// </summary>
        /// <param name="posX">The x position</param>
        /// <param name="posY">The y position</param>
        /// <returns>The teleportation portal if there's one, else, it return null</returns>
        public TeleportationPortal GetTeleportationPortalAt(int posX, int posY)
        {
            return TeleportationPortals.FirstOrDefault(portal => (portal.PosX == posX && portal.PosY == posY));
        }


        /// <summary>
        /// Get the loot at the specified position if there's one
        /// </summary>
        /// <param name="posX">The x position</param>
        /// <param name="posY">The y position</param>
        /// <returns>The loot if there's one, else, it return null</returns>
        public ILoot GetLootAt(int posX, int posY)
        {
            return Loots.FirstOrDefault(loot => (loot.PosX == posX && loot.PosY == posY));
        }


        /// <summary>
        /// Check if the player is on staircase
        /// </summary>
        /// <param name="player">The player</param>
        /// <returns>True if the player is on staircase</returns>
        public bool IsPlayerOnStaircase(Player player)
        {
            if (Staircase != null)
            {
                return Staircase.PosX == player.PosX && Staircase.PosY == player.PosY;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check if the player is on a teleportation portal
        /// </summary>
        /// <param name="player">The player</param>
        /// <returns>True if the player is on a teleportation portal</returns>
        public bool IsPlayerOnTeleportationPortal(Player player)
        {
            foreach (TeleportationPortal portal in TeleportationPortals)
            {
                if (portal.PosX == player.PosX && portal.PosY == player.PosY)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Add a loot to the map
        /// </summary>
        /// <param name="loot">A loot</param>
        public void AddLoot(ILoot loot)
        {
            if (loot is Gold)
            {
                // Add the gold only if the amount is > to 0
                Gold goldLoot = loot as Gold;
                if (goldLoot.Amount > 0)
                {
                    Loots.Add(loot);
                }
            }
            else
            {
                Loots.Add(loot);
            }
        }

        /// <summary>
        /// Collect a loot if there's one
        /// </summary>
        /// <param name="player">The player that collects the loot</param>
        /// <param name="posX">The x position of the loot</param>
        /// <param name="posY">The y position of the loot</param>
        /// <returns></returns>
        private bool CollectLoot(Player player, int posX, int posY)
        {
            ILoot loot = GetLootAt(posX, posY);

            // Used To check if it's on a merchant's stall
            ISellable sellableLoot = loot as ISellable;


            if (loot != null && player.Collect(loot, this))
            {

                if (loot is Gold)
                {
                    Gold gold = loot as Gold;
                    Game.MessageLog.AddMessage("You found " + gold.Amount + " gold");
                }
                else if (loot is Artifact)
                {
                    Game.MessageLog.AddMessage("You've collected " + loot.Name);
                }
                else if (sellableLoot.SoldByMerchant == null)
                {
                    Game.MessageLog.AddMessage("You found " + loot.Name);
                }
                else
                {
                    Game.MessageLog.AddMessage("You bought " + loot.Name);
                }
                Loots.Remove(loot);
                return true;

            }
            return false;
        }

        /// <summary>
        /// Add a teleportation portal to the map
        /// </summary>
        /// <param name="portal">A teleportation portal</param>
        public void AddTeleportationPortal(TeleportationPortal portal)
        {
            TeleportationPortals.Add(portal);
            AnimatedSprites.Add(portal);
        }

        /// <summary>
        /// Find the closest walkable cell around an active character
        /// </summary>
        /// <param name="activeCharacter">The active character</param>
        /// <returns>The closest walkable cell</returns>
        public Cell FindClosestWalkableCell(ActiveCharacter activeCharacter)
        {
            int distanceMax = 5; // max distance from which we search for a cell
            for (int i = 1; i < distanceMax; i++)
            {
                foreach (Cell cell in GetBorderCellsInSquare(activeCharacter.PosX, activeCharacter.PosY, i))
                {
                    if (cell.IsWalkable && GetLootAt(cell.X, cell.Y) == null) // To avoid item superposition if the cell is walkable but an item is on it
                    {
                        return cell;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Find a random walkable cell on the map
        /// </summary>
        /// <returns>The random walkable cell</returns>
        public ICell FindRandomWalkableCell()
        {
            Random random = new Random();
            int x;
            int y;
            do
            {
                x = random.Next(0, Width);
                y = random.Next(0, Height);
            } while (!IsWalkable(x, y));
            return GetCell(x, y);
        }

    }
}