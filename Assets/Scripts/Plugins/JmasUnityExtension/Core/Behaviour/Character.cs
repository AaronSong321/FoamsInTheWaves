using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Jmas
{
    public interface ICharacter : IActor
    {
        string CharacterName { get; }
    }
    
    public class Character: Actor, ICharacter
    {
        public readonly InitOnlyRef<string> characterName = new InitOnlyRef<string>();
        string ICharacter.CharacterName {
            get => characterName.Value;
        }
        
    }
}
