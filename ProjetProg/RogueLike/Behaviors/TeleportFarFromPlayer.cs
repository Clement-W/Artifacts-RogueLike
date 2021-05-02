using RogueLike.Interfaces;
using RogueLike.Core;
using RogueSharp;
namespace RogueLike.Behaviors
{
    public class TeleportFarFromPlayer : IBehavior
    {
        // The enemy teleport itself randomly and attack on a random side when teleporting.
        // It is used by the damari's final boss that has a very high attack. If the player is 
        // hit, it can die instantly.
        public bool Act(Enemy enemy, Game game)
        {
            CurrentMap map = game.Map;
            Player player = game.Player;

            ICell randomCell = map.FindRandomWalkableCell();
            game.CommandSystem.MoveEnemy(enemy,randomCell,map,player);

            return true;
        }
    }
}