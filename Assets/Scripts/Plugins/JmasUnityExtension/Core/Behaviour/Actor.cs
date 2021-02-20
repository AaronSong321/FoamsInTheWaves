using System;
using UnityEngine;

namespace Jmas
{
    public interface IActor
    {
        /// <summary>
        /// Called to init this actor itself. It is guaranteed that every <see cref="IActor"/> statically registered in the <see cref="GameMode"/> calls <see cref="SelfInit"/> sooner than any <see cref="InterInit"/>.
        /// It is the implementer's job to ensure this function is called only once on each <see cref="IActor"/>.
        /// </summary>
        void SelfInit();
        void InterInit();
        IGameMode GetGameMode();
    }

    public sealed class ReInitException : Exception
    {
        public ReInitException(string message): base(message) { }
    }
    
    public class Actor : MonoBehaviour, IActor
    {
        public static readonly string SelfInitName = "SelfInitImpl";
        public static readonly string InterInitName = "InterInitImpl";
        
        protected virtual void Awake()
        {
        }
        protected virtual void Start()
        {
            SelfInit();
            InterInit();
        }
        protected virtual void Update()
        {
        }
        protected virtual void OnDestroy()
        {
        }

        private bool selfInitToken = false;
        public void SelfInit()
        {
            if (!selfInitToken)
                this.CallMessageMethod(SelfInitName);
            selfInitToken = true;
        }
        private bool interInitToken = false;
        public void InterInit()
        {
            if (!interInitToken)
                this.CallMessageMethod(InterInitName);
            interInitToken = true;
        }
        
        IGameMode IActor.GetGameMode()
        {
            return GameMode.Instance;
        }

        // protected void OnApplicationQuit()
        // {
        //     if (!PlatformChecker.IsWinPhoneOrWinStore())
        //         OnUniAppQuit();
        // }
        // protected void OnApplicationFocus(bool hasFocus)
        // {
        //     if (!hasFocus && PlatformChecker.IsWinPhoneOrWinStore()) {
        //         OnUniAppQuit();
        //     }
        // }
    }
}