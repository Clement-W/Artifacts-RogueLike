using RLNET;
using RogueSharp;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using RogueLike.Interfaces;
using System.Threading;
using System.Diagnostics;
using System;
namespace RogueLike.Core
{

    public class CurrentMap : Map
    {
        public List<IAnimated> AnimatedSprites { get; set; }

        public List<Enemy> Enemies { get; private set; }

        public List<ILoot> Loots { get; private set; }

        public List<Merchant> Merchants { get; private set; }

        public Staircase Staircase { get; set; } // To go deeper in the map

        public List<TeleportationPortal> TeleportationPortals { get; private set; }

        public List<ICell> AttackedCells { get; set; } // To save which cells are attacked by the player (used to change the appearance of those cells)

        public Location Location { get; set; }




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


        public List<Enemy> GetEnemies()
        {
            return Enemies;
        }

        // check if the given cell is in the list of the attacked cells
        private bool IsCellAtacked(ICell cell)
        {
            lock (AttackedCells) // Lock the list to avoid that another thread access or modify it at the same time to avoid multi-threading errors

            {
                foreach (Cell attCell in AttackedCells)
                {
                    if (cell.X == attCell.X && cell.Y == attCell.Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // Called when the map is updated
        // Draw the characters on each cell
        public void Draw(RLConsole mapConsole)
        {
            foreach (Cell cell in GetAllCells())
            {

                if (!IsCellAtacked(cell))
                {
                    DrawCell(mapConsole, cell);
                }
                else
                {
                    lock (AttackedCells) // Lock the list while we're drawing a cell of this list
                    {
                        DrawCellWithColor(mapConsole, cell, Colors.AttackedCell);
                    }
                }

            }

            foreach (ILoot loot in Loots)
            {
                IDrawable drawableLoot = loot as IDrawable;
                drawableLoot.Draw(mapConsole, this);
            }

            if (Staircase != null)
            {
                Staircase.Draw(mapConsole, this);
            }

            foreach (Merchant merchant in Merchants)
            {
                merchant.Draw(mapConsole, this);
                merchant.DrawStall(mapConsole, this);
            }

            foreach (TeleportationPortal portal in TeleportationPortals)
            {
                portal.Draw(mapConsole, this);
            }

            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(mapConsole, this);
            }

            

            
        }


        //TODO : Ajouter d'autres couleurs sur certains murs pour changer
        //TODO : Différencier cette méthode en fonction du type de planète (la couleur du background change par exemple )
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


        // Draw the cell with the specified color (used when a cell is attacked by the player)
        private void DrawCellWithColor(RLConsole mapConsole, Cell cell, RLColor color)
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

        // Set the field of view of the player according to it's awareness ()
        public void UpdatePlayerFieldOfView(Player player)
        {
            int radius;
            if (Location.MapType == MapType.BossRoom || Location.MapType == MapType.Spaceship)
            {
                radius = Width;
            }
            else
            {
                radius = player.Awareness;
            }
            ReadOnlyCollection<ICell> cellsFov = ComputeFov(player.PosX, player.PosY, radius, true); // Get the cells in the player fov
            foreach (Cell cell in cellsFov)
            {
                SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
            }
        }

        public void SetCellWalkability(int x, int y, bool isWalkable)
        {
            ICell cell = GetCell(x, y);
            SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
        }

        public bool CheckLootCollectability(Player player, int posX, int posY)
        {
            bool lootCanBeWalkedOn = true;
            //Before moving and collect the item, check if it's in a merchant stall
            ILoot loot = GetLootAt(posX, posY);

            if (loot != null && loot is ISellable)
            {
                ISellable sellable = loot as ISellable;
                if (sellable.SoldByMerchant != null) // If the item is in a merchant stall
                {
                    if (player.Gold >= sellable.Cost)
                    { // if the player have enough moneyf (

                        if (CollectLoot(player, posX, posY)) // collect the item if possible
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
                        Game.MessageLog.AddMessage("You don't have enough gold");
                        Game.MessageLog.AddMessage($"You need {sellable.Cost - player.Gold} more");
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

        public void AddPlayerOnTheMap(Player player)
        {
            SetCellWalkability(player.PosX, player.PosY, false);
            UpdatePlayerFieldOfView(player);
        }

        public void AddEnemy(Enemy enemy)
        {
            Enemies.Add(enemy);
            SetCellWalkability(enemy.PosX, enemy.PosY, false);
        }

        public void RemoveEnemy(Enemy enemy)
        {
            Enemies.Remove(enemy);
            SetCellWalkability(enemy.PosX, enemy.PosY, true);
        }

        public void AddMerchant(Merchant merchant)
        {
            Merchants.Add(merchant);
            SetCellWalkability(merchant.PosX, merchant.PosY, false);
            AnimatedSprites.Add(merchant);
        }


        public Enemy GetEnemyAt(int posX, int posY)
        {
            return Enemies.FirstOrDefault(enemy => (enemy.PosX == posX && enemy.PosY == posY));
        }


        public TeleportationPortal GetTeleportationPortalAt(int posX, int posY)
        {
            return TeleportationPortals.FirstOrDefault(portal => (portal.PosX == posX && portal.PosY == posY));
        }

        public ILoot GetLootAt(int posX, int posY)
        {
            return Loots.FirstOrDefault(loot => (loot.PosX == posX && loot.PosY == posY));
        }

        public bool PlayerIsOnStairCase(Player player)
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

        public bool PlayerIsOnTeleportationPortal(Player player)
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

        public void AddLoot(ILoot loot)
        {
            if (loot is Gold)
            {
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

            // On laisse bien le loot walkable
        }

        // Collect a loot if there is one
        public bool CollectLoot(Player player, int posX, int posY)
        {
            ILoot loot = GetLootAt(posX, posY);
            ISellable sellableLoot = loot as ISellable; // To check if it's on a merchant's stall


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


        public void AddTeleportationPortal(TeleportationPortal portal)
        {
            TeleportationPortals.Add(portal);
            AnimatedSprites.Add(portal);
        }

        // Find a walkable cell around the active character
        public Cell FindClosestWalkableCell(ActiveCharacter activeCharacter)
        {
            int distanceMax = 5; // max distance from which we search for a cell
            for (int i = 1; i < distanceMax; i++)
            {
                foreach (Cell cell in GetBorderCellsInSquare(activeCharacter.PosX, activeCharacter.PosY, i))
                {
                    if (cell.IsWalkable && GetLootAt(cell.X,cell.Y)==null) // To avoid item superposition if the cell is walkable but an item is on it
                    {
                        return cell;
                    }
                }
            }
            return null;
        }

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