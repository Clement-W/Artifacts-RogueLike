using RLNET;
using RogueSharp;
using System.Collections.Generic;
using System.Linq;

namespace testRogueSharp.Core
{

    // map de donjon custom qui hérite de la classe Map de roguesharp
    public class DungeonMap : Map
    {

        public readonly List<Monster> monsters;

        public Stairs StairsUp { get; set; }
        public Stairs StairsDown { get; set; }

        public DungeonMap()
        {
            Game.SchedulingSystem.Clear();
            monsters = new List<Monster>();
        }

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
        public void Draw(RLConsole mapConsole, RLConsole statConsole)
        {
            foreach (Cell cell in GetAllCells())
            {
                SetConsoleSymbolForCell(mapConsole, cell);
            }

            int indexMonster = 0;

            foreach (Monster monster in monsters)
            {
                monster.Draw(mapConsole, this);
                if (IsInFov(monster.X, monster.Y))
                {
                    monster.DrawStats(statConsole, indexMonster);
                    indexMonster++;
                }
            }

            StairsUp.Draw(mapConsole, this);
            StairsDown.Draw(mapConsole, this);
        }

        //Méthode pour mettre à jour la fov dès que le joueur se déplacera
        public void UpdatePlayerFieldOfView()
        {
            Player player = Game.Player;
            var cells = ComputeFov(player.X, player.Y, player.Awareness, true);
            // On marque toutes les cells du champ de vision comme explorées
            foreach (Cell cell in cells)
            {

                SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);

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

        public void AddPlayer(Player player)
        {
            Game.Player = player;
            SetIsWalkable(player.X, player.Y, false);
            UpdatePlayerFieldOfView();
            Game.SchedulingSystem.Add(player); // pour l'ajouter au schedulling system
        }

        public void AddMonster(Monster monster)
        {
            monsters.Add(monster);
            SetIsWalkable(monster.X, monster.Y, false);
            Game.SchedulingSystem.Add(monster);
        }

        public void RemoveMonster(Monster monster)
        {
            monsters.Remove(monster);
            SetIsWalkable(monster.X, monster.Y, true);
            Game.SchedulingSystem.Remove(monster);
        }

        public Monster GetMonsterAt(int x, int y)
        {
            return monsters.FirstOrDefault(m => m.X == x && m.Y == y); // on prend le monstre à ces coordonnées
        }

        public bool CanMoveDownToNextLevel()
        {
            Player player = Game.Player;
            return StairsDown.X == player.X && StairsDown.Y == player.Y;
        }


    }
}