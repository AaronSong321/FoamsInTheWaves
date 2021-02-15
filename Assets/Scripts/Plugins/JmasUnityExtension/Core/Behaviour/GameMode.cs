using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


namespace Jmas
{
    public interface IGameMode : IActor
    {
        PrefabFactoryManager PrefabFactoryManager { get; }
        event Action<float> GameTimeTicker;
        float DeltaTime { get; }
        float TimeScale { get; }
        float CurrentTime { get; }
        IMap Map { get; set; }
        void GameOver();
        void InitPrefabFactory();
        GameObject gameObject { get; }
    }

    /// <summary>
    /// Provides functionality of Interface <see cref="IGameMode"/>
    /// Would be much better if Unity supports C# 9 and interfaces can have default method implementations
    /// </summary>
    public static class GameModeExtensions
    {
        public static void SetInitSlot(this IGameMode actor)
        {
            var type = actor.GetType();
            if (!type.IsAssignableTo(typeof(MonoBehaviour)))
                return;
            PrefabFactory defaultFactory = null;
            if (type.GetCustomAttribute(typeof(ReadFromFactoryClassAttribute)) is ReadFromFactoryClassAttribute attr) {
                defaultFactory = actor.PrefabFactoryManager.GetFactory(attr.FactoryName);
            }
            var fields = type.GetFields(ReflectionHelper.InstanceMemberFinder);
            foreach (var field in fields) {
                if (field.FieldType.IsAssignableTo(typeof(IActor)) || field.FieldType.IsAssignableTo(typeof(GameObject))) {
                    var initSlot = field.GetCustomAttribute(typeof(ReadFromFactorySlotAttribute)) as ReadFromFactorySlotAttribute;
                    if (initSlot is null)
                        continue;
                    var factory = !(initSlot.FactoryName is null) ? actor.PrefabFactoryManager.GetFactory(initSlot.FactoryName) : defaultFactory;
                    if (factory is null)
                        throw new InitSlotException($"PrefabFactory of type {initSlot.FactoryName} doesn't exist.");
                    if (!(factory.GetType().GetField(initSlot.PropertyName, ReflectionHelper.InstanceMemberFinder)?.GetValue(factory) is GameObject prefab))
                        throw new InitSlotException($"type {initSlot.FactoryName} doesn't have a field {initSlot.PropertyName} typed {nameof(PrefabFactory)}");
                    var newObj = UnityEngine.Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, actor.gameObject.transform);
                    if (field.FieldType.IsAssignableTo(typeof(GameObject))) {
                        ReflectionHelper.SetField(actor, field, newObj);
                    }
                    else if (field.FieldType.IsAssignableTo(typeof(IActor))) {
                        ReflectionHelper.SetField(actor, field, newObj.GetComponent(field.FieldType));
                    }
                }
            }
            var properties = type.GetProperties(ReflectionHelper.InstanceMemberFinder);
            foreach (var property in properties) {
                if (property.PropertyType.IsAssignableTo(typeof(IActor)) || property.PropertyType.IsAssignableTo(typeof(GameObject))) {
                    var initSlot = property.GetCustomAttribute(typeof(ReadFromFactorySlotAttribute)) as ReadFromFactorySlotAttribute;
                    if (initSlot is null)
                        continue;
                    var factory = !(initSlot.FactoryName is null) ? actor.PrefabFactoryManager.GetFactory(initSlot.FactoryName) : defaultFactory;
                    if (factory is null)
                        throw new InitSlotException($"PrefabFactory of type {initSlot.FactoryName} doesn't exist.");
                    if (!(factory.GetType().GetField(initSlot.PropertyName, ReflectionHelper.InstanceMemberFinder)?.GetValue(factory) is GameObject prefab))
                        throw new InitSlotException($"type {initSlot.FactoryName} doesn't have a field {initSlot.PropertyName} typed {nameof(PrefabFactory)}");
                    var newObj = UnityEngine.Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, actor.gameObject.transform);
                    if (property.PropertyType.IsAssignableTo(typeof(GameObject))) {
                        ReflectionHelper.SetProperty(actor, property, newObj);
                    }
                    else if (property.PropertyType.IsAssignableTo(typeof(IActor))) {
                        ReflectionHelper.SetProperty(actor, property, newObj.GetComponent(property.PropertyType));
                    }
                }
            }
        }

        public static IEnumerable<IActor> GetActors(this IActor actor)
        {
            var type = actor.GetType();
            var fields = type.GetFields(ReflectionHelper.InstanceMemberFinder).Where(t => t.FieldType.IsAssignableTo(typeof(IActor)));
            foreach (var fieldInfo in fields) {
                if (fieldInfo.GetValue(actor) is IActor p)
                    yield return p;
            }
            foreach (var fieldInfo in type.GetFields(ReflectionHelper.InstanceMemberFinder).Where(t => t.FieldType.IsAssignableTo(typeof(InitOnlyRef<IActor>)))) {
                if (fieldInfo.GetValue(actor) is InitOnlyRef<IActor> p) {
                    var pt = p.TryGet();
                    if (pt != null)
                        yield return pt;
                }
            }
        }

        /// <summary>
        /// Call this function in your GameMode's Awake function.
        /// Note this is not an extension method. If it is, the name will be confused.
        /// </summary>
        /// <param name="gameMode"></param>
        public static void Awake(IGameMode gameMode)
        {
            gameMode.SelfInit();
            gameMode.InitPrefabFactory();
            var fieldActors = gameMode.GetActors().ToList();
            fieldActors.ForEach(t => t.SelfInit());
            gameMode.SetInitSlot();
            gameMode.InterInit();
            fieldActors.ForEach(t => t.InterInit());
        }
    }

    public class GameMode: Actor, IGameMode
    {
        public static GameMode Instance { get; private set; }
        public event Action<float> GameTimeTicker;
        [field: SerializeField] public float TimeScale { get; protected set; } = 1f;
        public float DeltaTime {
            get => TimeScale * Time.deltaTime;
        }
        [field: SerializeField] public float CurrentTime { get; private set; }
        public PrefabFactoryManager PrefabFactoryManager { get; protected set; }
        GameObject IGameMode.gameObject {
            get => gameObject;
        }
        public readonly InitOnlyRef<Map> map = new InitOnlyRef<Map>();
        IMap IGameMode.Map {
            get => map.Value;
            set => map.Value = value as Map;
        }

        protected virtual void InitPrefabFactory()
        {
            PrefabFactoryManager = gameObject.AddComponent<PrefabFactoryManager>();
        }
        void IGameMode.InitPrefabFactory()
        {
            InitPrefabFactory();;
        }
        protected virtual void GameOver()
        {
            Debug.Log("Game Over");
        }
        void IGameMode.GameOver()
        {
            GameOver();
        }

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
            GameModeExtensions.Awake(this);
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
            GameTimeTicker?.Invoke(DeltaTime);
            CurrentTime += DeltaTime;
        }
    }
}
