using System;

namespace Jmas
{
    /// <summary>
    /// A scene is a story unit that contains several dialogs and reflects directly to a scene (Unity Scene or UE4 Level)
    /// </summary>
    [Serializable]
    public class StoryScene : StoryItem
    {
        public override bool IsCompleted {
            get => false;
        }
        public override StoryItem GetCurrentStory()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Gets the name of the scene to load. Games built for different platforms store scenes for mobile and desktop version in separate folders, and that path need not be included in this name.
        /// </summary>
        public string SceneName { get; set; }
    }
}
