using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


namespace Jmas
{
    public class AsyncNewYear: Actor
    {
        protected new virtual async void Start()
        {
            base.Start();
            Debug.Log("preparing");
            // string p = await File.ReadAllTextAsync("/Users/aarons/Aaron/Projects/UnityGames/JmasUnityExtension/JmasUnityExtension/Test/DummyStory.cs");
            var t1 = Task.Delay(1000);
            var t2 = Task.Delay(1000);
            await t1;
            Debug.Log("read 1 completed");
            await t2;
            Debug.Log("read completed");
            // Debug.Log(p);
        }
    }
}
