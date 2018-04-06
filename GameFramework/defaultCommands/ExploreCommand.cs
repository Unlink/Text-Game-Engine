using GameFramework.Parser;

namespace GameFramework.defaultCommands
{
    internal class ExploreCommand : ICommand
    {
        public string Execute(Game game, Options options)
        {
            return game.Player.Explore(options.Get(0));
            ;
        }

        public string Name
        {
            get { return "explore"; }
        }
    }
}