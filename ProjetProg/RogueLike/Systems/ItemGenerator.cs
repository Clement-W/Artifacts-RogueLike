using System;
using System.Collections.Generic;
using RogueLike.Core;
namespace RogueLike.Systems
{
    public class ItemGenerator
    {
        public static Item CreateItem(int posX, int posY)
        {
            Random random = new Random();
           

            List<Item> possibleItems = new List<Item>(new Item[]{new OldSandwich(),new Ration(), new HealthKit(), new Bandage(), new MedicinaHerb()});

            Item item = possibleItems[random.Next(0, possibleItems.Count)];
            item.PosX = posX;
            item.PosY = posY;
            return item;

        }
    }
}