using System.Collections.Generic;
using System.IO;
using System.Linq;
using Jmas;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Fitw.UI
{
    public class WelcomeUI : UIBase
    {
        private Button BStoryList;
        public EndlessSorrowStoryManager storyManager;
        private Account account;
        private Canvas uiRoot;
        private Canvas CMenuButtons;
        private GameObject PGameSlots;
        private Button BContinue;
        private Button BNewGame;
        private Button BExit;
        private Button BGameSlotsPanelEscape;
        private Dictionary<Button, string> gameSlotDictionary = new Dictionary<Button, string>();
        [SerializeField] private GameObject PrefabGameSlotButton;

        protected override void SelfInitImpl()
        {
            base.SelfInitImpl();
            LoadUI();
            LoadStories();
        }

        private void LoadUI()
        {
            storyManager = GameObject.Find("StoryManager").GetComponent<EndlessSorrowStoryManager>();
            storyManager.CallMessageMethod("SelfInit");
            CMenuButtons = Node("Canvas/CMenuButtons").GetComponent<Canvas>();
            BContinue = CMenuButtons.gameObject.FindChildByName("BContinue").GetComponent<Button>();
            BContinue.onClick.AddListener(OnClickContinue);
            BNewGame = CMenuButtons.gameObject.FindChildByName("BNewGame").GetComponent<Button>();
            BNewGame.onClick.AddListener(OnClickNewGame);
            BExit = CMenuButtons.gameObject.FindChildByName("BExit").GetComponent<Button>();
            BExit.onClick.AddListener(OnClickExit);
            PGameSlots = Node("Canvas/PGameSlots");
            PGameSlots.SetActive(false);
            BGameSlotsPanelEscape = PGameSlots.FindChildByName("BEscape").GetComponent<Button>();
            BGameSlotsPanelEscape.onClick.AddListener(OnClickGameSlotsPanelEscape);
            account = storyManager.UseTestAccount();
        }

        protected override void Update()
        {
            base.Update();
            #if UNITY_EDITOR
            if (Input.GetKey(KeyCode.A)) {
                storyManager.GetGameSlots(account).Select(t=>t.Name).ForEach(storyManager.DeleteGameSlot);
            }
            #endif
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            BContinue.onClick.RemoveListener(OnClickContinue);
            BNewGame.onClick.RemoveListener(OnClickNewGame);
            BExit.onClick.RemoveListener(OnClickExit);
            BGameSlotsPanelEscape.onClick.RemoveListener(OnClickGameSlotsPanelEscape);
        }

        private void LoadStories()
        {
            var gameSlots = storyManager.GetGameSlots(account).ToArray();
            var gameSlotButtonsRoot = Node("Canvas/PGameSlots");
            RectTransform lastButtonTransform = null;
            for (int i = 0; i < gameSlots.Length && i < 4; ++i) {
                var gameSlot = gameSlots[i];
                var gameSlotObject = Instantiate(PrefabGameSlotButton, gameSlotButtonsRoot.transform);
                var gameSlotButton = gameSlotObject.GetComponent<Button>();
                gameSlotButton.GetComponentInChildren<Text>().text = gameSlot.Name;
                gameSlotButton.onClick.AddListener(() => {
                    OnClickGameSlot(gameSlotButton);
                });
                var gameSlotButtonTransform = gameSlotButton.transform as RectTransform;
                if (i > 0 && i <= 3) {
                    var anchorMin = lastButtonTransform.anchorMin;
                    var anchorMax = lastButtonTransform.anchorMax;
                    gameSlotButtonTransform.anchorMin = new Vector2(anchorMin.x, anchorMin.y - 0.25f);
                    gameSlotButtonTransform.anchorMax = new Vector2(anchorMax.x, anchorMax.y - 0.25f);
                }
                gameSlotDictionary[gameSlotButton] = gameSlot.Name;
                lastButtonTransform = gameSlotButtonTransform;
            }
            BGameSlotsPanelEscape.transform.SetAsLastSibling();
        }
        
        private void OnClickContinue()
        {
            PGameSlots.SetActive(true);
            BNewGame.interactable = false;
            BContinue.interactable = false;
            BExit.interactable = false;
            LoadStories();
        }

        private void OnClickNewGame()
        {
            string newGameName = $"Game {storyManager.GetGameSlots(account).Count() + 1}";
            var gameSlot = storyManager.CreateGameSlot(newGameName);
            var fitw = FitwGameState.MakeDefault();
            fitw.Save(gameSlot);
            OnClickContinue();
        }

        private void OnClickExit()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }

        private void OnClickGameSlotsPanelEscape()
        {
            foreach (Transform t in PGameSlots.transform) {
                if (t.gameObject.GetComponent<Button>() != BGameSlotsPanelEscape)
                    Destroy(t.gameObject);
            }
            PGameSlots.SetActive(false);
            BNewGame.interactable = true;
            BContinue.interactable = true;
            BExit.interactable = true;
            gameSlotDictionary.Clear();
        }

        private void OnClickGameSlot(Button gameSlotButton)
        {
            Debug.Log(gameSlotDictionary[gameSlotButton]);
        }
    }
}
