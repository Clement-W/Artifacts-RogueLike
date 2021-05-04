using RogueSharp;
namespace RogueLike.Core.Enemies
{
    /// <summary>
    /// This class represent a specific enemy : a final boss. This enemy is different because
    /// when it dies, it drops it's weapon
    /// </summary>
    public class FinalBoss : Enemy
    {
        /// <summary>
        /// This method deals with the boss death : It drop it's weapon, the artifact
        /// and a teleportation portal spawn to allow the player to go back to the spaceship
        /// </summary>
        /// <param name="map"></param>
        public void ResolveBossDeath(CurrentMap map)
        {
            // If the this is a final boss, drop it's weapon
            Cell dropCell = map.FindClosestWalkableCell(this);
            this.Weapon.PosX = dropCell.X;
            this.Weapon.PosY = dropCell.Y;
            map.AddLoot(this.Weapon);

            // Drop the artifact
            Cell artifactCell = map.FindClosestWalkableCell(this);
            map.AddLoot(new Artifact(map.Location.Planet, artifactCell.X, artifactCell.Y));

            // Reveal Teleportation portal to go back to the spaceships
            Cell portalCell = map.FindClosestWalkableCell(this);
            map.AddTeleportationPortal(new PortalToSpaceship(portalCell.X, portalCell.Y));

        }
    }
}