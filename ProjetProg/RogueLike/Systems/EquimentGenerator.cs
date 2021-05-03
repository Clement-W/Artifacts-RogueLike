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
            if (difficultyLevel >=1)
            {
                possibleEquipments.AddRange(new Equipment[]{Boots.CreatePolymerBoots(),Leggins.CreatePolymerLeggins(),Chestplate.CreatePolymerChestplate(),Helmet.CreatePolymerHelmet()});
                possibleEquipments.AddRange(new Equipment[]{Knife.CreateKifeMk1(),Sword.CreateSwordMk1(),Spear.CreateSpearMk1()});
            }
            if(difficultyLevel >= 3){
                possibleEquipments.AddRange(new Equipment[]{Boots.CreateCarbonBoots(),Leggins.CreateCarbonLeggins(),Chestplate.CreateCarbonChestplate(),Helmet.CreateCarbonHelmet()});
                possibleEquipments.AddRange(new Equipment[]{Knife.CreateKnifeMk2(),Sword.CreateSwordMk2(),Spear.CreateSpearMk2()});
            }
            if(difficultyLevel >= 4){
                possibleEquipments.AddRange(new Equipment[]{Boots.CreatePlatinumBoots(),Leggins.CreatePlatinumLeggins(),Chestplate.CreatePlatinumChestplate(),Helmet.CreatePlatinumHelmet()});
                possibleEquipments.AddRange(new Equipment[]{Knife.CreateKnifeMk3(),Sword.CreateSwordMk3(),Spear.CreateSpearMk3()});
            }
            if(difficultyLevel >= 5){
                possibleEquipments.AddRange(new Equipment[]{Boots.CreateTitaniumBoots(),Leggins.CreateTitaniumLeggins(),Chestplate.CreateTitaniumChestplate(),Helmet.CreateTitaniumHelmet()});
                possibleEquipments.AddRange(new Equipment[]{Knife.CreateKnifeMk4(),Sword.CreateSwordMk4(),Spear.CreateSpearMk4()});
            }

            Equipment equipment = possibleEquipments[random.Next(0, possibleEquipments.Count)];
            equipment.PosX = posX;
            equipment.PosY = posY;
            return equipment;

       

        }
    }
}