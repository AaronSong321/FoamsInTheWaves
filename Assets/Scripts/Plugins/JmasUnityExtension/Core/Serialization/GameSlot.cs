using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;


namespace Jmas
{
    // [Serializable]
    public class GameSlot
    {
        // [JsonProperty(ItemIsReference = true)
        public Account Owner { get; set; }
        public string Name { get; set; }
        private const string GameSlotFileSuffix = ".bin";

        public string GetPath()
        {
            return Path.Combine(Application.persistentDataPath, "Players",  Owner.playerName, Name + GameSlotFileSuffix);
        }
    }
}
