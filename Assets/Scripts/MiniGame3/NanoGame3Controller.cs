using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame3
{
    public class NanoGame3Controller : MonoBehaviour
    {
        public event Action OnFinishedGame;
        public event Action OnSkipGame;
        
        public Button skipButton;
        
        public int beltArrowCount = 9;
        public GameObject spawnPoint;
        public GameObject beltArrowPrefabMobile;
        public GameObject beltArrowPrefabDesktop;
        
        private bool _gameStarted = false;
        private bool _gameFinished = false;

        private float _beltSpeed = 0;
        public float beltSpeed
        {
            get => _beltSpeed;
            set => _beltSpeed = value;
        }
        
        private static int _startOffset = 422;
        private static int _screenOffset = 2580;
        
        private NanoGame3SliderController _beltSpeedSlider;
        private NanoGame3SliderController _heatSlider;

        private Image _heaterImage;
        
        private List<GameObject> _beltObjects = new List<GameObject>();
        private List<int> _startPositions = new List<int>();
        
        // Start is called before the first frame update
        void Start()
        {
            InstantiateBeltArrows();
            _beltSpeedSlider = GameObject.Find("SpeedSlider").GetComponentInChildren<NanoGame3SliderController>();
            _heatSlider = GameObject.Find("HeatSlider").GetComponentInChildren<NanoGame3SliderController>();
            _heaterImage = GameObject.Find("Heater").GetComponentInChildren<Image>();
            
            skipButton.onClick.AddListener(SkipGame);
            
            _gameStarted = true;
        }
        
        void FixedUpdate()
        {
            if (_gameStarted && !_gameFinished)
            {
                foreach (GameObject beltArrow in _beltObjects)
                {
                    MoveBeltDivider(beltArrow);
                }

                HeatingMapColorSetter();
                
                if (_beltSpeedSlider.GetIsSolved() && _heatSlider.GetIsSolved())
                {
                    OnGameFinished();
                }
            }
        }
        
        private void MoveBeltDivider(GameObject beltDivider)
        {
            float value = _beltSpeedSlider.GetCurrentValue();
            
            if (beltDivider.transform.localPosition.x < -_screenOffset)
            {
                beltDivider.transform.localPosition = new Vector3(_screenOffset/2, beltDivider.transform.localPosition.y, 0);
            }
            else
            {
                beltDivider.transform.localPosition = new Vector3(beltDivider.transform.localPosition.x - (value * 10f),
                    beltDivider.transform.localPosition.y, 0);
            }
        }

        private void HeatingMapColorSetter()
        {
            float value = _heatSlider.GetCurrentValue();
            
            if (value < 0.6f)
                _heaterImage.color = Color.Lerp(Settings.ColorMap[Tailwind.BlueUI], Settings.ColorMap[Tailwind.Yellow1], value  / .6f );
            else 
                _heaterImage.color = Color.Lerp(Settings.ColorMap[Tailwind.Yellow1], Settings.ColorMap[Tailwind.Red2], (value - 0.6f) / (1f - 0.6f));
        }

        private void InstantiateBeltArrows()
        {
            // Calculate start positions
            for (int i = 0; i < beltArrowCount; i++)
            {
                _startPositions.Add(-(_screenOffset/2) - _startOffset + i * _startOffset);
            }

            foreach (float startPosition in _startPositions)
            {
                GameObject newBeltDivider = Instantiate(Application.isMobilePlatform? beltArrowPrefabMobile:beltArrowPrefabDesktop, spawnPoint.transform);
                newBeltDivider.transform.localScale = Vector3.one;
                newBeltDivider.transform.localPosition = new Vector3(startPosition,0,0);
                newBeltDivider.name = Application.isMobilePlatform? "BeltDividerMobile":"BeltDividerDesktop";
                _beltObjects.Add(newBeltDivider);
            }
        }
        
        private void StopGame()
        {
            skipButton.onClick.RemoveListener(SkipGame);
            _gameFinished = true;
            _heaterImage.color = Settings.ColorMap[Tailwind.Yellow3];
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

        public void ClearNanoGame()
        {
            
        }
    }
}
