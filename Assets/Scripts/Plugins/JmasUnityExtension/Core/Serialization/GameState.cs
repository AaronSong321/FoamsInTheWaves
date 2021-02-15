using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Jmas
{
    public class GameSlot
    {
        public string FilePath { get; }
        public GameSlot(string filePath)
        {
            FilePath = filePath;
        }
    }
    
    [Serializable]
    public class GameState
    {
        public void Save(GameSlot slot)
        {
            
        }
    }
}
