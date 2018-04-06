namespace GameFramework
{
    /// <summary>
    ///     Postava
    /// </summary>
    public interface ICharacter : INamed
    {
        /// <summary>
        ///     Miestnosť osoby
        /// </summary>
        Room Room { get; set; }

        /// <summary>
        ///     Osloví osobu
        /// </summary>
        /// <param name="player"></param>
        void Ask(Player player);
    }
}