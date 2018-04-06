using GameFramework.Parser;

namespace GameFramework.DefaultCommands
{
    internal class DropCommand : ICommand
    {
        public string Execute(Game game, Options options)
        {
            string itm = options.Get(0);
            if (itm != null)
            {
                if (!game.Player.Drop(itm))
                {
                    return "Item not found";
                }
                return "Item dropped";
            }
            return "You should speciffy item";
        }

        public string Name
        {
            get { return "drop"; }
        }
    }
}