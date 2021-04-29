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

        public Staircase Staircase { get; set; } // To go deeper in the map

        public CurrentMap()
        {
            enemies = new List<Enemy>();
            loots = new List<ILoot>();
        }


        public List<Enemy> GetEnemies()
        {
            return enemies;
        }

        // Called when the map is updated
        // Draw the characters on each cell
        public void Draw(RLConsole mapConsole, RLConsole statConsole)
        {
            foreach (Cell cell in GetAllCells())
            {
                DrawCell(mapConsole, cell);
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


            Staircase.Draw(mapConsole, this);
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

        // Set the field of view of the player according to it's awareness ()
        public void UpdatePlayerFieldOfView(Player player)
        {
            ReadOnlyCollection<ICell> cellsFov = ComputeFov(player.PosX, player.PosY, player.Awareness, true); // Get the cells in the player fov
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

        public bool SetCharacterPosition(Character character, int posX, int posY)
        {
            // If the desired position is walkable
            if (GetCell(posX, posY).IsWalkable)
            {
                if (character is Player)
                {
                    CollectLoot(character as Player, posX, posY);
                }

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

        public Enemy GetEnemyAt(int posX, int posY)
        {
            return enemies.FirstOrDefault(enemy => (enemy.PosX == posX && enemy.PosY == posY));
        }

        public ILoot GetLootAt(int posX, int posY)
        {
            return loots.FirstOrDefault(loot => (loot.PosX == posX && loot.PosY == posY));
        }

        public bool CanMoveToNextLevel(Player player)
        {
            return Staircase.PosX == player.PosX && Staircase.PosY == player.PosY;
        }

        public void AddLoot(ILoot loot)
        {
            loots.Add(loot);
            // On laisse bien le loot walkable
        }

        // Collect a loot if there is one
        public void CollectLoot(Player player, int posX, int posY)
        {
            ILoot loot = GetLootAt(posX,posY);
            

            if (loot != null && player.Collect(loot, this))
            {
                if (loot is Equipment)
                {
                    Equipment lootEquipment = loot as Equipment;
                    Game.MessageLog.AddMessage("You found " + lootEquipment.Name);
                }

                if(loot is Gold){
                    Gold gold = loot as Gold;
                    Game.MessageLog.AddMessage("You found " + gold.Amount + " $");
                }

                loots.Remove(loot);
            }
        }


    }
}