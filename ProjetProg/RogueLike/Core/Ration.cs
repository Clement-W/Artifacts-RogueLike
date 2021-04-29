namespace RogueLike.Core
{
    public class Ration : Item
    {
        public Ration() : base(){
            Name = "Ration";
        }

        public override void Use(Player player){
            player.Health+=50;
            if(player.Health>player.MaxHealth){
                player.Health = player.MaxHealth;   
            }
        }
    }
}