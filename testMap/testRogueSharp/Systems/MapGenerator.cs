using RogueSharp;
using testRogueSharp.Core;
using RogueSharp.MapCreation;
using RogueSharp.DiceNotation;
using System;

namespace testRogueSharp.Systems
{

    public class MapGenerator
    // Si on veut faire une map à la main
    {
        private readonly int width;
        private readonly int height;

        private readonly DungeonMap map;

        public MapGenerator(int w, int h)
        {
            width = w;
            height = h;
            map = new DungeonMap();
        }

        //Generation d'une map qui est juste une grande pièce
        /*public DungeonMap CreateMap()
        {
            //Initialisation des cell de la map, elles sont toutes walkables, transparentes et visitées.
            map.Initialize(width, height);
            foreach (Cell cell in map.GetAllCells())
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true); //(x,y,istransparent,iswalkable,isexplored)
            }

            //On met les bords haut et bas en non transparant ni walkable (mur)
            foreach (Cell cell in map.GetCellsInRows(0, height - 1))
            { //on prend la première et la dernière
                map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            //pareil avec les bords côtés gauche et droit
            foreach (Cell cell in map.GetCellsInColumns(0, width - 1))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }
            return map;
        }*/

        public DungeonMap CreateCaveMap()
        {
            IMapCreationStrategy<Map> mapCreationStrategy = new CaveMapCreationStrategy<Map>(width, height, 45, 4, 2);
            Map caveMap = Map.Create(mapCreationStrategy); // on créé une map de type cave
            map.Initialize(width, height);
            map.Copy(caveMap); //on copie la map cave dans notre map dungeon
            PlacePlayer(); // POUR PLACER le joueur dans une case walkable, on le fait ici
            return map;
        }

        //Place le joueur aléatoirement sur la map créée
        private void PlacePlayer()
        {
            Player player = Game.Player;
            if (player == null)
            {
                player = new Player();
            }


            int x;
            int y;
            do
            {
                x = Game.Random.Next(0, width);
                y = Game.Random.Next(0, height);
            } while (!map.IsWalkable(x, y));

            player.X = x;
            player.Y = y;
            map.AddPlayer(player);
            PlaceMonsters();

        }

        private void PlaceMonsters()
        {
            //Le nombre de monstre est décidé aléatoirement en fonction de la taille de la map 
            // ( on veut au moins 1 monstre tous les 100 pixels carrés)
            int nbMonstresPotentiels = (width * height) / 100;
            for (int i = 0; i < nbMonstresPotentiels; i++)
            {
                if (Dice.Roll("1D6") > 3)
                { // Une chance sur deux qu'il y ait le monstre
                    int x;
                    int y;
                    do
                    {
                        x = Game.Random.Next(0, width);
                        y = Game.Random.Next(0, height);
                    } while (!map.IsWalkable(x, y));

                    Monster monster = Kobold.Create(1); //temporairement on met 1 seul niveau du dongeon
                    monster.X = x;
                    monster.Y = y;
                    map.AddMonster(monster);

                }
            }
        }

    }

}