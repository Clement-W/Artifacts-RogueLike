using RogueSharp;
namespace RogueLike.Core.Enemies
{
    public class FinalBoss : Enemy
    {
        public void ResolveBossDeath(CurrentMap map)
        {
            // If the this is a final boss, drop it's weapon
            Cell dropCell = map.FindClosestWalkableCell(this);
            this.Weapon.PosX = dropCell.X;
            this.Weapon.PosY = dropCell.Y;
            map.AddLoot(this.Weapon);

            //Drop the artifact
            Cell artifactCell = map.FindClosestWalkableCell(this);
            map.AddLoot(new Artifact(map.Location.Planet, artifactCell.X, artifactCell.Y));

            //Reveal Teleportation portal to go back to the spaceships
            Cell portalCell = map.FindClosestWalkableCell(this);
            map.AddTeleportationPortal(new PortalToSpaceship(portalCell.X, portalCell.Y));

        }
    }
}