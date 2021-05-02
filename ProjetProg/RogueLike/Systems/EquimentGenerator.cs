using System;
using System.Collections.Generic;
using RogueLike.Core;
namespace RogueLike.Systems
{
    public class EquimentGenerator
    {
        public static Equipment CreateEquipment(int difficultyLevel, int posX, int posY)
        {

            Random random = new Random();
            List<Equipment> possibleEquipments = new List<Equipment>();
            // If the difficulty level is high, there's more equipment in the possible equipments, because
            // it includes the low level equipment
            if (difficultyLevel <=2)
            {
                possibleEquipments.AddRange(new Equipment[]{Boots.Polymer(),Leggins.Polymer(),Chestplate.Polymer(),Helmet.Polymer()});
                possibleEquipments.AddRange(new Equipment[]{Knife.Mk1(),Sword.Mk1(),Spear.Mk1()});
            }
            if(difficultyLevel <= 3){
                possibleEquipments.AddRange(new Equipment[]{Boots.Carbon(),Leggins.Carbon(),Chestplate.Carbon(),Helmet.Carbon()});
                possibleEquipments.AddRange(new Equipment[]{Knife.Mk2(),Sword.Mk2(),Spear.Mk2()});
            }
            if(difficultyLevel <= 4){
                possibleEquipments.AddRange(new Equipment[]{Boots.Platinum(),Leggins.Platinum(),Chestplate.Platinum(),Helmet.Platinum()});
                possibleEquipments.AddRange(new Equipment[]{Knife.Mk3(),Sword.Mk3(),Spear.Mk3()});
            }
            if(difficultyLevel <= 5){
                possibleEquipments.AddRange(new Equipment[]{Boots.Titanium(),Leggins.Titanium(),Chestplate.Titanium(),Helmet.Titanium()});
                possibleEquipments.AddRange(new Equipment[]{Knife.Mk4(),Sword.Mk4(),Spear.Mk4()});
            }

            Equipment equipment = possibleEquipments[random.Next(0, possibleEquipments.Count)];
            equipment.PosX = posX;
            equipment.PosY = posY;
            return equipment;

       

        }
    }
}