using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jmas;
using Newtonsoft.Json;
using UnityEngine;


namespace Fitw
{

    public class NewYearRenderer: Actor
    {
        [field: SerializeField] public int Year { get; set; }

        public void Celebrate()
        {
            
        }
    }
}
