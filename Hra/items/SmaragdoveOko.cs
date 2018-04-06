using System;
using GameFramework;

namespace Hra.items
{
    [Serializable]
    internal class SmaragdoveOko : IItem
    {
        public string Name
        {
            get { return "smaragd"; }
        }

        public string Description
        {
            get { return "Smaragdové oko je prekrásny diamant"; }
        }

        public bool CanMove(Player player = null, Room room = null)
        {
            return true;
        }
    }
}