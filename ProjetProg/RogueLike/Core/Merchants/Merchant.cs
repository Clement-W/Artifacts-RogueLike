using System.Collections.Generic;
using RogueLike.Interfaces;
using RLNET;
using RogueSharp;
using System.Linq;

namespace RogueLike.Core.Merchants
{
    public abstract class Merchant : Character, IAnimated
    {
        public char AlternateSymbol1 { get; set; }
        public char AlternateSymbol2 { get; set; }

        public int MerchantLevel { get; set; } // equal to the number of artefacts found by the player

        public Dictionary<int, ISellable> Stall { get; set; } // The sellable items proposed by the merchant
        // The value of the dictionnary is the sellable, and the key is it's position on the stall
        // We don't use a list because the index of the element change with respect of the list size

        public Merchant(int merchantLevel)
        {
            Stall = new Dictionary<int, ISellable>();
            MerchantLevel = merchantLevel;
            GenerateStall();
        }

        // called perdiodically to animate the merchants
        public void ChangeSymbolAlternative()
        {
            Symbol = (Symbol == AlternateSymbol1) ? AlternateSymbol2 : AlternateSymbol1;
        }

        public abstract void GenerateStall();


        public void DrawStall(RLConsole console, CurrentMap map)
        {
            // Draw the items below the mechants   

            foreach (KeyValuePair<int, ISellable> sellable in Stall)
            {
                ISellable sellableLoot = sellable.Value;
                int lootPosition = sellable.Key;
                sellableLoot.PosX = PosX + (lootPosition - 1);
                // If the item position is 0, the item will be displayed on bottom left of the merchant (posX+(0-1))
                // At index 1, the item is displayed just below the merchant (posX+(1-1))
                // At index 2, the item is displayed on bottom right of the merchant (posX+(2-1))

                sellableLoot.PosY = PosY + 1;
            }

        }

        public void SellItem(ISellable sellable)
        {
            foreach (KeyValuePair<int, ISellable> sellableInStall in Stall)
            {
                if (sellableInStall.Value.PosX == sellable.PosX && sellableInStall.Value.PosY == sellable.PosY)
                {
                    Stall.Remove(sellableInStall.Key);
                    sellable.SoldByMerchant = null;
                    //Game.MessageLog.AddMessage("Thanks for buying this.");
                    break;
                }
            }



        }








    }
}