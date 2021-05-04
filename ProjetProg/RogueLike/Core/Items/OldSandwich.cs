namespace RogueLike.Core.Items
{
    public class OldSandwich : Item
    {
        public OldSandwich() : base(){
            Name = "Old Sandwich";
            Cost = 10;
            Symbol = Icons.sandwichSymbol;
        }

        public override void Use(Player player){
            player.Health+=20;
            if(player.Health>player.MaxHealth){
                player.Health = player.MaxHealth;   
            }
        }
    }
}