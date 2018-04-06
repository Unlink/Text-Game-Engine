using GameFramework.Parser;

namespace GameFramework.defaultCommands
{
    internal class ExitCommand : ICommand
    {
        public string Execute(Game game, Options options)
        {
            game.exit();
            return null;
        }

        public string Name
        {
            get { return "exit"; }
        }
    }
}