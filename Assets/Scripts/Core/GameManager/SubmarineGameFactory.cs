using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jmas;
using UnityEngine;


namespace Fitw
{
    [CreateAssetMenu]
    public class SubmarineGameFactory: PrefabFactory
    {
        [SerializeField] private GameObject water;
    }
}
