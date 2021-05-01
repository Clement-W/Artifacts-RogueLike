using System.Collections.Generic;
using System;
namespace RogueLike.Core
{
    public class EquipmentSeller : Merchant
    {
        public EquipmentSeller(int posX, int posY, int merchantLevel) : base(merchantLevel)
        {
            Name = "Equipment Seller";
            PrintedColor = Colors.Seller;
            Symbol = '~';
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
                    possibleEquipment = new List<Equipment>(new Equipment[] { Boots.Polymer(), Leggins.Polymer(), Chestplate.Polymer(), Helmet.Polymer(), Sword.Mk1(), Spear.Mk1(), Knife.Mk1() });
                    break;
                case 1:
                    possibleEquipment = new List<Equipment>(new Equipment[] { Boots.Carbon(), Leggins.Carbon(), Chestplate.Carbon(), Helmet.Carbon(), Boots.Platinum(), Leggins.Platinum(), Chestplate.Platinum(), Helmet.Platinum(), Sword.Mk2(), Spear.Mk2(), Knife.Mk2(), Sword.Mk2(), Spear.Mk3(), Knife.Mk3() });
                    break;
                case 2:
                    possibleEquipment = new List<Equipment>(new Equipment[] { Boots.Titanium(), Leggins.Titanium(), Chestplate.Titanium(), Helmet.Titanium(), Sword.Mk4(), Spear.Mk4(), Knife.Mk4() });
                    break;

            }
            // Pick 3 piece of equipment in the list
            if (possibleEquipment != null)
            {
                Equipment equipment1 = possibleEquipment[random.Next(0, possibleEquipment.Count)];
                possibleEquipment.Remove(equipment1); // Remove it to avoid selling the same item

                Equipment equipment2 = possibleEquipment[random.Next(0, possibleEquipment.Count)];
                possibleEquipment.Remove(equipment1);

                Equipment equipment3 = possibleEquipment[random.Next(0, possibleEquipment.Count)];
                possibleEquipment.Remove(equipment1);

                Stall.Add(0,equipment1);
                Stall.Add(1,equipment2);
                Stall.Add(2,equipment3); 
            }

        }
    }
}