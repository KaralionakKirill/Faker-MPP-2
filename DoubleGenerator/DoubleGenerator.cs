using System;
using FakerLib.Interfaces;

namespace DoubleGenerator
{
    public class DoubleGenerator : IGenerator
    {
        private readonly Random _random = new();
        public bool CanGenerate(Type type)
        {
            return type == typeof(double);
        }

        public object Generate(Type type)
        {
            return _random.NextDouble();
        }
    }
}