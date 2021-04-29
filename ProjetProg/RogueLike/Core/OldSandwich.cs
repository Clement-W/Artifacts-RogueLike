namespace RogueLike.Core
{
    public class OldSandwich : Item
    {
        public OldSandwich() : base(){
            Name = "Old Sandwich";
            Cost = 10;
        }

        public override void Use(Player player){
            player.Health+=20;
            if(player.Health>player.MaxHealth){
                player.Health = player.MaxHealth;   
            }
        }
    }
}