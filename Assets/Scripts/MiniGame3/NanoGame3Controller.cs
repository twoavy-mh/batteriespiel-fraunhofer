using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UI;
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

        public SelfTranslatingText helpText;
        
        public string helpKeyStandard;
        public string helpKeyPerfectHeat;
        public string helpKeyToHot;
        public string helpKeyToCold;
        public string helpKeyPrefectSpeed;
        public string helpKeyToSlow;
        public string helpKeyToFast;
        
        private bool _gameStarted = false;
        private bool _gameFinished = false;
        
        private string _currentHelpKey = "";
        
        private static int _startOffset = 422;
        private static int _screenOffset = 2580;
        
        private NanoGame3SliderController _beltSpeedSlider;
        private float _beltSpeedValue;
        
        private NanoGame3SliderController _heatSlider;
        private float _heatValue;

        private Image _heaterImage;
        
        private List<GameObject> _beltObjects = new List<GameObject>();
        private List<int> _startPositions = new List<int>();
        
        // Start is called before the first frame update
        void OnEnable()
        {
            InstantiateBeltArrows();
            _beltSpeedSlider = GameObject.Find("SpeedSlider").GetComponentInChildren<NanoGame3SliderController>();
            _beltSpeedSlider.OnValueChange += OnSpeedChange;
            _heatSlider = GameObject.Find("HeatSlider").GetComponentInChildren<NanoGame3SliderController>();
            _heatSlider.OnValueChange += HeatingMapColorSetter;
            _heaterImage = GameObject.Find("Heater").GetComponentInChildren<Image>();
            
            SetHelpText(helpKeyStandard);
            
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
                
                if (_beltSpeedSlider.GetIsSolved() && _heatSlider.GetIsSolved())
                {
                    OnGameFinished();
                }
            }
        }
        
        private void MoveBeltDivider(GameObject beltDivider)
        {
            _beltSpeedValue = _beltSpeedSlider.GetCurrentValue();
            
            if (beltDivider.transform.localPosition.x < -_screenOffset)
            {
                beltDivider.transform.localPosition = new Vector3(_screenOffset/2, beltDivider.transform.localPosition.y, 0);
            }
            else
            {
                beltDivider.transform.localPosition = new Vector3(beltDivider.transform.localPosition.x - (_beltSpeedValue * 10f),
                    beltDivider.transform.localPosition.y, 0);
            }
        }

        private void OnSpeedChange()
        {
            NanoGame3SliderController.IsInRange _valueInRange = _beltSpeedSlider.IsValueInRange();
            switch (_valueInRange)
            {
                case NanoGame3SliderController.IsInRange.toLow:
                    SetHelpText(helpKeyToSlow);
                    break;
                case NanoGame3SliderController.IsInRange.inRange:
                    SetHelpText(helpKeyPrefectSpeed);
                    break;
                case NanoGame3SliderController.IsInRange.toHigh:
                    SetHelpText(helpKeyToFast);
                    break;
                default:
                    SetHelpText(helpKeyStandard);
                    break;
            }
        }
        
        public void OnHeatChange()
        {
            NanoGame3SliderController.IsInRange _valueInRange = _heatSlider.IsValueInRange();
            switch (_valueInRange)
            {
                case NanoGame3SliderController.IsInRange.toLow:
                    SetHelpText(helpKeyToCold);
                    break;
                case NanoGame3SliderController.IsInRange.inRange:
                    SetHelpText(helpKeyPerfectHeat);
                    break;
                case NanoGame3SliderController.IsInRange.toHigh:
                    SetHelpText(helpKeyToHot);
                    break;
                default:
                    SetHelpText(helpKeyStandard);
                    break;
            }
        }

        private void HeatingMapColorSetter()
        {
            _heatValue = _heatSlider.GetCurrentValue();
            OnHeatChange();
            
            if (_heatValue < 0.6f)
                _heaterImage.color = Color.Lerp(Settings.ColorMap[Tailwind.BlueUI], Settings.ColorMap[Tailwind.Yellow1], _heatValue  / .6f );
            else 
                _heaterImage.color = Color.Lerp(Settings.ColorMap[Tailwind.Yellow1], Settings.ColorMap[Tailwind.Red2], (_heatValue - 0.6f) / (1f - 0.6f));
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

        private void SetHelpText(string key)
        {
            if (_currentHelpKey == key)
                return;
            
            _currentHelpKey = key;
            helpText.translationKey = key;
        }

        private void SkipGame()
        {
            StopGame();
            OnSkipGame?.Invoke();
        }

        public void OnDisable()
        {
            foreach (GameObject beltArrow in _beltObjects)
            {
                DestroyImmediate(beltArrow);
            }

            _gameStarted = false;
            _gameFinished = false;
            _currentHelpKey = "";
            _beltSpeedValue = 0f;
            _heatValue = 0f;
            _heaterImage.color = Settings.ColorMap[Tailwind.BlueUI];
            _beltObjects = new List<GameObject>();
            _startPositions = new List<int>();
        }
    }
}
