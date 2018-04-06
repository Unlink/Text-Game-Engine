namespace GameFramework
{
    /// <summary>
    ///     Predmet interface
    /// </summary>
    public interface IItem : INamed
    {
        /// <summary>
        ///     Možnosť zobrať / položiť predmet
        /// </summary>
        /// <param name="player"></param>
        /// <param name="room"></param>
        /// <returns></returns>
        bool CanMove(Player player = null, Room room = null);
    }
}