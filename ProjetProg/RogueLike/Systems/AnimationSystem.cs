using System.Diagnostics;
using RogueLike.Core;
using RogueLike.Interfaces;
namespace RogueLike.Systems
{
    public class AnimationSystem
    {
        private static Stopwatch animationStopWatch;

        public AnimationSystem()
        {
            animationStopWatch = new Stopwatch();
            animationStopWatch.Start();
        }

        public void AnimateAnimatedElements(Game game)
        {

            if (animationStopWatch.ElapsedMilliseconds > 500)
            {
                animationStopWatch.Reset();
                foreach (IAnimated animated in game.Map.AnimatedSprites)
                {
                    animated.ChangeSymbolAlternative();
                }
                View.GameScreen.RenderRequired = true;
                animationStopWatch.Start();
            }

        }
    }
}