namespace RogueLike.Core.Items
{
    public class HealthKit : Item
    {
        public HealthKit() : base(){
            Name = "Health kit";
            Cost = 50;
            Symbol = Icons.healthKitSymbol;
        }

        public override void Use(Player player){
            player.Health+=100;
            if(player.Health>player.MaxHealth){
                player.Health = player.MaxHealth;   
            }
        }
    }
}