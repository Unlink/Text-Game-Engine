namespace GameFramework.Parser
{
    /// <summary>
    ///     Vykonateľný príkaz
    /// </summary>
    public class ExecutableCommand
    {
        /// <summary>
        ///     Konkrétny príkaz ktory sa bude vykonavat
        /// </summary>
        private readonly ICommand _command;

        /// <summary>
        ///     Konkrétne parametre píkazu
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public readonly Options _options;

        internal ExecutableCommand(ICommand command, string options)
        {
            _command = command;
            _options = new Options(options);
        }

        /// <summary>
        ///     Vykoná príkaz
        /// </summary>
        /// <param name="game">Hra, na ktorej sa príkaz vykonava</param>
        /// <returns>Výsledok príkazu - vypisuje sa na konzolu</returns>
        public string Execute(Game game)
        {
            return _command.Execute(game, _options);
        }
    }
}