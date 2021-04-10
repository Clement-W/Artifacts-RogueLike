using RLNET;
using RogueSharp;

namespace testRogueSharp.Core
{

    // map de donjon custom qui hérite de la classe Map de roguesharp
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
        // Elle permet de dessiner des caracteres sur chaque cell
        public void Draw(RLConsole mapConsole)
        {
            mapConsole.Clear();
            foreach (Cell cell in GetAllCells())
            {
                SetConsoleSymbolForCell(mapConsole, cell);
            }
        }

        //Méthode pour mettre à jour la fov dès que le joueur se déplacera
        public void UpdatePlayerFieldOfView()
        {
            Player player = Game.Player;
            ComputeFov(player.X, player.Y, player.Awareness, true);
            // On marque toutes les cells du champ de vision comme explorées
            foreach (Cell cell in GetAllCells())
            {
                if (IsInFov(cell.X, cell.Y))
                {
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }


        //Change le parametre isWalkable d'une cell donnée
        public void SetIsWalkable(int x, int y, bool isWalkable)
        {
            ICell cell = GetCell(x, y);
            SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
        }

        //Retourne true si on peut placer un actor sur une cell, false sinon
        //SI on peut déplacer l'actor, on le fait
        public bool SetActorPosition(Actor actor, int x, int y)
        {

            //on autorise le déplacement que si la cell est walkable
            if (GetCell(x, y).IsWalkable)
            {
                SetIsWalkable(actor.X, actor.Y, true);
                //on déplace l'actor
                actor.X = x;
                actor.Y = y;
                //La cell sur laquelle est l'actor devient alors non walkable
                SetIsWalkable(actor.X, actor.Y, false);
                // on met à jour le champ de vision
                if (actor is Player)
                {
                    UpdatePlayerFieldOfView();
                }
                return true;
            }
            return false;
        }
    }
}