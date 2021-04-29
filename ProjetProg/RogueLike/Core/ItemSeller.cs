using System;
using System.Collections.Generic;
namespace RogueLike.Core
{
    public class ItemSeller : Merchant
    {
        public ItemSeller(int posX, int posY, int merchantLevel) : base(merchantLevel)
        {
            Name = "Item Seller";
            PrintedColor = Colors.Seller;
            Symbol = '(';
            PosX = posX;
            PosY = posY;

        }

        // Called in the constructor of Merchant
        public override void GenerateStall()
        {
            Random random = new Random();
            List<Item> possibleItem = null;
            switch (MerchantLevel)
            {
                case 0:
                    possibleItem = new List<Item>(new Item[] {new OldSandwich(), new MedicinaHerb() });
                    break;
                case 1:
                    possibleItem = new List<Item>(new Item[] { new OldSandwich(), new MedicinaHerb(), new Bandage(), new Ration() });
                    break;
                case 2:
                    possibleItem = new List<Item>(new Item[] { new OldSandwich(),new Ration(), new MedicinaHerb()});
                    break;

            }
            // Pick 3 piece of Item in the list
            if (possibleItem != null)
            {
                Item item1 = possibleItem[random.Next(0, possibleItem.Count)];
                //Here, it's possible to have 2 equal items on the stall

                Item item2 = possibleItem[random.Next(0, possibleItem.Count)];
   

                Item item3 = possibleItem[random.Next(0, possibleItem.Count)];
              

                Stall.Add(item1);
                Stall.Add(item2);
                Stall.Add(item3);
            }

        }
    }
}