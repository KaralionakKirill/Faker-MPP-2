using System;
using FakerLib.Interfaces;

namespace FakerLib.Implementations.Generators
{
    public class IntegerGenerator : IGenerator
    {
        private readonly Random _random = new();

        public bool CanGenerate(Type type)
        {
            return type == typeof(int);
        }

        public object Generate(Type type)
        {
            return _random.Next();
        }
    }
}