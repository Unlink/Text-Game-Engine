using GameFramework.Parser;

namespace GameFramework.defaultCommands
{
    internal class LoadCommand : ICommand
    {
        public string Execute(Game game, Options options)
        {
            if (!string.IsNullOrEmpty(options.Opts))
            {
                return game.LoadGame(options.Opts);
            }
            return "Please speciffy save name for load";
        }

        public string Name
        {
            get { return "load"; }
        }
    }
}