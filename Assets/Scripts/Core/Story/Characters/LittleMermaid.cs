using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jmas;


namespace Finw
{
    public sealed class LittleMermaid: PlayerCharacter
    {
        public InitOnlyRef<CharacterProperty> sorrow = new InitOnlyRef<CharacterProperty>();
        public InitOnlyRef<CharacterProperty> intelligence = new InitOnlyRef<CharacterProperty>();
        public InitOnlyRef<ResourceHolder> money = new InitOnlyRef<ResourceHolder>();
    }
}
