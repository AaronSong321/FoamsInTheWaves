using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Jmas
{
    /// <summary>
    /// A storyline is contains a set of consecutive chapters
    /// </summary>
    public interface IStoryLine : IActor
    {
        
    }
    
    public class StoryLine: Actor, IStoryLine
    {
        
    }

    /// <summary>
    /// A section is a story unit containing several scenes
    /// </summary>
    public interface ISection : IActor
    {
        
    }

    /// <summary>
    /// A scene is a story unit that contains several dialogs and reflects directly to a scene (Unity Scene or UE4 Level)
    /// </summary>
    public interface IScene : IActor
    {
        
    }

}
