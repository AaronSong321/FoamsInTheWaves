using UnityEngine;
using Jmas;

namespace Finw
{
    public class SubmarineMap : Map
    {
        private WaveGenerator waveGenerator;
        
        protected override void SelfInit()
        {
            base.SelfInit();
            waveGenerator = this.FindChildByName("Water").GetComponent<WaveGenerator>();
        }
    }
}
