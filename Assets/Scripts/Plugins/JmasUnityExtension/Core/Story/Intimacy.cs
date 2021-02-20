using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Jmas
{
    /// <summary>
    /// The intimacy from this character to another
    /// Only records the target character, as "this" character should be implied by "this"
    /// </summary>
    public class Intimacy
    {
        [JsonProperty(IsReference = true)]
        public ICharacter To { get; }
        private ResourceHolder v;
        /// <summary>
        /// Gets the value of the intimacy.
        /// The setter is for Json serialization use only.
        /// </summary>
        [JsonProperty]
        public int Value {
            get => v.Value;
            private set => v = v is null ? new ResourceHolder(value) : new ResourceHolder(value, v.MaxValue);
        }

        public Intimacy(ICharacter c)
        {
            To = c;
            v = new ResourceHolder();
        }
    }
}
