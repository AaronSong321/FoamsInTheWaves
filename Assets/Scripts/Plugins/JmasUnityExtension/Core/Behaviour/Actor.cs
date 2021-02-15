using System;
using UnityEngine;

namespace Jmas
{
    public interface IActor
    {
        void SelfInit();
        void InterInit();
        void OnUniAppQuit();
        IGameMode GetGameMode();
    }

    public sealed class ReInitException : Exception
    {
        public ReInitException(string message): base(message) { }
    }
    
    public class Actor : MonoBehaviour, IActor
    {
        protected virtual void Awake()
        {
        }
        protected virtual void Start()
        {
        }
        protected virtual void Update()
        {
        }
        protected virtual void OnDestroy()
        {
        }

        private bool selfInitToken = false;
        protected virtual void SelfInit()
        {
            if (selfInitToken)
                throw new ReInitException($"Re SelfInit: {GetType()} at object {gameObject.name}");
            selfInitToken = true;
        }
        void IActor.SelfInit()
        {
            SelfInit();
        }
        private bool interInitToken = false;
        protected virtual void InterInit()
        {
            if (interInitToken)
                throw new ReInitException($"Re InterInit: {GetType()} at object {gameObject.name}");
            interInitToken = true;
        }
        void IActor.InterInit()
        {
            InterInit();
        }
        protected virtual void OnUniAppQuit()
        {
        }
        void IActor.OnUniAppQuit()
        {
            OnUniAppQuit();
        }
        IGameMode IActor.GetGameMode()
        {
            return GameMode.Instance;
        }

        protected void OnApplicationQuit()
        {
            if (!PlatformChecker.IsWinPhoneOrWinStore())
                OnUniAppQuit();
        }
        protected void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus && PlatformChecker.IsWinPhoneOrWinStore()) {
                OnUniAppQuit();
            }
        }
    }
}