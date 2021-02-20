using System;
using System.Text;
using Newtonsoft.Json;

namespace Jmas
{
    [Serializable]
    public abstract class StoryItem
    {
        /// <summary>
        /// A unique name for every story item to be stored in permanent storage.
        /// To distinct from other items, it is usually a path, such as ChapterName.SectionName.SceneName
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// A name to show the player.
        /// It is a simple name and can be the same as other items, but preferably unique.
        /// This name is also locale-oriented.
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// The index of this item in its parent story item.
        /// Usually this is the index in its parent's StoryItem container's index plus 1.
        /// </summary>
        public int SequentialStoryIndex { get; set; }
        /// <summary>
        /// The story to proceed at this point
        /// </summary>
        public int CurrentStoryIndex { get; set; }
        /// <summary>
        /// How many times have this item been visited
        /// </summary>
        public int VisitTimes { get; set; }
        [JsonIgnore]
        public bool IsVisited {
            get => VisitTimes != 0;
        }
        [JsonProperty(IsReference = true)]
        public StoryItem Parent { get; set; }
        public abstract bool IsCompleted { get; }
        /// <summary>
        /// Gets the current story
        /// </summary>
        /// <returns>The story to be loaded, null if this story item is completed. Also makes sense to load the last story item or any item.</returns>
        public abstract StoryItem GetCurrentStory();
        public string GetDefaultUniqueName()
        {
            var sb = new StringBuilder();

            void A(StoryItem i)
            {
                if (i is null)
                    return;
                A(i.Parent);
                sb.Append(i.GetType().Name + i.SequentialStoryIndex + '.');
            }

            A(Parent);
            sb.Append(GetType().Name);
            sb.Append(SequentialStoryIndex);
            return sb.ToString();
        }
        
    }
}
