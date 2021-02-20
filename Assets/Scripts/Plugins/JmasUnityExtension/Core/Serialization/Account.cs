using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;


namespace Jmas
{
    [Serializable]
    public class Account
    {
        public readonly string playerName;
        public readonly string passwordHash;
        public int Age { get; set; }

        [JsonConstructor]
        protected Account(string playerName, string passwordHash, int age)
        {
            this.playerName = playerName;
            this.passwordHash = passwordHash;
            Age = age;
        }


        public static Account CreateAccount(string playerName, string password, int age)
        {
            return new Account(playerName, GameSerializer.GetHash(password), age);
        }

        public string GetSerializePath()
        {
            return Path.Combine(Application.persistentDataPath, "Players", playerName);
        }

        private static char[] accountLegalChars = { '_', ' ' };
        public static bool ContainsAccountIllegalChars(string s)
        {
            return !s.All(ch => char.IsLetter(ch) || char.IsDigit(ch) || accountLegalChars.Contains(ch));
        }
    }
}
