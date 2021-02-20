using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Jmas
{
    /// <summary>
    /// A character that a player can play as
    /// </summary>
    public interface IPlayerCharacter : ICharacter
    {
        
        
    }
    
    public class PlayerCharacter: Character, IPlayerCharacter
    {
        
    }
}
