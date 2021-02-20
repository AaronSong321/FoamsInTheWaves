using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


namespace Jmas
{
    public class EnumerateNewYear: Actor
    {
        protected new virtual IEnumerator Start()
        {
            base.Start();
            Debug.Log("preparing ny");
            yield return new WaitForSeconds(1);
            Debug.Log("Ready to happy ny");
        }
    }
}
