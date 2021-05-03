using System.Diagnostics;
using RogueLike.Core;
namespace RogueLike.Systems
{
    public class SchedulingSystem
    {
        private static Stopwatch schedulingStopWatch;

        public SchedulingSystem(){
            schedulingStopWatch = new Stopwatch();
            schedulingStopWatch.Start();


        }


        public void CheckSchedule(Game game){
            // Every second, trigger the scheduling system to move the non playable characters
            if (schedulingStopWatch.ElapsedMilliseconds > 100)
            {
                // Reset the stopwatch
                schedulingStopWatch.Reset();
                game.CommandSystem.MoveEnemies(game); // Move the enemies
                View.GameScreen.RenderRequired = true; // Render the game screen
                schedulingStopWatch.Start(); // Restart the stopwatch
            } 
        }
    }
}