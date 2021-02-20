using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Jmas
{
    [Serializable]
    public class DummyChapter: Chapter
    {
        [JsonProperty]
        private int fieldToStore;
    }

    [Serializable]
    public class DummyScene : StoryScene
    {
        [JsonProperty]
        private int fieldToStore;
    }
}
