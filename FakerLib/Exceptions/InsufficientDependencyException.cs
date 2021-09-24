using System;

namespace FakerLib.Exceptions
{
    public class InsufficientDependencyException : Exception
    {
        public InsufficientDependencyException(string message)
            : base(message)
        {
            
        }
    }
}