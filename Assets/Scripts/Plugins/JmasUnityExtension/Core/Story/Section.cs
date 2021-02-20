using System;

namespace Jmas
{
    /// <summary>
    /// A section is a story unit containing several scenes
    /// </summary>
    public interface ISection : IActor
    {
        
    }

    /// <summary>
    /// A section is a story unit containing several scenes. Section is not mandatory; for simplicity, a chapter can contain scenes directly.
    /// </summary>
    [Serializable]
    public class Section : StoryItem
    {
        private bool isCompleted;

        public override bool IsCompleted {
            get => isCompleted;
        }
        public override StoryItem GetCurrentStory()
        {
            throw new System.NotImplementedException();
        }
    }
}
