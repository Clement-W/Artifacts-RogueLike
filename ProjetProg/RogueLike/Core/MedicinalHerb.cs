namespace RogueLike.Core
{
    public class MedicinaHerb : Item
    {
        public MedicinaHerb() : base(){
            Name = "Medicinal Herb";
            Cost = 15;
        }

        public override void Use(Player player){
            player.Health+=30;
            if(player.Health>player.MaxHealth){
                player.Health = player.MaxHealth;   
            }
        }
    }
}