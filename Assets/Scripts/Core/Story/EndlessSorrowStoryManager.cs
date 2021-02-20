using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jmas;
using UnityEngine;


namespace Finw
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
            var g = GetGameSlots(SessionAccount).ToArray();
            // DeleteGameSlot("1");
            var gs = g.Length is 0 ? CreateGameSlot((g.Length+1).ToString()): ChooseGameSlot(g[0].Name);
            var fitw = FitwGameState.MakeDefault();
            fitw.Save(gs);
        }
    }
}
