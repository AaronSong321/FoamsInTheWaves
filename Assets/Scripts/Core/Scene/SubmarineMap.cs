using UnityEngine;
using Jmas;

namespace Finw
{
    public class SubmarineMap : Map
    {
        private WaveGenerator waveGenerator;
        
        private void SelfInitImpl()
        {
            waveGenerator = this.FindChildByName("Water").GetComponent<WaveGenerator>();
        }
    }
}
