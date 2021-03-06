using RogueLike.View;
using RogueLike.Systems;


namespace RogueLike.Core
{
    /// <summary>
    /// This class is the game instance. It contains everything about the game except the views (console).
    /// </summary>
    public class Game
    {


        /// <value>
        /// The player of the game
        /// </value>
        public Player Player { get; private set; }

        /// <value>
        /// The camera system that is used to create a scrolling map
        /// </value>
        public CameraSystem Camera { get; private set; }

        /// <value>
        /// The command system that is used to perform actions on the game
        /// </value>
        public CommandSystem Command { get; private set; }

        /// <value>
        /// This is the scheduling system that schedule the enemy's actions
        /// </value>
        public SchedulingSystem Scheduling { get; private set; }

        /// <value>
        /// The animation system animates the animated character periodically
        /// </value>
        public AnimationSystem Animation { get; private set; }

        /// <value>
        /// The message log is used to give information about the game state to the player
        /// </value>
        public static MessageLog Messages { get; set; }
        
      
        /// <value>
        /// This is the current map where the player is
        /// </value>
        public CurrentMap Map { get; set; }

        /// <value>
        /// This is the current level in the map (used in the planet's map)
        /// </value>
        public static int CurrentLevel { get; set; } 


        /// <summary>
        /// This method is used to start the game
        /// </summary>
        public void StartGame()
        {
            CurrentLevel = 1;
            GameScreen gameScreen = new GameScreen(this);

            Command = new CommandSystem();
            Camera = new CameraSystem();
            Messages = new MessageLog();
            Player = new Player();
            Scheduling = new SchedulingSystem();
            Animation = new AnimationSystem();
            InitializeMap();
        }

        /// <summary>
        /// Initialize the first map : the spaceship
        /// </summary>
        private void InitializeMap()
        {
            MapCreation mapCreation = new MapCreation(Dimensions.worldWidth, Dimensions.worldHeight, CurrentLevel, Player.ArtifactsCollected.Count, MapType.Spaceship);

            Map = mapCreation.CreateMap(Player);
        }






    }
}
