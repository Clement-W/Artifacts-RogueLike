using RogueLike.Interfaces;
using RogueLike.Core;
using RogueSharp;
using RogueLike.Systems;
namespace RogueLike.Behaviors
{
    public class TeleportFarFromPlayer : IBehavior
    {
        // The enemy teleport itself randomly and attack on a random side when teleporting.
        // It is used by the damari's final boss that has a very high attack. If the player is 
        // hit, it can die instantly.
        public void Act(Enemy enemy, Player player, CurrentMap map, CommandSystem commandSystem)
        {


            ICell randomCell = map.FindRandomWalkableCell();
            commandSystem.MoveEnemy(enemy,randomCell,map,player);

        }
    }
}