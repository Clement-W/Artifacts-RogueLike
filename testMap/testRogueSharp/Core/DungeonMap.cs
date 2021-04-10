using RLNET;
using RogueSharp;

namespace testRogueSharp.Core
{

    // map de donjon custop qui hérite de la classe Map de roguesharp
    public class DungeonMap : Map
    {

        // Permet de créer un affichage sur une cell de la map
        private void SetConsoleSymbolForCell(RLConsole console, Cell cell)
        {

            // Si la cell n'a pas été explorée, on fait rien
            if (!cell.IsExplored)
            {
                return;
            }

            // Si la cell est dans le champ de vision, on la dessine avec une couleur claire
            if (IsInFov(cell.X, cell.Y))
            {

                // On dessine un caractère différent en fonction du type de cell
                if (cell.IsWalkable)
                {
                    // set(x,y,couleur caractère,couleur d'arrière plan,caractere)
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


        //La méthode draw sera appelée chaque fois que la map est mise à jour
        public void Draw(RLConsole mapConsole)
        {
            mapConsole.Clear();
            foreach (Cell cell in GetAllCells())
            {
                SetConsoleSymbolForCell(mapConsole, cell);
            }
        }

        //Méthode pour mettre à jour la fov dès que le joueur se déplacera
        public void UpdatePlayerFieldOfView(){
            Player player = Game.Player;
            ComputeFov(player.X,player.Y,player.Awareness,true);
            // On marque toutes les cells du champ de vision comme explorées
            foreach(Cell cell in GetAllCells()){
                if(IsInFov(cell.X,cell.Y)){
                    SetCellProperties(cell.X,cell.Y,cell.IsTransparent,cell.IsWalkable,true);
                }
            }
        }
    }
}