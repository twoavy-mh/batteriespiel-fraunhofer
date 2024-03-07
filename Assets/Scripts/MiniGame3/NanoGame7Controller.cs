using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame3
{
    public class NanoGame7Controller : MonoBehaviour
    {
        public event Action OnFinishedGame;
        public event Action OnSkipGame;
        
        public Button skipButton;
        
        private bool _gameStarted = false;
        private bool _gameFinished = false;
        
        // Start is called before the first frame update
        void Start()
        {
            skipButton.onClick.AddListener(SkipGame);
            
            _gameStarted = true;
        }
        
        private void StopGame()
        {
            skipButton.onClick.RemoveListener(SkipGame);
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
    }
}
