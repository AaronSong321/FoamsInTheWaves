using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


namespace Jmas
{
    public enum SceneKind
    {
        Desktop,
        Mobile
    }
    
    public static class PlatformChecker
    {
        public static bool IsWinPhoneOrWinStore()
        {
            var platform = Application.platform;
            return platform == RuntimePlatform.WSAPlayerX64 || platform == RuntimePlatform.WSAPlayerX86 || platform == RuntimePlatform.WSAPlayerARM || platform == RuntimePlatform.XboxOne;
        }
        public static bool IsIos()
        {
            var platform = Application.platform;
            return platform == RuntimePlatform.IPhonePlayer;
        }
        public static bool IsWebPlayer()
        {
            var platform = Application.platform;
            return platform == RuntimePlatform.WebGLPlayer;
        }
        public static SceneKind GetSceneKind()
        {
            var platform = Application.platform;
            return platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer ? SceneKind.Mobile : SceneKind.Desktop;
        }
        public static string GetSceneKindString()
        {
            return GetSceneKind().ToString();
        }
    }
}
