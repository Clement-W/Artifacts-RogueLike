using RogueLike.Core;
using RogueLike.Systems;

namespace RogueLike.Interfaces
{
    public interface IBehavior
    {
        bool Act(Enemy enemy, Game game);
    }
}