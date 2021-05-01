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
                default:
                    possibleItem = new List<Item>(new Item[] { new OldSandwich(),new Ration(), new MedicinaHerb(),new Bandage()});
                    break;
                case 2:
                    possibleItem = new List<Item>(new Item[] { new OldSandwich(),new Ration(), new MedicinaHerb(),new Bandage(), new HealthKit()});
                    break;

            }
            // Pick 3 piece of Item in the list
            if (possibleItem != null)
            {
                Item item1 = possibleItem[random.Next(0, possibleItem.Count)];
                possibleItem.Remove(item1);
               
                Item item2 = possibleItem[random.Next(0, possibleItem.Count)];
                possibleItem.Remove(item2);

                Item item3 = possibleItem[random.Next(0, possibleItem.Count)];
                possibleItem.Remove(item3);


                Stall.Add(0,item1);
                Stall.Add(1,item2);
                Stall.Add(2,item3);
            }

        }
    }
}