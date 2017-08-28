using System;

namespace Assignment1
{
    public class NotFoundException : Exception
    {
        public string argument;
        public NotFoundException(string param){
            argument = param;
        }
    }
}