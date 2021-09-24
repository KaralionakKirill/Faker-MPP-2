using System;
using System.Collections;
using System.Collections.Generic;
using FakerLib.Interfaces;

namespace FakerLib.Implementations.Generators
{
    public class ListGenerator : IGenerator
    {
        private readonly Random _random = new();
        public bool CanGenerate(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }

        public object Generate(Type type)
        {
            var list = (IList) Activator.CreateInstance(type);
            var amount = _random.Next();
            for (var i = 0; i < amount; i++)
            {
                list.Add(_random.Next());
            }

            return list;
        }
    }
}