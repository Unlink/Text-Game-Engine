using System;

namespace GameFramework
{
    /// <summary>
    ///     Vínimka ukončujúca hru
    /// </summary>
    public class EndGameException : Exception
    {
        /// <summary>
        ///     Konštruktor triedy
        /// </summary>
        /// <param name="message"></param>
        public EndGameException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Konštruktor triedy
        /// </summary>
        /// <param name="message"></param>
        public EndGameException()
        {
        }
    }
}