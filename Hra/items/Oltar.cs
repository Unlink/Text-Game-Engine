using System;
using System.Runtime.Serialization;
using GameFramework;

namespace Hra.items
{
    [Serializable]
    internal class Oltar : IUsable, ISerializable
    {
        private readonly Room _chramRoom;

        /// <summary>
        ///     Má smaragd v sebe?
        /// </summary>
        private bool _hasSmaragd;

        public Oltar(Room chramRoom)
        {
            _hasSmaragd = false;
            _chramRoom = chramRoom;
            chramRoom.ItemDroped += ((room, player, item) => { if (item is SmaragdoveOko) _hasSmaragd = true; });
            chramRoom.ItemPicked += ((room, player, item) => { if (item is SmaragdoveOko) _hasSmaragd = false; });
        }

        /// <summary>
        ///     Deserializuje dáta
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ctxt"></param>
        public Oltar(SerializationInfo info, StreamingContext ctxt)
        {
            _hasSmaragd = (bool) info.GetValue("_hasSmaragd", typeof (bool));
            _chramRoom = (Room) info.GetValue("_chramRoom", typeof (Room));
            _chramRoom.ItemDroped += ((room, player, item) => { if (item is SmaragdoveOko) _hasSmaragd = true; });
            _chramRoom.ItemPicked += ((room, player, item) => { if (item is SmaragdoveOko) _hasSmaragd = false; });
        }

        /// <summary>
        ///     Serialize object
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ctxt"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("_hasSmaragd", _hasSmaragd);
            info.AddValue("_chramRoom", _chramRoom);
        }

        public string Name
        {
            get { return "oltar"; }
        }

        public string Description
        {
            get { return "Krásny prastarý oltár s miestom do ktorého je možné umiesniť nejaký drahokam"; }
        }

        public bool CanMove(Player player = null, Room room = null)
        {
            return false;
        }

        public void Use(Player player, Room room = null)
        {
            if (_hasSmaragd && room != null)
            {
                room.RemoveItem("smaragd");
                room.AddItem(new MahagonovyKluc());
                Console.WriteLine("Smaragd sa zmenil na novučičký mahagonový klúč");
            }
            else
            {
                Console.WriteLine("Nič sa nedeje");
            }
        }
    }
}