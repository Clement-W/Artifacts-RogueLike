using System;
using System.Collections.Generic;
using RogueLike.Core;
namespace RogueLike.Systems
{
    public class EquimentGenerator
    {
        public static Equipment CreateEquipment(int difficultyLevel, int posX, int posY)
        {

            // TODO : faire ça avec des progressions cohérentes (évaluer aussi la difficulté sur telle ou telle planète)
            Random random = new Random();
            List<Equipment> possibleEquipments = new List<Equipment>();

            if (difficultyLevel >= 0)
            {
                possibleEquipments.Add(Boots.Leather());
                possibleEquipments.Add(Leggins.Leather());
                possibleEquipments.Add(Chestplate.Leather());
                possibleEquipments.Add(Helmet.Leather());

                possibleEquipments.Add(Dagger.Wood());
                possibleEquipments.Add(Sword.Wood());
                possibleEquipments.Add(Spear.Wood());
        
                possibleEquipments.Add(Boots.Iron());
                possibleEquipments.Add(Leggins.Iron());
                possibleEquipments.Add(Chestplate.Iron());
                possibleEquipments.Add(Helmet.Iron());

                possibleEquipments.Add(Dagger.Iron());
                possibleEquipments.Add(Sword.Iron());
                possibleEquipments.Add(Spear.Iron());
            }

            Equipment equipment = possibleEquipments[random.Next(0, possibleEquipments.Count)];
            equipment.PosX = posX;
            equipment.PosY = posY;
            return equipment;

       

        }
    }
}