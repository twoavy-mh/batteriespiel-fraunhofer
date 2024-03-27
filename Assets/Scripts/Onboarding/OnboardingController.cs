using System.Collections;
using System.Collections.Generic;
using Helpers;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Onboarding
{
    public class OnboardingController : MonoBehaviour
    {
        public TMP_InputField inputField;
        public GameObject onScreenKeyboard;
        public RectTransform onboardingContent;
        
        private bool _shouldShift = true;
        // Start is called before the first frame update
        void Start()
        {
#if UNITY_WEBGL
            _shouldShift = false;
#endif
            if (Utility.GetDevice() == Device.Mobile || !_shouldShift)
            {
                onScreenKeyboard.SetActive(false);
            }

            if (_shouldShift)
            {
                inputField.onSelect.AddListener((value) => SetActiveFocus(true));
                inputField.onEndEdit.AddListener((value) => SetActiveFocus(false));
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        
        public void SetActiveFocus(bool isActive)
        {
            int posY = isActive ? 250 : -250;
            onboardingContent.DOLocalMove(
                new Vector3(onboardingContent.localPosition.x, onboardingContent.localPosition.y + posY,
                    onboardingContent.localPosition.z), 0.5f).SetEase(Ease.InCubic);
        }
    }
}
