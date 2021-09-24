using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FakerLib.Exceptions;
using FakerLib.Interfaces;

namespace FakerLib.Implementations
{
    public static class Faker
    {
        private static readonly List<IGenerator> Generators;

        static Faker()
        {
            var generatorType = typeof(IGenerator);
            var impls = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.GetInterfaces().Contains(generatorType) && type.IsClass)
                .Select(type => (IGenerator) Activator.CreateInstance(type));
            Generators = new List<IGenerator>(impls);
        }
        
        private static object Initialize(Type type)
        {
            var constructors = type.GetConstructors().ToList();
            foreach (var constructor in constructors)
            {
                try
                {
                    var pars = constructor
                        .GetParameters()
                        .Select(info => info.ParameterType)
                        .Select(Create);

                    return constructor.Invoke(pars.ToArray());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            throw new InsufficientDependencyException($"Cannot create object of type {type}");
        }

        private static void InitializeFields(object o)
        {
            var type = o.GetType();
            type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .ToList()
                .ForEach(field =>
                {
                    try
                    {
                        if (Equals(field.GetValue(o), GetDefaultValue(field.FieldType)))
                        {
                            field.SetValue(o, Faker.Create(field.FieldType));
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                });
        }

        private static object GetDefaultValue(Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }


        public static T Create<T>()
        {
            return (T) Create(typeof(T));
        }
        
        public static object Create(Type type)
        {
            foreach (var generator in Generators.Where(generator => generator.CanGenerate(type)))
            {
                return generator.Generate(type);
            }

            CyclicDependencyHelper.Assert(type);

            var obj = Initialize(type);
            InitializeFields(obj);
            return obj;
        }
    }
}