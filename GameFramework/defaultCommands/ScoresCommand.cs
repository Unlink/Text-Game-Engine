using GameFramework.Parser;

namespace GameFramework.defaultCommands
{
    internal class ScoresCommand : ICommand
    {
        public string Execute(Game game, Options options)
        {
            return game.ShowScores();
        }

        public string Name
        {
            get { return "showscore"; }
        }
    }
}