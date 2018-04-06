using GameFramework.Parser;

namespace GameFramework.defaultCommands
{
    internal class SaveCommand : ICommand
    {
        public string Execute(Game game, Options options)
        {
            if (!string.IsNullOrEmpty(options.Opts))
            {
                return game.Save(options.Opts);
            }
            return "Please speciffy save name";
        }

        public string Name
        {
            get { return "save"; }
        }
    }
}