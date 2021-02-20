using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jmas;
using UnityEngine;


namespace JmasUnityExtension.Core.Auxiliary
{
    public class Timer: IDisposable
    {
        private float time;
        private bool active;
        private float countTo;
        private readonly IGameMode gameMode; 
        
        public Timer(IGameMode gameMode)
        {
            gameMode.GameTimeTicker += Tick;
            this.gameMode = gameMode;
        }

        public void Count(float timeToCount)
        {
            if (active) 
                Debug.LogWarning($"Timer is up");
            active = true;
            time = 0;
            countTo = timeToCount;
        }

        public void Reset()
        {
            time = 0f;
            active = false;
        }

        private void Tick(float dt)
        {
            time += dt;
        }

        public void Dispose()
        {
            gameMode.GameTimeTicker -= Tick;
        }
    }
}
