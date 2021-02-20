using System.Collections;
using System.IO;

namespace Jmas
{
    public abstract class StoryCluster//: IStoryCluster
    {
        public StoryLine RootStory { get; set; }
        public abstract StoryLine LoadStory(Account account, GameSlot slot);
        public abstract StoryLine CreateNewStory(Account account, GameSlot slot);
        protected static bool CheckIdentity(Account account, GameSlot slot)
        {
            return account == slot.Owner;
        }
        
        public string ResolveSceneToLoad()
        {
            return RootStory.GetCurrentChapter().GetCurrentScene().SceneName;
        }
        public IEnumerator LoadNextSceneToContinue()
        {
            string scenePath = Path.Combine(PlatformChecker.GetSceneKindString(), ResolveSceneToLoad());
            var it = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scenePath);
            while (!it.isDone)
                yield return 0;
        }
    }
}
