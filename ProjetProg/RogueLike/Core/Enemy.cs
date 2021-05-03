using RLNET;
using RogueLike.Systems;

using RogueLike.Behaviors;
namespace RogueLike.Core
{
    public abstract class Enemy : ActiveCharacter
    {

        // The number of turns while the enemy is alerted of the player presence
        public int? NbTurnsAlerted { get; set; }

        // The number of time period needed for the enemy to move
        // A time period is equal to 200ms
        public int MovingTimePeriod { get; set; }

        // The remaning number of period needed for the enemy to move
        // Desincremented in the update loop, and set to MovingTimePeriod when the enemy has moved
        public int RemainingTimePeriodToMove { get; set; }



        public Enemy()
        {
            Weapon = new Fist();
        }


        // base behavior of an enemy, that can be overwritten in the subclasses
        public virtual void PerformAction(Player player, CurrentMap map,CommandSystem commandSystem)
        {
            ChasePlayer behavior = new ChasePlayer();
            behavior.Act(this, player,map,commandSystem);
        }


        // To change the sprite that corresponds to the moving direction of the enemy
        // Also used to update the saved direction of the enemy
        public void ChangeDirection(int lastX, int lastY, int newX, int newY)
        {
            if (newX > lastX)
            {
                Symbol = RightSymbol;
                Direction = Direction.Right;
            }
            else if (newX < lastX)
            {
                Symbol = LeftSymbol;
                Direction = Direction.Left;
            }
            else if (newY > lastY)
            {
                Symbol = DownSymbol;
                Direction = Direction.Down;
            }
            else if (newY < lastY)
            {
                Symbol = UpSymbol;
                Direction = Direction.Up;
            }
        }

    }
}