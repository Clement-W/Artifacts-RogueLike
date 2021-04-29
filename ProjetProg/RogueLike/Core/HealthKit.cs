namespace RogueLike.Core
{
    public class HealthKit : Item
    {
        public HealthKit() : base(){
            Name = "Health kit";
        }

        public override void Use(Player player){
            player.Health+=100;
            if(player.Health>player.MaxHealth){
                player.Health = player.MaxHealth;   
            }
        }
    }
}