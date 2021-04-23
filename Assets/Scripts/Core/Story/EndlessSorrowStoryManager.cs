using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jmas;
using UnityEngine;


namespace Fitw
{
    public class EndlessSorrowStoryManager: StoryManager
    {
        private EndlessSorrowStory story;
        private FitwGameState gameState;

        protected override void SelfInitImpl()
        {
            base.SelfInitImpl();
            UseTestAccount();
            DontDestroyOnLoad(this);
        }

        
    }
}
