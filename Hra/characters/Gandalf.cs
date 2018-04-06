using System;
using GameFramework;

namespace Hra.characters
{
    [Serializable]
    internal class Gandalf : ICharacter, IMovingDirection
    {
        private readonly Room _gandalfDestination;

        public Gandalf(Room gandalfDestination)
        {
            _gandalfDestination = gandalfDestination;
        }

        public string Name
        {
            get { return "gandalf"; }
        }

        public string Description
        {
            get { return "Sám veľký gandalf"; }
        }

        public Room Room { get; set; }

        public void Ask(Player player)
        {
            Console.WriteLine("Viem ťa zaviesť do veže cestovateľ");
        }

        public Room GetDestination()
        {
            return _gandalfDestination;
        }
    }
}