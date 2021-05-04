using RLNET;
using RogueLike.Systems;
using RogueLike.Core;
using RogueLike.Core.Behaviors;
using RogueLike.Core.Equipments;

namespace RogueLike.Core.Enemies
{
    public abstract class Enemy : ActiveCharacter
    {

        // The number of turns during which the enemy is alerted of the player presence
        public int NbTurnsAlerted { get; set; }

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


        /// <summary>
        /// Perform an action according to a specific behavior
        /// </summary>
        /// <param name="player">The player to attack if encountered</param>
        /// <param name="map">The map where the enemy and the player are situated</param>
        /// <param name="command">The command system is used to take an action on the game</param>
        public virtual void PerformAction(Player player, CurrentMap map, CommandSystem commandSystem)
        {
            ChasePlayer behavior = new ChasePlayer();
            behavior.Act(this, player, map, commandSystem);
        }


        /// <summary>
        /// This method is used to change the sprite that corresponds to the moving direction of the enemy
        /// Also used to update the saved direction of the enemy
        /// </summary>
        /// <param name="lastX"> The last x position of the enemy</param>
        /// <param name="lastY"> The last y position of the enemy</param>
        /// <param name="newX"> The new x position of the enemy</param>
        /// <param name="newY"> The new x position of the enemy</param>
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