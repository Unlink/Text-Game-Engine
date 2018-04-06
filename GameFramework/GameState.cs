using System;

namespace GameFramework
{
    /// <summary>
    ///     Stav hry
    /// </summary>
    [Flags]
    public enum GameState
    {
        /// <summary>
        ///     Nová hra pri vytvorení objektu
        /// </summary>
        New = 1,

        /// <summary>
        ///     Bežiaca
        /// </summary>
        Running = 2,

        /// <summary>
        ///     Skončená
        /// </summary>
        End = 4
    }
}