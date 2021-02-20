using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Jmas
{
    public static class GameSerializer
    {
        public static HashAlgorithm PasswordHashAlgorithm { get; set; } = SHA256.Create();
        public static Encoding PasswordEncoding { get; set; } = Encoding.Default;
        public static string GetHash(string s)
        {
            var encode = PasswordHashAlgorithm.ComputeHash(PasswordEncoding.GetBytes(s));
            return PasswordEncoding.GetString(encode);
        }
        private static byte[] EncodeString(string s)
        {
            return PasswordEncoding.GetBytes(s);
        }
        private static string DecodeBytes(byte[] b)
        {
            return PasswordEncoding.GetString(b);
        }
        
        private static JsonSerializerSettings Settings { get; } = new JsonSerializerSettings {
            PreserveReferencesHandling = PreserveReferencesHandling.All
        };
        private static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, Settings);
        }
        private static T Deserialize<T>(string s)
        {
            return JsonConvert.DeserializeObject<T>(s, Settings);
        }

        // public static T ReadFromFile<T>(string path)
        // {
        //     return Deserialize<T>(DecodeBytes(File.ReadAllBytes(path)));
        // }
        // public static void SaveToFile(string path, object o)
        // {
        //     File.WriteAllBytes(path, EncodeString(Serialize(o)));
        // }

        public static T ReadFromFile<T>(string path)
        {
            return Deserialize<T>(File.ReadAllText(path));
        }
        public static void SaveToFile(string path, object o)
        {
            SystemIO.Write(path, Serialize(o));
        }
    }
}
