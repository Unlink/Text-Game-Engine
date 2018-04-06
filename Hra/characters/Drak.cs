using System;
using System.Runtime.Serialization;
using GameFramework;

namespace Hra.characters
{
    [Serializable]
    internal class Drak : ICharacter, ISerializable
    {
        public Drak(Room room)
        {
            Room = room;
            room.PlayerEntered += (room1, player) => { Console.WriteLine(Name + ": Arrrrrrrrr!"); };
        }

        /// <summary>
        ///     Deserializuje objekt
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ctxt"></param>
        public Drak(SerializationInfo info, StreamingContext ctxt)
        {
            Room = (Room) info.GetValue("room", typeof (Room));
            Room.PlayerEntered += (room1, player) => { Console.WriteLine(Name + ": Arrrrrrrrr!"); };
        }

        public string Name
        {
            get { return "manarath"; }
        }

        public string Description
        {
            get { return "Strašlivý drak manarath"; }
        }

        public Room Room { get; set; }

        public void Ask(Player player)
        {
            Console.WriteLine("Arrrrrrrrr!");
        }

        /// <summary>
        ///     Serializuje objekt
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("room", Room);
        }
    }
}