using System.Collections.Generic;

using RLNET;
using RogueSharp;

using RogueLike.Interfaces;
using RogueLike.Core.Equipments;
using RogueLike.Core.Items;

namespace RogueLike.Core
{

    /// <summary>
    /// This class represents the player. The player is an active character
    /// </summary>
    public class Player : ActiveCharacter
    {

        /// <value>
        /// This is the head of the player, it can be protected by a helmet
        /// </value>
        public Helmet Head { get; set; }

        /// <value>
        /// This is the head of the player, it can be protected by a Chestplate
        /// </value>
        public Chestplate Chest { get; set; }

        /// <value>
        /// This is the head of the player, it can be protected by a Leggins
        /// </value>
        public Leggins Legs { get; set; }

        /// <value>
        /// This is the head of the player, it can be protected by some boots
        /// </value>
        public Boots Feet { get; set; }
        

 
        /// <value>
        /// This is the list of items the player is carrying
        /// </value>
        public List<Item> Items { get; set; }
        
  
        /// <value>
        /// This is the list of artifacts collected by the player
        /// </value>
        public List<Artifact> ArtifactsCollected{get;set;}


        /// <summary>
        /// The max number of items the player can carry on
        /// </summary>
        private const int NB_ITEM_MAX=5;



        /// <summary>
        /// This is the constructor of the player class
        /// It initialize they stats and set the armor to None
        /// The base weapon is the fist
        /// </summary>
        public Player()
        {
            Items = new List<Item>();
            ArtifactsCollected = new List<Artifact>();

            Attack = 10;
            Defense = 5;
            Awareness = 15;
            Gold = 100;
            Health = 100;
            MaxHealth = 100;
            Name = "Adventurer";
            PrintedColor = Colors.Player;
            BaseColor = PrintedColor;
            ColorAfterHit = Colors.PlayerHit;

            UpSymbol = Symbols.playerUpSymbol;
            DownSymbol = Symbols.playerDownSymbol;
            LeftSymbol = Symbols.playerLeftSymbol;
            RightSymbol = Symbols.playerRightSymbol;
            Symbol = DownSymbol;
            Direction = Direction.Up;

            Head = Helmet.None();
            Chest = Chestplate.None();
            Legs = Leggins.None();
            Feet = Boots.None();

            Weapon = new Fist();
        }


        /// <summary>
        /// This method draws the player stats into the stat console
        /// A life bar and the attack, defense and gold amount of the player are displayed
        /// </summary>
        /// <param name="statConsole">The stat console</param>
        public void DrawStats(RLConsole statConsole)
        {
            
            int healthBarWidth = Dimensions.statConsoleWidth;
            int remainingHealth = (int)(((double)Health / (double)MaxHealth) * healthBarWidth);

            // Create the health bar using the background color
            statConsole.SetBackColor(1, 1, remainingHealth, 1, Colors.HealthBar);
            statConsole.SetBackColor(1 + remainingHealth, 1, healthBarWidth - remainingHealth, 1, Colors.HealthBarDamage);

            statConsole.Print(((int)(healthBarWidth / 2)), 1, "PV", Colors.Text);

            statConsole.Print(1, 2, $"{Symbols.attackSymbol}: {Attack}", Colors.GrayText);
            statConsole.Print(1 + (int)(Dimensions.statConsoleWidth / 3), 2, $"{Symbols.defenseSymbol}: {Defense}", Colors.GrayText);
            statConsole.Print(1 + (int)(2 * Dimensions.statConsoleWidth / 3), 2, $"Gold: {Gold}", Colors.Gold);
        }

        /// <summary>
        /// This method draw the equipment (armor + weapon) of the player into the 
        /// equipment console
        /// </summary>
        /// <param name="equipmentConsole"></param>
        public void DrawEquipmentInventory(RLConsole equipmentConsole)
        {
            equipmentConsole.Print(3, 2, $"{Symbols.headSlotSymbol}: ", Colors.Text);
            equipmentConsole.Print(6, 2, $"{Head.Symbol}", Head.PrintedColor);
            equipmentConsole.Print(3, 3, $"{Symbols.chestSlotSymbol}: ", Colors.Text);
            equipmentConsole.Print(6, 3, $"{Chest.Symbol}", Chest.PrintedColor);
            equipmentConsole.Print(3, 4, $"{Symbols.legsSlotSymbol}: ", Colors.Text);
            equipmentConsole.Print(6, 4, $"{Legs.Symbol}", Legs.PrintedColor);
            equipmentConsole.Print(3, 5, $"{Symbols.footSlotSymbol}: ", Colors.Text);
            equipmentConsole.Print(6, 5, $"{Feet.Symbol}", Feet.PrintedColor);
            equipmentConsole.Print(3, 6, $"{Symbols.weaponSlotSymbol}: ", Colors.Text);
            equipmentConsole.Print(6, 6, $"{Weapon.Symbol}", Weapon.PrintedColor);
        }

        /// <summary>
        /// This method draws the items of the player into the items console
        /// </summary>
        /// <param name="itemsConsole"></param>
        public void DrawItemsInventory(RLConsole itemsConsole)
        {
            int yPosition = 2;
            for (int i = 0; i < Items.Count; i++)
            {
                itemsConsole.Print(3, yPosition, $"{i + 1}: {Items[i].Symbol}", Colors.Text);
                yPosition++;
            }
        }

        /// <summary>
        /// This method sets the position of the player to the specified coordinates
        /// </summary>
        /// <param name="x">The x new position</param>
        /// <param name="y">The y new position</param>
        public void SetPosition(int x, int y)
        {
            PosX = x;
            PosY = y;  
        }



        /// <summary>
        /// This method allows the player to collect a loot and add
        /// it into the inventory
        /// </summary>
        /// <param name="loot">The loot</param>
        /// <param name="map">The map</param>
        /// <returns>Return true if the player collect the loot</returns>
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
                // The player can't have more than 5 items
                if (Items.Count < NB_ITEM_MAX)
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

            else if(loot is Artifact){
                Artifact artifact = loot as Artifact;
                ArtifactsCollected.Add(artifact);
                isLootCollected = true;
            }

            return isLootCollected;
        }

        /// <summary>
        /// This method allows the player to drop an item on the ground.
        /// It is used when the player collects an equipment but already has this type
        /// of equipment
        /// </summary>
        /// <param name="map">The map</param>
        /// <param name="loot">The loot</param>
        private void DropItem(CurrentMap map, ILoot loot)
        {
            // Drop the item on the closest walkable cell
            Cell cell = map.FindClosestWalkableCell(this);
            if (cell != null)
            {
                loot.PosX = cell.X;
                loot.PosY = cell.Y;
                if (loot is Equipment)
                {
                    Equipment lootEquipment = loot as Equipment;
                    Game.MessageLog.AddMessage("You've dropped " + lootEquipment.Name);
                }
                // Add the loot to the map
                map.AddLoot(loot);
            }
        }
        

        /// <summary>
        /// This allows the player to use an item at the specified index
        /// </summary>
        /// <param name="index"></param>
        public void UseItem(int index)
        {
            if (Items.Count > index) // To avoid accessing the list at an outbound index
            {
                Item item = Items[index];
                item.Use(this);
                Items.RemoveAt(index);
            }
        }
    }
}