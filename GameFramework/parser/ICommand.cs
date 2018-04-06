namespace GameFramework.Parser
{
    /// <summary>
    ///     Interface príkazu
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        ///     Názov commandu
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Vykoná príkaz pod hrou a zo zadanými parametrami
        /// </summary>
        /// <param name="game">Hra, na ktorej sa príkaz vykonava</param>
        /// <param name="options">Parametre príkazu</param>
        /// <returns></returns>
        string Execute(Game game, Options options);
    }
}