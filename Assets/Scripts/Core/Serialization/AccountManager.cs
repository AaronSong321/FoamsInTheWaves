using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jmas;
using UnityEngine;


namespace Fitw
{
    public class AccountManager: Actor
    {
        private static string AccountPath {
            get => Path.Combine(Application.persistentDataPath, "Accounts.bin");
        }
        public static AccountManager Instance { get; private set; }
        private List<Account> Accounts { get; set; }
        public Account CurrentAccount { get; private set; }

        private void SelfInitImpl()
        {
            Instance = this;
            ReadAccounts();
        }
        
        void ReadAccounts()
        {
            if (File.Exists(AccountPath)) {
                Accounts = GameSerializer.ReadFromFile<List<Account>>(AccountPath);
                CurrentAccount = Accounts[Accounts.Count-1];
            }
            else
                Accounts = new List<Account>();
        }
        
        public string CreateAccount(string name, string psd, int age)
        {
            var account = Account.CreateAccount(name, psd, age);
            if (Accounts.Any(a => a.playerName == name)) {
                return "used name";
            }
            Accounts.Add(account);
            CurrentAccount = account;
            GameSerializer.SaveToFile(AccountPath, Accounts);
            return null;
        }

        public void CreateTestAccount()
        {
            CreateAccount("test", "ffffaaaa", 20);
        }
    }
}
