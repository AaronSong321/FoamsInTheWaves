using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


namespace Jmas
{
    public static class GameObjectFinder
    {
        public static GameObject FindChildByName(this MonoBehaviour obj, string name)
        {
            return obj.transform.Find(name).gameObject;
        }
    }
}
