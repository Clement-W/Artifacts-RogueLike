namespace RogueLike.Core
{
    public class Bandage : Item
    {
        public Bandage() : base(){
            Name = "Bandage";
        }

        public override void Use(Player player){
            player.Health+=40;
            if(player.Health>player.MaxHealth){
                player.Health = player.MaxHealth;   
            }
        }
    }
}