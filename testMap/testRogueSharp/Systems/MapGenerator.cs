using RogueSharp;
using testRogueSharp.Core;

namespace testRogueSharp.Systems
{

    public class MapGenerator
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
        public DungeonMap CreateMap()
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
        }

    }

}