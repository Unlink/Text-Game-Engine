using GameFramework.Parser;

namespace GameFramework.DefaultCommands
{
    internal class PickUpCommand : ICommand
    {
        public string Execute(Game game, Options options)
        {
            string itm = options.Get(0);
            if (itm != null)
            {
                if (!game.Player.PickUp(itm))
                {
                    return "Item not found";
                }
                return "Item is now in your backpack";
            }
            return "You should speciffy item";
        }

        public string Name
        {
            get { return "pickup"; }
        }
    }
}