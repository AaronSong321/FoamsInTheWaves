using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;


namespace Jmas
{

    /// <summary>
    /// A storyline is contains a set of consecutive chapters. Storyline is the largest story unit, and typically a whole set of story is a storyline, or a dlc forms a storyline.
    /// </summary>
    [Serializable]
    public class StoryLine: StoryItem
    {
        public Chapter[] Chapters { get; set; }
        public override bool IsCompleted {
            get => Chapters.All(t => t.IsCompleted);
        }
        public override StoryItem GetCurrentStory()
        {
            return GetCurrentChapter();
        }
        public Chapter GetCurrentChapter()
        {
            return Chapters[CurrentStoryIndex];
        }
    }

}
