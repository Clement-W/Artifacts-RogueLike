using System.Collections.Generic;
using System;
using RogueLike.Core.Equipments;
namespace RogueLike.Core.Merchants
{
    public class EquipmentSeller : Merchant
    {
        public EquipmentSeller(int posX, int posY, int merchantLevel) : base(merchantLevel)
        {
            Name = "Equipment Seller";
            PrintedColor = Colors.Seller;
            AlternateSymbol1 = Icons.equipementSellerSymbol1;
            AlternateSymbol2 = Icons.equipementSellerSymbol2;
            Symbol = AlternateSymbol1;
            PosX = posX;
            PosY = posY;
        }

        // Called in the constructor of Merchant
        public override void GenerateStall()
        {
            Random random = new Random();
            List<Equipment> possibleEquipment = null;
            switch (MerchantLevel)
            {
                case 0:
                    possibleEquipment = new List<Equipment>(new Equipment[] { Boots.CreatePolymerBoots(), Leggins.CreatePolymerLeggins(), Chestplate.CreatePolymerChestplate(), Helmet.CreatePolymerHelmet(), Sword.CreateSwordMk1(), Spear.CreateSpearMk1(), Knife.CreateKnifeMk1() });
                    break;
                case 1:
                    possibleEquipment = new List<Equipment>(new Equipment[] { Boots.CreateCarbonBoots(), Leggins.CreateCarbonLeggins(), Chestplate.CreateCarbonChestplate(), Helmet.CreateCarbonHelmet(), Boots.CreatePlatinumBoots(), Leggins.CreatePlatinumLeggins(), Chestplate.CreatePlatinumChestplate(), Helmet.CreatePlatinumHelmet(), Sword.CreateSwordMk2(), Spear.CreateSpearMk2(), Knife.CreateKnifeMk2(), Sword.CreateSwordMk3(), Spear.CreateSpearMk3(), Knife.CreateKnifeMk3() });
                    break;
                case 2:
                    possibleEquipment = new List<Equipment>(new Equipment[] { Boots.CreateTitaniumBoots(), Leggins.CreateTitaniumLeggins(), Chestplate.CreateTitaniumChestplate(), Helmet.CreateTitaniumHelmet(), Sword.CreateSwordMk4(), Spear.CreateSpearMk4(), Knife.CreateKnifeMk4() });
                    break;

            }
            // Pick 3 piece of equipment in the list
            if (possibleEquipment != null)
            {
                Equipment equipment1 = possibleEquipment[random.Next(0, possibleEquipment.Count)];
                possibleEquipment.Remove(equipment1); // Remove it to avoid selling the same item

                Equipment equipment2 = possibleEquipment[random.Next(0, possibleEquipment.Count)];
                possibleEquipment.Remove(equipment2);

                Equipment equipment3 = possibleEquipment[random.Next(0, possibleEquipment.Count)];
                possibleEquipment.Remove(equipment3);

                Stall.Add(0, equipment1);
                Stall.Add(1, equipment2);
                Stall.Add(2, equipment3);
            }

        }
    }
}