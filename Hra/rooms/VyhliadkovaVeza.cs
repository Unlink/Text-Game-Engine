using System;
using System.Runtime.Serialization;
using GameFramework;
using Hra.characters;

namespace Hra.rooms
{
    [Serializable]
    internal class VyhliadkovaVeza : Room
    {
        public VyhliadkovaVeza(string name) : base("Vyhladkova Veza")
        {
        }

        /// <summary>
        ///     Deserializuje dáta
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ctxt"></param>
        public VyhliadkovaVeza(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt)
        {
        }

        public override bool CheckAccess(Room room, IMovingDirection mediator, Player player)
        {
            if (!(mediator is Gandalf) && room is Rozcestie)
            {
                throw new InvalidRoomAccessException(
                    "Rebrík do veže je zlomený, skús najsť niekoho kto ti pomôže sa dostať do veže");
            }
            return true;
        }
    }
}