using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Jmas
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigAttribute: Attribute
    {
        public string Path { get; }
        public string SectionName { get; }
        
        public ConfigAttribute(string path, string sectionName) => (Path, SectionName) = (path, sectionName);
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ConfigPropertyAttribute : Attribute
    {
        public string Path { get; }
        public string SectionName { get; }
        public string ItemName { get; }
        public bool IsArray { get; }
        
        public ConfigPropertyAttribute(string path, string sectionName, string itemName, bool isArray)
        {
            Path = path;
            SectionName = sectionName;
            ItemName = itemName;
            IsArray = isArray;
        }
    }
}
