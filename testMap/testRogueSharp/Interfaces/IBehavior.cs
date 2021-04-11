using testRogueSharp.Systems;
using testRogueSharp.Core;
namespace testRogueSharp.Interfaces{
    public interface IBehavior{
        bool Act(Monster monster, CommandSystem commandSystem);
    }
}