namespace GameFramework
{
    /// <summary>
    ///     Použitelný príkaz
    /// </summary>
    public interface IUsable : IItem
    {
        /// <summary>
        ///     Použi predmet
        /// </summary>
        /// <param name="player"></param>
        /// <param name="room"></param>
        void Use(Player player, Room room = null);
    }
}