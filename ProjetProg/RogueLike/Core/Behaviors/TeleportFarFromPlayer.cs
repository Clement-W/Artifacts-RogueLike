using RogueLike.Interfaces;
using RogueLike.Core.Enemies;
using RogueSharp;
using RogueLike.Systems;
namespace RogueLike.Core.Behaviors
{
    /// <summary>
    /// This class describes a specific behavior where the enemy will teleport itself randomly across the map
    /// </summary>
    public class TeleportFarFromPlayer : IBehavior
    {

        /// <summary>
        /// The enemy teleports itself randomly and attacks on a random side when teleporting.
        /// It is used by the damari's final boss that has a very high attack. If the player is 
        /// hit, he can be killed instantly. (very bad accuracy but very high power)
        /// </summary>
        /// <param name="enemy">The enemy that needs to acts</param>
        /// <param name="player">The player to attack if encountered</param>
        /// <param name="map">The map where the enemy and the player are situated</param>
        /// <param name="command">The command system is used to take an action on the game</param>
        public void Act(Enemy enemy, Player player, CurrentMap map, CommandSystem commandSystem)
        {
            // Find a random walkable cell
            ICell randomCell = map.FindRandomWalkableCell();
            // Move the enemy on this cell (also attack the player if he's on the enemy's direction)
            commandSystem.MoveEnemy(enemy, randomCell, map, player);
        }
    }
}