using GameFramework.Parser;

namespace GameFramework.DefaultCommands
{
    /// <summary>
    ///     Use command
    /// </summary>
    public class UseCommand : ICommand
    {
        /// <summary>
        ///     Metóda vykoná príkaz
        /// </summary>
        /// <param name="game"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public string Execute(Game game, Options options)
        {
            string itm = options.Get(0);
            if (itm != null)
            {
                if (!game.Player.Use(itm))
                {
                    return "Item not found";
                }
                return null;
            }
            return "You should speciffy item";
        }

        /// <summary>
        ///     Meno
        /// </summary>
        public string Name
        {
            get { return "use"; }
        }
    }
}