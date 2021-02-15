using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


namespace Jmas
{
    [CreateAssetMenu]
    public abstract class PrefabFactory: ScriptableObject
    {
        public GameObject GetPrefab(string prefabName)
        {
            var prefab = (GameObject)GetType().GetField(prefabName, ReflectionHelper.InstanceMemberFinder)?.GetValue(this);
#if DEBUG
            UnityEngine.Assertions.Assert.IsNotNull(prefab, "Prefab == null!");
#endif
            return prefab;
        }
    }
}
