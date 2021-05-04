using RogueLike.Interfaces;
using RLNET;
using RogueSharp;
using RogueLike.Core.Merchants;
namespace RogueLike.Core.Equipments
{
    public abstract class Equipment : IDrawable, ILoot, ISellable
    {

        public RLColor PrintedColor { get; set; }

        public char Symbol { get; set; }

        public int PosX { get; set; }

        public int PosY { get; set; }


        public Merchant SoldByMerchant{get;set;}


        public string Name { get; set; }

        public int Cost{get;set;}


        // Implicitly called
        public Equipment()
        {
            PrintedColor = Palette.DbLight;
            SoldByMerchant=null;
        }

        public void Draw(RLConsole console, CurrentMap map)
        {
            // If it has never been explored, don't draw it
            if (map.GetCell(PosX, PosY).IsExplored)
            {

                // draw it differently if it's the fov or not
                if (map.IsInFov(PosX, PosY))
                {
                    // Draw it with the floor fov background color
                    console.Set(PosX, PosY, PrintedColor, map.Location.FloorBackgroundColorInFov, Symbol);
                }
                else
                {
                    // Draw it with the floor background and a '.' symbol
                    console.Set(PosX, PosY, PrintedColor, map.Location.FloorBackgroundColor, map.Location.FloorSymbol);
                }
            }
        }

        
    }
}