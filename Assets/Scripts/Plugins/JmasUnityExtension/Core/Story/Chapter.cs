using System;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

namespace Jmas
{
    /// <summary>
    /// A chapter is a large story unit containing several not necessarily consecutive sections or scenes directly
    /// </summary>
    [Serializable]
    public class Chapter : StoryItem
    {
        [JsonProperty] public bool ContainsSceneDirectly { get; set; }
        [JsonProperty] public Section[] SectionStories { get; set; }
        [JsonProperty] public StoryScene[] SceneStories { get; set; }
        
        public override StoryItem GetCurrentStory()
        {
            return GetCurrentScene();
        }
        public StoryScene GetCurrentScene()
        {
            return ContainsSceneDirectly ? SceneStories[CurrentStoryIndex] : SectionStories[CurrentStoryIndex].GetCurrentStory() as StoryScene;
        }
        public override bool IsCompleted {
            get => CurrentStoryIndex == (SectionStories?.Length ?? SceneStories.Length);
        }
    }
}
