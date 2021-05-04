using System.Diagnostics;
using RogueLike.Core;
using RogueLike.Interfaces;
namespace RogueLike.Systems
{
    /// <summary>
    /// This class allows to animate periodically every animated elements
    /// </summary>
    public class AnimationSystem
    {

        /// <summary>
        /// This constant is the periodic time from which we trigger the animation
        /// </summary>
        private const int ANIMATION_TIME_PERIOD = 500;

        /// <summary>
        /// The stopwatch that allows to know when to change the sprite of the 
        /// animated element
        /// </summary>
        private Stopwatch animationStopWatch;

        /// <summary>
        /// This is the constructor of this class which initializes and starts the stopwatch
        /// </summary>
        public AnimationSystem()
        {
            animationStopWatch = new Stopwatch();
            animationStopWatch.Start();
        }

        /// <summary>
        /// This method animates the sprites and is called in the render method of the game screen
        /// </summary>
        /// <param name="map">The map instance </param>
        public void AnimateAnimatedElements(CurrentMap map)
        {
            // When the stopwatch exceeds 500ms, change the sprite of the animated element
            if (animationStopWatch.ElapsedMilliseconds > ANIMATION_TIME_PERIOD)
            {
                foreach (IAnimated animated in map.AnimatedSprites)
                {
                    animated.ChangeSymbolAlternative(); // Animate every animated elements
                }
                View.GameScreen.RenderRequired = true; // Render the game screen
                animationStopWatch.Restart(); // Restart the stopwatch
            }

        }
    }
}