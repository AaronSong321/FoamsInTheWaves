using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jmas;
using Newtonsoft.Json;


namespace Fitw
{
    public class EndlessSorrowStoryCluster: StoryCluster
    {
        
        public override StoryLine LoadStory(Account account, GameSlot slot)
        {
            throw new NotImplementedException();
        }
        public override StoryLine CreateNewStory(Account account, GameSlot slot)
        {
            throw new NotImplementedException();
        }
    }
}
