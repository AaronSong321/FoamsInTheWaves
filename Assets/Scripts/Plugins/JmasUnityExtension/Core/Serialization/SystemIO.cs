using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Jmas
{
    public static class SystemIO
    {
        public static void Write(string path, string contents)
        {
            Directory.CreateDirectory(Directory.GetParent(path).FullName);
            File.WriteAllText(path, contents);
        }
        public static void Write(string path, string[] contents)
        {
            Directory.CreateDirectory(Directory.GetParent(path).FullName);
            File.WriteAllLines(path, contents);
        }
        public static void Write(string path, byte[] contents)
        {
            Directory.CreateDirectory(Directory.GetParent(path).FullName);
            File.WriteAllBytes(path, contents);
        }
    }
}
