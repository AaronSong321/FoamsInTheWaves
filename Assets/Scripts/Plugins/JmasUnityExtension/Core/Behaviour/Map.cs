using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


namespace Jmas
{
    /// <summary>
    /// Map is the only Actor statically placed in the scene. It loads the <see cref="GameMode"/> in its <see cref="MonoBehaviour.Awake"/> function and let the <see cref="GameMode"/> do all the initialisation.
    /// </summary>
    public interface IMap : IActor
    {
        
    }

    public static class MapExtensions
    {
        
    }
    
    public class Map: Actor, IMap
    {
        [SerializeField] private GameObject GameModePrefab;
        private GameMode gameMode;

        protected override void Awake()
        {
            base.Awake();
            // Cannot wait for GameMode to call SelfInit on Map
            SelfInit();
            this.GetActors().Where(t => !(t is IGameMode)).ForEach(actor => actor.SelfInit());
            var gameModeObj = Instantiate(GameModePrefab);
            gameMode = gameModeObj.GetComponent<GameMode>();
            GameMode.Instance.map.Value = this;
        }
    }
}
