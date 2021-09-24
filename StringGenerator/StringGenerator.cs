using System;
using FakerLib.Interfaces;

namespace StringGenerator
{
    public class StringGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(string);
        }

        public object Generate(Type type)
        {
            return Guid.NewGuid().ToString();
        }
    }
}