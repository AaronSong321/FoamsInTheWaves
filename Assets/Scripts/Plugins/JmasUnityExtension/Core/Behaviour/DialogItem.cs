using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Jmas
{
    /// <summary>
    /// A dialog item to be recorded
    /// </summary>
    public abstract class DialogItem
    {
        
    }

    public class Soleloquy : DialogItem
    {
        
    }

    /// <summary>
    /// A choose that provides several choice
    /// </summary>
    public class DialogBranch : DialogItem
    {
        private DialogChoice[] choiceGroup;
        public DialogBranch(params DialogChoice[] choices)
        {
            choiceGroup = choices;
        }
    }

    public class DialogChoice : DialogItem
    {
        
    }
}
