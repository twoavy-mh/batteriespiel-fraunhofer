using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame3
{
    public class NanoGame1Controller : MonoBehaviour
    {
        public event Action OnFinishedGame;
        public event Action OnSkipGame;
        
        public Button skipButton;

        public Transform nanoGame1ParentTarget;
        public GameObject nanoGame1Mobile;
        public GameObject nanoGame1Desktop;

        private GameObject _ng1Instance;
        
        private bool _gameStarted = false;
        private bool _gameFinished = false;
        
        // Start is called before the first frame update
        void Start()
        {
            skipButton.onClick.AddListener(SkipGame);
            
            InstantiateGame();
            
            _gameStarted = true;
        }
        
        private void StopGame()
        {
            skipButton.onClick.RemoveListener(SkipGame);
            _ng1Instance.GetComponent<NanoGame1MixtureController>().OnFinished -= OnGameFinished;
            _gameFinished = true;
        }

        void OnGameFinished()
        {
            StopGame();
            OnFinishedGame?.Invoke();
        }
        
        private void SkipGame()
        {
            StopGame();
            OnSkipGame?.Invoke();
        }
        
        private void InstantiateGame()
        {
            _ng1Instance = Instantiate(Application.isMobilePlatform? nanoGame1Mobile:nanoGame1Desktop, nanoGame1ParentTarget);
            _ng1Instance.GetComponent<NanoGame1MixtureController>().OnFinished += OnGameFinished;
        }
    }
}
