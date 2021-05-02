using RLNET;

using RogueLike.Behaviors;
namespace RogueLike.Core
{
    public abstract class Enemy : ActiveCharacter
    {

        // The number of turns while the enemy is alerted of the player presence
        public int? NbTurnsAlerted{get;set;}

        // The number of time period needed for the enemy to move
        // A time period is equal to 200ms
        public int MovingTimePeriod{get;set;}

        // The remaning number of period needed for the enemy to move
        // Desincremented in the update loop, and set to MovingTimePeriod when the enemy has moved
        public int RemainingTimePeriodToMove{get;set;}

        public Enemy(){
            Weapon = new Fist();
        }


        // base behavior of an enemy, that can be overwritten in the subclasses
        public virtual void PerformAction(Game game){
            ChasePlayer behavior = new ChasePlayer();
            behavior.Act(this,game);
        }


        // To draw the sprite that corresponds to the moving direction of the enemy
        public void ChangeDirectionSymbol(int lastX, int lastY, int newX, int newY){
            if(newX > lastX){
                Symbol = RightSymbol;
            }else if(newX < lastX){
                Symbol = LeftSymbol;
            }else if(newY > lastY){
                Symbol = DownSymbol;
            }else if(newY < lastY){
                Symbol = UpSymbol;
            }
        }

        
    }
}