namespace RogueLike.Interfaces
{
    /// <summary>
    /// This interface is implemented by every classes that generate drawable elements on the map
    /// </summary>
    public interface IDrawableGenerator
    {
        /// <summary>
        /// Create a drawable elememnt
        /// </summary>
        /// <param name="difficultyLevel"> The difficluty level of the game that is computed in mapCreation</param>
        /// <param name="posX"> The x position of the element</param>
        /// <param name="posY"> The y position of the element</param>
        /// <returns>Return the created drawable</returns>
        IDrawable Create(int difficultyLevel,int posX, int posY);
    }
}