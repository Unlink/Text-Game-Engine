using System;
using GameFramework;

namespace Hra.items
{
    [Serializable]
    internal class Dynamit : IUsable
    {
        public string Name
        {
            get { return "dynamit"; }
        }

        public string Description
        {
            get { return "Strašne výbušná substancia"; }
        }

        public bool CanMove(Player player = null, Room room = null)
        {
            return true;
        }

        public void Use(Player player, Room room = null)
        {
            Console.WriteLine("Buuuuuuuuum");
        }
    }
}