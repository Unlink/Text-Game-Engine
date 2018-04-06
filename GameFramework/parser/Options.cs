namespace GameFramework.Parser
{
    /// <summary>
    ///     Struktúra obsahujúca parametre príkazu
    /// </summary>
    public struct Options
    {
        private readonly string _opts;

        public Options(string options)
        {
            _opts = options;
        }

        /// <summary>
        ///     Reťazec repretentujuci nastavenia
        /// </summary>
        public string Opts
        {
            get { return _opts; }
        }

        /// <summary>
        ///     Vráti konkrétny option
        /// </summary>
        /// <param name="option">Index požadovaného optionu</param>
        /// <returns>Reťazec alebo null</returns>
        public string Get(int option)
        {
            string[] splitted = Opts.Split(' ');
            if (option >= 0 && option < splitted.Length)
            {
                return splitted[option];
            }
            return null;
        }
    }
}