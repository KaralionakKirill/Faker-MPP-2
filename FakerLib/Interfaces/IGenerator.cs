using System;

namespace FakerLib.Interfaces
{
    public interface IGenerator
    {
        bool CanGenerate(Type type);

        object Generate(Type type);
    }
}