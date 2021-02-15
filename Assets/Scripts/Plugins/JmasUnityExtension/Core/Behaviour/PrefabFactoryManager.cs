using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Jmas
{
    public class PrefabFactoryManager: Actor
    {
        public T GetFactory<T>() where T: PrefabFactory
        {
            return GetType().GetFields(ReflectionHelper.InstanceMemberFinder).First(f => f.FieldType.IsAssignableTo(typeof(T))).GetValue(this) as T;
        }
        public PrefabFactory GetFactory(string factoryName)
        {
            return GetType().GetFields(ReflectionHelper.InstanceMemberFinder).First(f => f.FieldType.IsAssignableTo(typeof(PrefabFactory)) && f.Name == factoryName).GetValue(this) as PrefabFactory;
        }
    }
}
