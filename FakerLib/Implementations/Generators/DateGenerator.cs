using System;
using FakerLib.Interfaces;

namespace FakerLib.Implementations.Generators
{
    public class DateGenerator : IGenerator
    {
        private readonly Random _random = new();
        
        public bool CanGenerate(Type type)
        {
            return type == typeof(DateTime);
        }

        public object Generate(Type type)
        {
            return new DateTime(_random.Next(1900, 2022), _random.Next(1, 12), _random.Next(1, 30));
        }
    }
}