using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;


namespace Jmas
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class ReadFromFactorySlotAttribute: Attribute
    {
        [CanBeNull] public string FactoryName { get; }
        public string PropertyName { get; }
        
        public ReadFromFactorySlotAttribute(string factoryName, string propertyName)
        {
            FactoryName = factoryName;
            PropertyName = propertyName;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ReadFromFactoryClassAttribute : Attribute
    {
        [CanBeNull] public string FactoryName { get; }
        
        public ReadFromFactoryClassAttribute(string scriptableObjectType)
        {
            FactoryName = scriptableObjectType;
        }
    }

    public sealed class InitSlotException : Exception
    {
        public InitSlotException(string message) : base(message) { }
        
    }
}
