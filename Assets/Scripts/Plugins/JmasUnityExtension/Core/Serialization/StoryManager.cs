using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;


namespace Jmas
{
    // /// <summary>
    // /// A manager unit that loads and saves all the story-related objects in a game
    // /// </summary>
    // public interface IStoryCluster
    // {
    //     StoryLine RootStory { get; set; }
    //     StoryLine LoadStory(string file);
    //     StoryLine CreateNewStory(string file);
    //     /// <summary>
    //     /// Gets the name of the scene to be loaded
    //     /// </summary>
    //     /// <returns></returns>
    //     string ResolveSceneToLoad();
    // }

    /// <summary>
    /// A manager MonoBehaviour that is put into the first scene and loads StoryCluster
    /// </summary>
    public class StoryManager : Actor
    {
        // public T StoryCluster { get; set; }
        protected Account SessionAccount { get; set; }
        protected List<Account> AccountCache { get; set; }
        protected GameSlot SessionGameSlot { get; set; }
        protected List<GameSlot> GameSlotCache { get; set; }

        protected string accountFilePath;

        private void ReadAccounts()
        {
            if (File.Exists(accountFilePath)) {
                AccountCache = GameSerializer.ReadFromFile<List<Account>>(accountFilePath);
            }
            else {
                AccountCache = new List<Account>();
                GameSerializer.SaveToFile(accountFilePath, AccountCache);
            }
        }

        private Account CreateNewAccount(string name, string password)
        {
            var ac = Account.CreateAccount(name, password, 20);
            AccountCache.Add(ac);
            Directory.CreateDirectory(Directory.GetParent(accountFilePath).FullName);
            GameSerializer.SaveToFile(accountFilePath, AccountCache);
            return ac;
        }
        
        public Account LogIn(string name, string password)
        {
            var a = AccountCache.FirstOrDefault(ac => ac.playerName == name);
            if (a is null)
                return null;
            var s = GameSerializer.GetHash(password);
            if (s != a.passwordHash) {
                Debug.Log($"hash '{s}' != password '{password}'");
                return null;
            }
            SessionAccount = a;
            GameSlotCache = GetGameSlots(a).ToList();
            return a;
        }

        public Account SignUp(string name, string password)
        {
            if (Account.ContainsAccountIllegalChars(name) || HasAccount(name))
                return null;
            SessionAccount = CreateNewAccount(name, password);
            GameSlotCache = new List<GameSlot>();
            return SessionAccount;
        }

        public bool HasAccount(string name)
        {
            return AccountCache.Any(t => t.playerName == name);
        }
        public Account UseTestAccount()
        {
            const string testAccountName = "test1";
            const string testPassword = "fear";
            var account = HasAccount(testAccountName) ? LogIn(testAccountName, testPassword) : SignUp(testAccountName, testPassword);
            return account;
        }

        protected virtual void SelfInitImpl()
        {
            accountFilePath = Path.Combine(Application.persistentDataPath, "Players", "Accounts.bin");
            ReadAccounts();
            DontDestroyOnLoad(this);
        }

        public IEnumerable<GameSlot> GetGameSlots(Account account)
        {
            if (!Directory.Exists(account.GetSerializePath()))
                return Array.Empty<GameSlot>();
            var dirInfo = new DirectoryInfo(account.GetSerializePath());
            return dirInfo.GetFiles().Where(fi => fi.Name.EndsWith(".bin")).Select(fi => {
                string p = fi.Name.Substring(0, fi.Name.Length - ".bin".Length);
                return new GameSlot() {
                    Owner = account, Name = p
                };
            });
        }

        public GameSlot ChooseGameSlot(string name)
        {
            var k = GameSlotCache.First(t => t.Name == name);
            SessionGameSlot = k;
            return k;
        }

        public void DeleteGameSlot(string name)
        {
            foreach (var t in GameSlotCache) {
                if (t.Name == name) {
                    DeleteGameSlot(t);
                    return;
                }
            }
        }

        private void DeleteGameSlot(GameSlot slot)
        {
            File.Delete(slot.GetPath());
            GameSlotCache.Remove(slot);
        }

        public GameSlot CreateGameSlot(string name)
        {
            if (GameSlotCache.Any(t => t.Name == name))
                return null;
            var p = new GameSlot() {
                Name = name,
                Owner = SessionAccount
            };
            GameSlotCache.Add(p);
            SessionGameSlot = p;
            return p;
        }

        // private Action<float> a;
        // private void add_a(Action<float> arg1)
        // {
        //     var loc0 = a;
        //     IL0007:
        //     var loc1 = loc0;
        //     var loc2 = (Action<float>) Delegate.Combine(loc1, arg1);
        //     loc0 = Interlocked.CompareExchange(ref this.a, loc2, loc1);
        //     if (loc0 != loc1)
        //         goto IL0007;
        // }
    }
}
