namespace GameFramework
{
    /// <summary>
    ///     Delegát pohybu
    ///     Trieda presúva hráča na miesto určenia
    /// </summary>
    public interface IMovingDirection : INamed
    {
        /// <summary>
        ///     Cielové umiestnenie
        /// </summary>
        /// <returns></returns>
        Room GetDestination();
    }
}