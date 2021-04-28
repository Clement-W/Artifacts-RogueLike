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
                possibleEquipments.Add(Boots.Polymer());
                possibleEquipments.Add(Leggins.Polymer());
                possibleEquipments.Add(Chestplate.Polymer());
                possibleEquipments.Add(Helmet.Polymer());

                possibleEquipments.Add(Knife.Mk1());
                possibleEquipments.Add(Sword.Mk1());
                possibleEquipments.Add(Spear.Mk1());
        
                possibleEquipments.Add(Boots.Carbon());
                possibleEquipments.Add(Leggins.Carbon());
                possibleEquipments.Add(Chestplate.Carbon());
                possibleEquipments.Add(Helmet.Carbon());

                possibleEquipments.Add(Knife.Mk3());
                possibleEquipments.Add(Sword.Mk3());
                possibleEquipments.Add(Spear.Mk3());
            }

            Equipment equipment = possibleEquipments[random.Next(0, possibleEquipments.Count)];
            equipment.PosX = posX;
            equipment.PosY = posY;
            return equipment;

       

        }
    }
}