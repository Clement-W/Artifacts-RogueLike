using System.Diagnostics;
using RogueLike.Core;
namespace RogueLike.Systems
{
    /// <summary>
    /// This class allows to move the enemies according to a specific schedule
    /// </summary>
    public class SchedulingSystem
    {

        /// <summary>
        /// This constant is the time to complete a period in miliseconds.
        /// Every enemies has a certain number of time period needed to move. 
        /// </summary>
        private const int TIME_PERIOD = 100;

        /// <summary>
        /// The stopwatch that allows to know when it's needed to check the enemies moving time period. 
        /// </summary>
        private static Stopwatch schedulingStopWatch;

        /// <summary>
        /// This is the constructor, it starts the stopwatch
        /// </summary>
        public SchedulingSystem(){
            schedulingStopWatch = new Stopwatch();
            schedulingStopWatch.Start();


        }

        /// <summary>
        /// Every time period, this method allows to move the non playable characters (only enemies at the moment)
        /// </summary>
        /// <param name="game"></param>
        public void CheckSchedule(Game game){

            if (schedulingStopWatch.ElapsedMilliseconds > TIME_PERIOD)
            {
                // Reset the stopwatch
                schedulingStopWatch.Reset();
                // Move the enemies
                game.Command.MoveEnemies(game); 
                // Render the game screen
                View.GameScreen.RenderRequired = true; 
                // Restart the stopwatch
                schedulingStopWatch.Start(); 
            } 
        }
    }
}