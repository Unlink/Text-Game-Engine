namespace GameFramework
{
    /// <summary>
    ///     Objekt ktorý je možné notifikovať o jeho presuvani
    /// </summary>
    public interface INoticableItem : IItem
    {
        /// <summary>
        ///     Location changed handler method
        /// </summary>
        /// <param name="room"></param>
        /// <param name="player"></param>
        void LocationChange(Room room, Player player);
    }
}