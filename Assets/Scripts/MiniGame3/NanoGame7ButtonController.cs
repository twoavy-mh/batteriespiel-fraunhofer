using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Helpers;

namespace Minigame3
{
    
    public class NanoGame7ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public ImageColorSetter imageColorSetter;
        
        [SerializeField]
        private bool _buttonPressed = false;
        
        [SerializeField]
        private bool _buttonDisabled = false;
        
        public void OnPointerDown(PointerEventData eventData){
            if (_buttonDisabled)
            {
                return;
            }

            _buttonPressed = true;
            imageColorSetter?.UpdateColor(Tailwind.Yellow3, 1f, true);
        }
 
        public void OnPointerUp(PointerEventData eventData){
            _buttonPressed = false; 
            imageColorSetter?.UpdateColor(Tailwind.Yellow3, 0f, true);
        }

        public bool ButtonPressed()
        {
            return _buttonPressed;
        }

        public void DisableButton()
        {
            _buttonDisabled = true;
            ImageColorSetter[] imageColorSetters = transform.GetComponentsInChildren<ImageColorSetter>();
            imageColorSetters[0].UpdateColor(Tailwind.Blue5);
            transform.GetComponentInChildren<FontStyler>().fontColor = Tailwind.Blue5;
        }

        private void OnDisable()
        {
            _buttonDisabled = false;
            _buttonPressed = false;
            ImageColorSetter[] imageColorSetters = transform.GetComponentsInChildren<ImageColorSetter>();
            imageColorSetters[0].UpdateColor(Tailwind.Yellow3);
            transform.GetComponentInChildren<FontStyler>().fontColor = Tailwind.Yellow3;
        }
    }
}
