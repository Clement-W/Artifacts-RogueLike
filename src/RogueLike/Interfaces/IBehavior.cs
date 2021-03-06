using RogueLike.Core;
using RogueLike.Systems;
using RogueLike.Core.Enemies;

namespace RogueLike.Interfaces
{
    /// <summary>
    /// This interface is implemented by the classes that act as a behavior
    /// for some enemies
    /// </summary>
    public interface IBehavior
    {
       

        /// <summary>
        /// Allow an enemy to act when it has to make a move
        /// </summary>
        /// <param name="enemy">The enemy that needs to acts</param>
        /// <param name="player">The player to attack if encountered</param>
        /// <param name="map">The map where the enemy and the player are situated</param>
        /// <param name="command">The command system is used to take an action on the game</param>
        void Act(Enemy enemy, Player player, CurrentMap map, CommandSystem command);
    }
}