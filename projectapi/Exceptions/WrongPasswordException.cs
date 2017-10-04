using System;

namespace projectapi.Processors
{
    internal class WrongPasswordException : Exception
    {
        public WrongPasswordException() {}
    }
}