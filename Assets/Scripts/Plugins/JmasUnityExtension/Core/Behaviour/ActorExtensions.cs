using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Jmas
{
    public static class ActorExtensions
    {
        private static readonly object[] EmptyArray = Array.Empty<object>();
        public static void CallMessageMethod(this object o, string funcName)
        {
            var method = o.GetType().GetMethod(funcName, ReflectionHelper.InstanceMemberFinder);
            if (method is null)
                return;
            if (method.GetParameters().Length != 0)
                return;
            if (method.ReturnType == typeof(void))
                method.Invoke(o, Array.Empty<object>());
            else if (method.ReturnType == typeof(Task)) {
                var task = method.Invoke(o, Array.Empty<object>()) as Task;
                System.Diagnostics.Debug.Assert(task != null, nameof(task) + " != null");
                task.GetAwaiter().GetResult();
            }
            else if (method.ReturnType.IsAssignableTo(typeof(IEnumerator)) && o is MonoBehaviour m) {
                m.StartCoroutine(funcName);
            }
            #if NET
            else if (method.ReturnType == typeof(ValueTask)) {
                var task = (ValueTask) method.Invoke(o, Array.Empty<object>());
                task.GetAwaiter().GetResult();
            }
            #endif
        }

        public static void DestroyAllChildren(this GameObject o)
        {
            var t = o.transform;
            foreach (Transform child in t) {
                Object.Destroy(child.gameObject);
            }
        }
    }
}
