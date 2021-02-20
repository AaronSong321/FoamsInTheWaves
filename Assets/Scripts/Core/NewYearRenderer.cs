using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jmas;
using Newtonsoft.Json;
using UnityEngine;


namespace Finw
{

    public class NewYearRenderer: Actor
    {
        [field: SerializeField] public int Year { get; set; }

        public void Celebrate()
        {
            
        }
    }
}
