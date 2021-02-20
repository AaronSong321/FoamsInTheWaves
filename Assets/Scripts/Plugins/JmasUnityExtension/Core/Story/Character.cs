using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Jmas
{
    /// <summary>
    /// A character that should be stored. Every character shall have a unique name
    /// </summary>
    public interface ICharacter
    {
        string CharacterName { get; }
        int GetIntimacy(ICharacter target);
    }
    
    [Serializable]
    public class Character: ICharacter
    {
        public readonly InitOnlyRef<string> characterName = new InitOnlyRef<string>();
        string ICharacter.CharacterName {
            get => characterName.Value;
        }
        public Dictionary<string, Intimacy> Intimacy { get; } = new Dictionary<string, Intimacy>();
        public int GetIntimacy(ICharacter target)
        {
            if (Intimacy.TryGetValue(target.CharacterName, out var b))
                return b.Value;
            return 0;
        }
    }
}
