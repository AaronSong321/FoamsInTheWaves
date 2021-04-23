using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jmas;
using UnityEngine;


namespace Jmas
{
    public sealed class UIObjectNotFoundException : Exception
    {
        private UIObjectNotFoundException(string message) : base(message) { }
        public static UIObjectNotFoundException Create(GameObject root, string uiObjectPath)
        {
            return new UIObjectNotFoundException($"UI object '{uiObjectPath}' in '{root.name}' not found");
        }
    }
    
    public class UIBase: Actor
    {
        protected UIFinder Finder { get; set; }
        protected virtual void SelfInitImpl()
        {
            Finder = new UIFinder(gameObject);
        }
        public GameObject Node(string path)
        {
            return Finder.FindChild(path);
        }
        public T Component<T>(string path)
        {
            return Finder.FindChild(path).GetComponent<T>();
        }
    }
}
