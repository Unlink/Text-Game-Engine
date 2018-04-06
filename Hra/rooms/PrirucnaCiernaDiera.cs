using System;
using System.Runtime.Serialization;
using GameFramework;
using Hra.items;

namespace Hra.rooms
{
    [Serializable]
    internal class PrirucnaCiernaDiera : Room, INoticableItem
    {
        public PrirucnaCiernaDiera(Room outRoom) : base("ciernadiera")
        {
            AddItem(new SmaragdoveOko());
            _namedObjects["von"] = outRoom;
        }

        public PrirucnaCiernaDiera(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt)
        {
        }

        public bool CanMove(Player player = null, Room room = null)
        {
            return true;
        }

        public void LocationChange(Room room, Player player)
        {
            _namedObjects.Remove("von");
            if (room != null)
            {
                _namedObjects["von"] = room;
            }
        }

        public new string Description
        {
            get { return "Príručná čierna diera ktorá smeruje do inej galaxie temna"; }
        }
    }
}