using GameFramework.Parser;

namespace GameFramework.defaultCommands
{
    /// <summary>
    ///     Nová hra
    /// </summary>
    public class NewGameCommand : ICommand
    {
        /// <summary>
        ///     Metóda vykoná príkaz
        /// </summary>
        /// <param name="game"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public string Execute(Game game, Options options)
        {
            return game.NewGame();
        }

        /// <summary>
        ///     Meno
        /// </summary>
        public string Name
        {
            get { return "newgame"; }
        }
    }
}