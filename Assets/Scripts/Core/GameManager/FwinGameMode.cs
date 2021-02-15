using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jmas;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Finw
{
    public class FwinGameMode: GameMode
    {
        [field: SerializeField] public int Year { get; set; }
        [SerializeField] private GameObject prefabManagerPrefab;
        [ReadFromFactorySlot(nameof(FinwPrefabManager.nyGenerator), "nyRenderer")]
        public NewYearRenderer Renderer { get; private set; }
        
        protected override void InitPrefabFactory()
        {
            var go = Instantiate(prefabManagerPrefab, Vector3.zero, Quaternion.identity, transform);
            PrefabFactoryManager = go.GetComponent<FinwPrefabManager>();
            Year = 2021;
        }

        protected override void Start()
        {
            base.Start();
            Renderer.Year = Year;
            Renderer.Celebrate();
        }
    }
}
