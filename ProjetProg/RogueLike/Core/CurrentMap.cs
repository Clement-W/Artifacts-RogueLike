using RLNET;
using RogueSharp;
using System.Collections.ObjectModel;
using System.Linq;

namespace RogueLike.Core
{

    public class CurrentMap : Map
    {

        // Called when the map is updated
        // Draw the characters on each cell
        public void Draw(RLConsole mapConsole, RLConsole statConsole)
        {
            foreach (Cell cell in GetAllCells())
            {
                DrawCell(mapConsole, cell);
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

                // The actual position of the character is now walkable
                SetCellWalkability(posX, posY, true);

                // Set the new position of the character
                character.PosX = posX;
                character.PosY = posY;

                // It's last position is now not walkable
                SetCellWalkability(character.PosX, character.PosY,false);

                // Update the fov if the character is the player
                if(character is Player){
                    UpdatePlayerFieldOfView(character as Player);
                }
                return true;
            }

            return false;
        }

        public void AddPlayerOnTheMap(Player player){
            SetCellWalkability(player.PosX,player.PosY,false);
            UpdatePlayerFieldOfView(player);
        }
    }
}