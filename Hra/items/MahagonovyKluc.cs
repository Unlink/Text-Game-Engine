using System;
using GameFramework;

namespace Hra.items
{
    [Serializable]
    internal class MahagonovyKluc : IUsable
    {
        public string Name
        {
            get { return "kluc"; }
        }

        public string Description
        {
            get { return "Mahagonový kľúč ktorý slúži na odomknutie dverí k drakovi"; }
        }

        public bool CanMove(Player player = null, Room room = null)
        {
            return true;
        }

        public void Use(Player player, Room room = null)
        {
            /*if (room != null && room.Name == "Hlavná jaskyna")
                ((DraciaJaskina) room.GetRoom("sever")).Locked = false;*/
        }
    }
}