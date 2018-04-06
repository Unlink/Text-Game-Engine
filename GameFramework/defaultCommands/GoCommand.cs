using GameFramework.Parser;

namespace GameFramework.DefaultCommands
{
    internal class GoCommand : ICommand
    {
        public string Execute(Game game, Options options)
        {
            if (options.Get(0) == null)
            {
                return "You must specify direction";
            }
            try
            {
                if (game.Player.Move(options.Get(0))) return "You are now in new room, explore it";

                var suggestion = TextTools.GetSuggestion(game.Player.Room.GetNeighbors(), options.Get(0));
                return "There is not room at " + options.Get(0) + "." + (suggestion != "" ? " Did you mean "+suggestion+"?" : "");
                
            }
            catch (InvalidRoomAccessException e)
            {
                return e.Message;
            }
        }

        public string Name
        {
            get { return "go"; }
        }
    }
}