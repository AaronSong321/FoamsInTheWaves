using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Jmas
{
    [Serializable]
    public class GameState
    {
        [JsonIgnore]
        public float LastIntermediateSaveTime { get; private set; }
        /// <summary>
        /// A unique index for every game state.
        /// This should be static, but how to serialize a static property?
        /// </summary>
        [JsonProperty] private int gameStateUniqueIndex;
        [JsonIgnore]
        private int GameStateUniqueIndex {
            get => gameStateUniqueIndex;
            set {
                if (value >= nextGameStateIndex)
                    nextGameStateIndex = value + 1;
                gameStateUniqueIndex = value;
            }
        }
        private static int nextGameStateIndex;
        public DateTime LastSaveTime { get; private set; }
        public float TimePlayed { get; private set; }
        
        public void Save(GameSlot slot)
        {
            if (GameMode.Instance != null) {
                float now = GameMode.Instance.CurrentTime;
                TimePlayed += LastIntermediateSaveTime - now;
                LastIntermediateSaveTime = now;
            }
            LastSaveTime = DateTime.Now;
            GameSerializer.SaveToFile(slot.GetPath(), this);
        }

        public static T Read<T>(GameSlot slot) where T: GameState
        {
            return GameSerializer.ReadFromFile<T>(slot.GetPath());
        }
        
    }
}
