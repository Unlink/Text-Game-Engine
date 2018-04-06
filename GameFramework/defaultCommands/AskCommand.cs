using GameFramework.Parser;

namespace GameFramework.defaultCommands
{
    internal class AskCommand : ICommand
    {
        public string Execute(Game game, Options options)
        {
            string itm = options.Get(0);
            if (itm != null)
            {
                if (!game.Player.Ask(itm))
                {
                    var suggestion = TextTools.GetSuggestion(game.Player.Room.GetCharacters(), options.Get(0));
                    return "There isn't person named " + options.Get(0) + " in the room." + (suggestion != "" ? " Did you mean " + suggestion + "?" : "");
                }
                return null;
            }
            return "Ask who?";
        }

        public string Name
        {
            get { return "ask"; }
        }
    }
}