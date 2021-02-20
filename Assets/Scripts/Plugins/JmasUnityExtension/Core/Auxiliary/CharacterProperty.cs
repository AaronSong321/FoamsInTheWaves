using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jmas;
using Newtonsoft.Json;


namespace Jmas
{
    [Serializable]
    public class CharacterProperty
    {
        [JsonProperty]
        private ResourceHolder holder;
        public CharacterProperty(int value = 0, int maxValue = Int32.MaxValue)
        {
            holder = new ResourceHolder(value, maxValue);
        }
        [JsonIgnore]
        public int Value {
            get => holder.Value;
            set => holder.Set(value);
        }
        public void Add(int v)
        {
            holder.Gain(v);
        }
        [JsonIgnore]
        public int MaxValue {
            get => holder.MaxValue;
        }
    }
}
