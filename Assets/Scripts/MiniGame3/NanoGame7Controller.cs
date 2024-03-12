using System;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame3
{
    public class NanoGame7Controller : MonoBehaviour
    {
        public event Action OnFinishedGame;
        public event Action OnSkipGame;

        public ImageColorSetter checkIcon;
        public Button skipButton;
        
        public NanoGame7MaskController[] maskControllers;
        
        private int _filledMaskCount = 0;
        
        private bool _gameStarted = false;
        private bool _gameFinished = false;
        
        // Start is called before the first frame update
        void Start()
        {
            skipButton.onClick.AddListener(SkipGame);
            foreach (NanoGame7MaskController maskController in maskControllers)
            {
                maskController.OnFilled += OnFilledMask;
            }
            _gameStarted = true;
        }
        
        private void StopGame()
        {
            checkIcon.UpdateColor(Tailwind.Black, 1f);
            skipButton.onClick.RemoveListener(SkipGame);
            foreach (NanoGame7MaskController maskController in maskControllers)
            {
                maskController.OnFilled -= OnFilledMask;
            }
            _gameFinished = true;
        }

        void OnGameFinished()
        {
            StopGame();
            OnFinishedGame?.Invoke();
        }

        void OnFilledMask()
        {
            _filledMaskCount++;
            if (_filledMaskCount == maskControllers.Length)
            {
                OnGameFinished();
            }
        }

        private void SkipGame()
        {
            StopGame();
            OnSkipGame?.Invoke();
        }
    }
}
