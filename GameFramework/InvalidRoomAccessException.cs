using System;

namespace GameFramework
{
    public class InvalidRoomAccessException : Exception
    {
        public InvalidRoomAccessException(string message) : base(message)
        {
        }

        public InvalidRoomAccessException()
        {
        }
    }
}