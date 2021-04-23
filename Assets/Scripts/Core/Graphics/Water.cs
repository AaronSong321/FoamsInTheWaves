using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jmas;
using UnityEngine;


namespace Fitw
{
    public class Water: WaveGenerator
    {
        [SerializeField] private GameObject wave;
        [SerializeField] private GameObject waterBase;

        private void SelfInitImpl()
        {
            wave = this.FindChildByName("Wave");
            waterBase = this.FindChildByName("WaterBase");
            waterMeshFilter = wave.GetComponent<MeshFilter>();
            var a = wave.transform.position;
            a.y = a.y/2 - waterBase.transform.localScale.y/2;
            waterBase.transform.position = a;
        }
    }
}
