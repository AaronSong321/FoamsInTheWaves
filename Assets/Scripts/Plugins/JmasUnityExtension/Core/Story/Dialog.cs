namespace Jmas
{
    /// <summary>
    /// A dialog is an individable story unit that contains several story items, choices or backgrounds
    /// </summary>
    public class Dialog: StoryItem
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
