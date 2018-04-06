using System;
using System.Runtime.Serialization;
using GameFramework;
using Hra.characters;
using Hra.items;

namespace Hra.rooms
{
    [Serializable]
    internal class DraciaJaskina : Room, ISerializable
    {
        public DraciaJaskina() : base("Dračia jaskiňa")
        {
            var drak = new Drak(this);
            AddCharacter(drak);
            ItemUsed += (room, player, item) =>
            {
                if (item is Dynamit)
                {
                    throw new EndGameException(drak.Name + " bol zničený, vyhrali ste");
                }
            };
            Locked = true;
        }

        /// <summary>
        ///     Deserializuje dáta
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ctxt"></param>
        public DraciaJaskina(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt)
        {
            Locked = (bool) info.GetValue("Locked", typeof (bool));
            var drak = new Drak(this);
            ItemUsed += (room, player, item) =>
            {
                if (item is Dynamit)
                {
                    throw new EndGameException(drak.Name + " bol zničený, vyhrali ste");
                }
            };
            Locked = true;
        }

        public bool Locked { get; set; }

        /// <summary>
        ///     Serialize object
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ctxt"></param>
        public new void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("Locked", Locked);
        }

        public override bool CheckAccess(Room room, IMovingDirection mediator, Player player)
        {
            if (Locked)
            {
                throw new InvalidRoomAccessException("Dračia jaskiňa je zamknutá, musíte nájsť kľúč");
            }
            return !Locked;
        }
    }
}