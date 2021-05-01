using RLNET;
using RogueSharp;
using RogueLike.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RogueLike.Core
{

    public class Player : ActiveCharacter
    {

        public Helmet Head { get; set; }
        public Chestplate Chest { get; set; }
        public Leggins Legs { get; set; }
        public Boots Feet { get; set; }
        public AttackEquipment Weapon { get; set; }

        public List<Item> Items { get; set; }




        public Player()
        {
            Items = new List<Item>();
            //create a player with the initial stats

            Attack = 10;
            Defense = 5;
            Awareness = 15;
            Gold = 0;
            Health = 100;
            MaxHealth = 100;
            Name = "Adventurer";
            PrintedColor = Colors.Player;
            BaseColor = PrintedColor;
            ColorAfterHit = Colors.PlayerHit;

            UpSymbol = '�';
            DownSymbol = '�';
            LeftSymbol = '�';
            RightSymbol = '�';
            Symbol = DownSymbol;

            Head = Helmet.None();
            Chest = Chestplate.None();
            Legs = Leggins.None();
            Feet = Boots.None();

            //Weapon = new Fist();
            Weapon = new Fist();
        }


        public void DrawStats(RLConsole statConsole)
        {

            int healthBarWidth = Dimensions.statConsoleWidth - 2;
            int remainingHealth = (int)(((double)Health / (double)MaxHealth) * healthBarWidth);

            // Create the health bar thanks to the background color
            statConsole.SetBackColor(1, 2, remainingHealth, 1, Colors.HealthBar);
            statConsole.SetBackColor(1 + remainingHealth, 2, healthBarWidth - remainingHealth, 1, Colors.HealthBarDamage);

            statConsole.Print(((int)(healthBarWidth / 2)), 2, "PV", Colors.Text);

            statConsole.Print(1, 3, $"Att: {Attack}", Colors.GrayText);
            statConsole.Print(1 + (int)(Dimensions.statConsoleWidth / 3), 3, $"Def: {Defense}", Colors.GrayText);
            statConsole.Print(1 + (int)(2 * Dimensions.statConsoleWidth / 3), 3, $"Gold: {Gold}", Colors.Gold);
        }


        public void DrawEquipmentInventory(RLConsole equipmentConsole)
        {
            equipmentConsole.Print(1, 1, $"{Icons.headStatusSymbol}: {Head.Symbol}", Colors.Text);
            equipmentConsole.Print(1, 2, $"{Icons.chestStatusSymbol}: {Chest.Symbol}", Colors.Text);
            equipmentConsole.Print(1, 3, $"{Icons.legsStatusSymbol}: {Legs.Symbol}", Colors.Text);
            equipmentConsole.Print(1, 4, $"{Icons.footStatusSymbol}: {Feet.Symbol}", Colors.Text);
            equipmentConsole.Print(1, 5, $"{Icons.weaponStatusSymbol}: {Weapon.Symbol}", Colors.Text);
        }

        public void DrawItemsInventory(RLConsole itemsConsole)
        {
            int yPosition = 1;
            for (int i = 0; i < Items.Count; i++)
            {
                itemsConsole.Print(1, yPosition, $"{i + 1}: {Items[i].Symbol}", Colors.Text);
                yPosition += 1;
            }
        }

        public void Move(int x, int y)
        {
            PosX = x;
            PosY = y;
        }



        //Collect an item or an equipment
        public bool Collect(ILoot loot, CurrentMap map)
        {
            bool isLootCollected = false;
            if (loot is Helmet)
            {
                if (Head.Name != "None")
                {
                    DropItem(map, Head); // Drop the current equipment 
                }
                Head = loot as Helmet; // Equip the equipment

                isLootCollected = true;
            }

            else if (loot is Chestplate)
            {
                if (Chest.Name != "None")
                {
                    DropItem(map, Chest); // Drop the current equipment 
                }
                Chest = loot as Chestplate; // Equip the equipment
                isLootCollected = true;
            }

            else if (loot is Leggins)
            {
                if (Legs.Name != "None")
                {
                    DropItem(map, Legs); // Drop the current equipment 
                }
                Legs = loot as Leggins; // Equip the equipment
                isLootCollected = true;
            }

            else if (loot is Boots)
            {
                if (Feet.Name != "None")
                {
                    DropItem(map, Feet);// Drop the current equipment 
                }
                Feet = loot as Boots; // Equip the equipment
                isLootCollected = true;
            }

            else if (loot is AttackEquipment)
            {
                if (Weapon is not Fist)
                {
                    DropItem(map, Weapon); // Drop the current weapon
                }
                Weapon = loot as AttackEquipment;
                isLootCollected = true;
            }

            else if (loot is Item)
            {
                if (Items.Count < 5)
                {
                    Items.Add(loot as Item);
                    isLootCollected = true;
                }
                else
                {
                    Game.MessageLog.AddMessage("You're carrying too many items to take that.");
                }
            }

            else if (loot is Gold)
            {
                Gold goldLoot = loot as Gold;
                Gold += goldLoot.Amount;
                isLootCollected = true;
            }

            return isLootCollected;
        }

        // Drop an item on the ground
        private void DropItem(CurrentMap map, ILoot loot)
        {
            Cell cell = FindClosestWalkableCell(map);
            if (cell != null)
            {
                loot.PosX = cell.X;
                loot.PosY = cell.Y;
                if (loot is Equipment)
                {
                    Equipment lootEquipment = loot as Equipment;
                    Game.MessageLog.AddMessage("You've dropped " + lootEquipment.Name);
                }
                map.AddLoot(loot);
            }
        }
        // Find a walkable cell around the player
        private Cell FindClosestWalkableCell(CurrentMap map)
        {
            int distanceMax = 5; // max distance from which we search for a cell
            for (int i = 1; i < distanceMax; i++)
            {
                foreach (Cell cell in map.GetBorderCellsInSquare(PosX, PosY, i))
                {
                    if (cell.IsWalkable)
                    {
                        return cell;
                    }
                }
            }
            return null;
        }

        public void UseItem(int index)
        {
            if (Items.Count > index) // To avoid accessing the list at a outbound index
            {
                Item item = Items[index];
                item.Use(this);
                Items.RemoveAt(index);
            }
        }



    }
}