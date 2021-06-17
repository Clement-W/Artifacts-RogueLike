using System.Collections.Generic;
using RLNET;
using RogueLike.Core;

namespace RogueLike.Systems
{

    /// <summary>
    /// This class contains the message system of the game to inform the player about what's
    /// happening.
    /// </summary>
    public class MessageLog
    {

        /// <summary>
        /// The maximum number of lines that are printed on the screen
        /// </summary>
        private static readonly int maxLines = 5;

        /// <summary>
        /// The queue of messages that are printed on the screen
        /// </summary>
        private readonly Queue<string> messages;

        /// <summary>
        /// This is the constructor of this class, that creates the queue of messages
        /// </summary>
        public MessageLog()
        {
            messages = new Queue<string>();
        }

        /// <summary>
        /// This method adds a message into the queue, and dequeue the old messages according to the
        /// maxLines attribute;
        /// </summary>
        /// <param name="message">The message to add in the queue</param>
        public void AddMessage(string message)
        {
            messages.Enqueue(message);
            if (messages.Count > maxLines)
            {
                messages.Dequeue();
            }
            View.GameScreen.RenderRequired = true;
        }

        /// <summary>
        /// Draw the messages into the message console
        /// </summary>
        /// <param name="console">The message console instance that is contained in the game screen</param>
        public void Draw(RLConsole console)
        {
            console.Clear();
            int cptMessage = 0;
            foreach (string message in messages)
            {
                console.Print(1, cptMessage, message, Colors.Text);
                cptMessage++;
            }
        }
    }
}