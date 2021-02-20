using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jmas;
using UnityEngine;


namespace Jmas
{
    public class UIFinder
    {
        public class Node
        {
            private static bool? isValueType;
            internal Node Child { get; set; }
            internal Node Parent { get; set; }
            internal Node Left { get; set; }
            internal Node Right { get; set; }
            public GameObject Value { get; }
            internal Node(GameObject value)
            {
                Value = value;
                if (isValueType is null) {
                    GameObject c = value;
                    isValueType = ReferenceEquals(c, value);
                }
                Left = null;
                Right = null;
            }

            public IEnumerable<Node> GetChildren()
            {
                var first = Child;
                if (first != null)
                    yield return first;
                else {
                    yield break;
                }
                while (first.Right != Child) {
                    first = first.Right;
                    yield return first;
                }
            }

            public Node FindChildNode(Node b)
            {
                if (b.Parent != this) {
                    throw new ArgumentException(nameof(b));
                }
                return GetChildren().FirstOrDefault(k => k == b);
            }

            public Node FindChild(string name)
            {
                var b = GetChildren().FirstOrDefault(t => t.Value.name == name);
                if (b is null) {
                    var c = Value.FindChildByName(name);
                    if (c != null) {
                        var k = new Node(c);
                        AddChild(k);
                        return k;
                    }
                    return null;
                }
                return b;
            }
            
            public void AddChild(Node child)
            {
                if (Child is null) {
                    Child = child;
                    child.Right = child;
                    child.Left = child;
                }
                else {
                    Child.Left.Right = child;
                    child.Left = Child.Left;
                    Child.Left = child;
                    child.Right = Child;
                }
                child.Parent = this;
            }

            public void RemoveChild(Node child)
            {
                if (child.Parent != this) {
                    throw new ArgumentException(nameof(child));
                }
                if (child == Child) {
                    if (child.Right == child) {
                        Child = null;
                    }
                    else {
                        Child = Child.Right;
                    }
                }
                child.Left.Right = child.Right;
                child.Right.Left = child.Left;
                child.Parent = null;
            }
        }

        public Node Root { get; }
        
        public UIFinder(GameObject value)
        {
            Root = new Node(value);
        }
        public GameObject FindChild(string path)
        {
            var obj = Root;
            string acc = string.Empty;
            var name = path.Split('/').Where(t => t.Length != 0);
            foreach (var n in name) {
                acc += n;
                obj = obj.FindChild(n);
                if (obj is null)
                    throw UIObjectNotFoundException.Create(Root.Value, acc);
            }
            return obj.Value;
        }
    }
}
