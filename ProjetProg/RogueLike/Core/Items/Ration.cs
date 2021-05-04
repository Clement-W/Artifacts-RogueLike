namespace RogueLike.Core.Items
{
    public class Ration : Item
    {
        public Ration() : base(){
            Name = "Ration";
            Cost = 40;
            Symbol = Icons.rationSymbol;
        }

        public override void Use(Player player){
            player.Health+=50;
            if(player.Health>player.MaxHealth){
                player.Health = player.MaxHealth;   
            }
        }
    }
}