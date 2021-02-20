using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jmas;
using Newtonsoft.Json;


namespace Finw
{
    [Serializable]
    public class FitwGameState: GameState
    {
        [JsonProperty]
        private EndlessSorrowStory mainStory;
        [JsonProperty]
        private Character[] characterList;

        public static FitwGameState MakeDefault()
        {
            var lm = new LittleMermaid {
                sorrow = { Value = new CharacterProperty(3) }, 
                intelligence = { Value = new CharacterProperty(4) }, 
                money = { Value = new ResourceHolder(UnityEngine.Random.Range(0, 1000), 10000) }
            };

            var mainStory = new EndlessSorrowStory() {
                RandomInt = UnityEngine.Random.Range(0, 10000),
                DisplayName = "Endless Sorrow",
                Name = "Endless Sorrow"
            };
            
            var ch1 = new DummyChapter() {
                ContainsSceneDirectly = true,
                DisplayName = "Childhood",
                Parent = mainStory
            };
            mainStory.Chapters = new Chapter[] { ch1 };
            
            var ch1sc1 = new DummyScene {
                DisplayName = "First sight about the surface",
                Parent = ch1
            };
            var ch1sc2 = new DummyScene {
                DisplayName = "Second sight about the surface",
                Parent = ch1
            };
            var ch1sc3 = new DummyScene {
                DisplayName = "Third sight about the surface",
                Parent = ch1
            };
            ch1.SceneStories = new StoryScene[] { ch1sc1, ch1sc2, ch1sc3 };
            
            var gs = new FitwGameState {
                characterList = new Character[] { lm },
                mainStory = mainStory
            };
            return gs;
        }
    }
}
