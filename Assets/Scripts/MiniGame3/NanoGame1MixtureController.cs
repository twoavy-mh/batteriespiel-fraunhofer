using System;
using UnityEngine;

namespace Minigame3
{
    public class NanoGame1MixtureController : MonoBehaviour
    {
        public event Action OnFinished;
        
        public GameObject activeSlider;
        public GameObject additiveSlider;
        public GameObject solventSlider;

        private NanoGame1SliderController _activeSliderController;
        private NanoGame1SliderController _additiveSliderController;
        private NanoGame1SliderController _solventSliderController;
        
        private bool _gameFinished = false; 
        // Start is called before the first frame update
        void Start()
        {
            _activeSliderController = activeSlider.GetComponent<NanoGame1SliderController>();
            _activeSliderController.OnValueChange +=OnValueChanged;
            
            _additiveSliderController = additiveSlider.GetComponent<NanoGame1SliderController>();
            _additiveSliderController.OnValueChange += OnValueChanged;
            
            _solventSliderController = solventSlider.GetComponent<NanoGame1SliderController>();
        }

        private void Update()
        {
            if (!_gameFinished)
            {
                if (_additiveSliderController.GetIsSolved() && _activeSliderController.GetIsSolved() && _solventSliderController.GetIsCorrect())
                {
                    _solventSliderController.SetSolved();
                    OnGameFinished(); 
                }
            }
        }

        private void OnValueChanged()
        {
            float solventValue = 100;
            
            solventValue -= _activeSliderController.GetCurrentValue();
            solventValue -= _additiveSliderController.GetCurrentValue();
            
            if (solventValue < 0)
            {
                solventValue = 0;
            }
            
            _solventSliderController.SetCurrentValue(solventValue);
            
        }
        
        private void OnGameFinished()
        {
            _gameFinished = true;
            _activeSliderController.OnValueChange -= OnValueChanged;
            _additiveSliderController.OnValueChange -= OnValueChanged;
            OnFinished?.Invoke();
        }
    }
}
