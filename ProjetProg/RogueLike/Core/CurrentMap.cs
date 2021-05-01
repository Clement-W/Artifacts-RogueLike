using RLNET;
using RogueSharp;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using RogueLike.Interfaces;

namespace RogueLike.Core
{

    public class CurrentMap : Map
    {

        private readonly List<Enemy> enemies;

        private readonly List<ILoot> loots;

        private readonly List<Merchant> merchants;

        public Staircase Staircase { get; set; } // To go deeper in the map

        public List<ICell> AttackedCells { get; set; } // To save which cells are attacked by the player (used to change the appearance of those cells)

        public MapType MapType { get; set; }

        public CurrentMap()
        {
            enemies = new List<Enemy>();
            loots = new List<ILoot>();
            AttackedCells = new List<ICell>();
            merchants = new List<Merchant>();
        }


        public List<Enemy> GetEnemies()
        {
            return enemies;
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
        public void Draw(RLConsole mapConsole, RLConsole statConsole)
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

            foreach (ILoot loot in loots)
            {
                IDrawable drawableLoot = loot as IDrawable;
                drawableLoot.Draw(mapConsole, this);
            }

            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(mapConsole, this);
            }

            foreach (Merchant merchant in merchants)
            {
                merchant.Draw(mapConsole, this);
                merchant.DrawStall(mapConsole, this);
            }

            if (Staircase != null)
            {
                Staircase.Draw(mapConsole, this);
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
                        console.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, '.');
                    }
                    else
                    {
                        console.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#');
                    }

                }
                else
                {
                    if (cell.IsWalkable)
                    {
                        console.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, '.');
                    }
                    else
                    {
                        console.Set(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, '#');
                    }
                }
            }
        }

        // Draw the cell with the specified color (used when a cell is attacked by the player)
        private void DrawCellWithColor(RLConsole mapConsole, Cell cell, RLColor color)
        {
            if (cell.IsWalkable)
            {
                mapConsole.Set(cell.X, cell.Y, Colors.AttackedCell, Colors.FloorBackgroundFov, '.');
            }
            else
            {
                mapConsole.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#');
            }
        }

        // Set the field of view of the player according to it's awareness ()
        public void UpdatePlayerFieldOfView(Player player)
        {
            int radius;
            if (MapType == MapType.BossRoom || MapType == MapType.Spaceship)
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
                        Game.MessageLog.AddMessage("You don't have enough gold !");
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
            enemies.Add(enemy);
            SetCellWalkability(enemy.PosX, enemy.PosY, false);
        }

        public void RemoveEnemy(Enemy enemy)
        {
            enemies.Remove(enemy);
            SetCellWalkability(enemy.PosX, enemy.PosY, true);
        }

        public void AddMerchant(Merchant merchant)
        {
            merchants.Add(merchant);
            SetCellWalkability(merchant.PosX, merchant.PosY, false);
        }


        public Enemy GetEnemyAt(int posX, int posY)
        {
            return enemies.FirstOrDefault(enemy => (enemy.PosX == posX && enemy.PosY == posY));
        }

        public ILoot GetLootAt(int posX, int posY)
        {
            return loots.FirstOrDefault(loot => (loot.PosX == posX && loot.PosY == posY));
        }

        public bool PlayerIsOnStairCase(Player player)
        {
            return Staircase.PosX == player.PosX && Staircase.PosY == player.PosY;
        }

        public void AddLoot(ILoot loot)
        {
            if (loot is Gold)
            {
                Gold goldLoot = loot as Gold;
                if (goldLoot.Amount > 0)
                {
                    loots.Add(loot);
                }
            }
            else
            {
                loots.Add(loot);
            }

            // On laisse bien le loot walkable
        }

        // Collect a loot if there is one
        public bool CollectLoot(Player player, int posX, int posY)
        {
            ILoot loot = GetLootAt(posX, posY);


            if (loot != null && player.Collect(loot, this))
            {
                if (loot is Equipment)
                {
                    Equipment lootEquipment = loot as Equipment;
                    Game.MessageLog.AddMessage("You found " + lootEquipment.Name);
                }

                if (loot is Item)
                {
                    Item lootItem = loot as Item;
                    Game.MessageLog.AddMessage("You found " + lootItem.Name);
                }

                if (loot is Gold)
                {
                    Gold gold = loot as Gold;
                    Game.MessageLog.AddMessage("You found " + gold.Amount + " $");
                }

                loots.Remove(loot);
                return true;
            }
            return false;
        }



    }
}