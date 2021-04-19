using RLNET;

using RogueLike.Behaviors;
namespace RogueLike.Core
{
    public class Enemy : ActiveCharacter
    {
        // The number of turns while the enemy is alerted of the player presence
        public int? NbTurnsAlerted{get;set;}

        // Draw a health bar corresponding to the monster, under the player stats
        public void DrawStats(RLConsole statConsole, int cptMonstre){

            // The player stats ends at y=13
            // We draw the enemy stat under the playerstats and under the other enemy stats if there's any (counted thanks to cptMontre)
            int yPosition = 13 + (cptMonstre*2);
            statConsole.Print(1, yPosition,Symbol.ToString(),Color); // Print the enemy symbol

            // Print the health bar on 16 cells
            int healthBarWidth = 16;
            int remaningHealth = (int)(((double)Health/(double)MaxHealth)*healthBarWidth);
            
            // Create the health bar thanks to the background color
            statConsole.SetBackColor(3, yPosition,remaningHealth,1,Colors.HealthBar);
            statConsole.SetBackColor(3+remaningHealth, yPosition,healthBarWidth - remaningHealth,1,Colors.HealthBarDamage);

            // Print the enemy name on the health bar
            statConsole.Print(2,yPosition,$": {Name}",Colors.Text);
        } 

        // base behavior of an enemy, that can be overwritten in the subclasses
        public virtual void PerformAction(Game game){
            ChasePlayer behavior = new ChasePlayer();
            behavior.Act(this,game);
        }
        
    }
}