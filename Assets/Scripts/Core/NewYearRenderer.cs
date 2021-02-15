using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jmas;
using UnityEngine;


namespace Finw
{
    public class NewYearRenderer: Actor
    {
        [field: SerializeField] public int Year { get; set; }
        
        protected override void SelfInit()
        {
            Debug.Log("Happy new year.");
        }

        protected override void InterInit()
        {
            Debug.Log("new year 2");
        }

        public void Celebrate()
        {
            Debug.Log($"Happy year {Year}");
        }
    }
}
